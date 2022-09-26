using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Server.Dto.LogsDto;
using Server.Dto.UserDto;
using Server.Enums;
using Server.Interfaces.ServiceInterfaces;
using Server.Interfaces.TokenMakerInterfaces;
using Server.Interfaces.UnitOfWorkInterfaces;
using Server.Interfaces.ValidationInterfaces;
using Server.Models;
using System.Text;

namespace Server.Services
{
    public class UserService : IUserService
    {
        private readonly IConfigurationSection _secretKey;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenMakerFactory _tokenMakerFactory;
        private readonly IValidation<User> _userValidation;

        public UserService (IConfiguration config, IUnitOfWork unitOfWork, ITokenMakerFactory tokenMakerFactory, IMapper mapper, IValidation<User> userValidation)
        {
            _secretKey = config.GetSection("SecretKey");
            _unitOfWork = unitOfWork;
            _tokenMakerFactory = tokenMakerFactory;
            _mapper = mapper;
            _userValidation = userValidation;
        }

        public async Task<List<LogDTO>> GetLogs(string username)
        {
            string filename = $"logs/logs{DateTime.Now.Year.ToString()}.txt";

            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete))
            {
                string line;
                StreamReader sr = new StreamReader(fs);
                List<LogDTO> logs = new List<LogDTO>();

                while ((line = await sr.ReadLineAsync()) != null)
                {
                    string logUsername = line.Substring(31).Split(' ')[1].Split(':')[0];
                    if (!String.Equals(username, logUsername)) { continue; }

                    LogDTO log = new LogDTO();
                    log.Timestamp = line.Substring(0, 23);
                    log.EventType = line.Substring(32, 3);
                    log.Message = line.Substring(line.LastIndexOf(':') + 2);

                    logs.Add(log);
                }

                sr.Close();
                logs.Reverse();

                return logs;
            }
        }

        public async Task<AuthDTO> Login(LoginDTO loginDTO)
        {
            User user = await _unitOfWork.Users.FindUserByUsername(loginDTO.Username);

            if (user == null)
            {
                throw new Exception("Invalid username!");
            }

            if (!BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password))
            {
                throw new Exception("Invalid password!");
            }

            // Create token and give to user.
            ITokenMaker tokenMaker = _tokenMakerFactory.CreateTokenMaker(Enums.ETokenType.JWT);
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey.Value));
            string token = tokenMaker.CreateToken(user, key);

            AuthDTO authDTO = _mapper.Map<AuthDTO>(user);
            authDTO.Token = token;

            return authDTO;
        }

        public async Task<DisplayUserDTO> RegisterNewUser(RegisterDTO registerDTO)
        {
            User user = _mapper.Map<User>(registerDTO);
            ValidationResult result = _userValidation.Validate(user);

            if (!result.isValid)
            {
                throw new Exception(result.Message);
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            Enum.TryParse<EUserType>(registerDTO.UserType, out EUserType userType);
            user.UserType = userType;

            await _unitOfWork.Users.AddAsync(user);
            if (userType == EUserType.STUDENT)
            {
                Student student = _mapper.Map<Student>(registerDTO);
                student.UserUsername = user.Username;

                await _unitOfWork.Students.AddAsync(student);
            }

            await _unitOfWork.SaveAsync();
            return _mapper.Map<DisplayUserDTO>(user);
        }

        public async Task<DisplayUserDTO> UpdateUser(UpdateDTO updateDTO)
        {
            User user = await _unitOfWork.Users.FindUserByUsername(updateDTO.Username);

            if (user == null)
            {
                throw new Exception("Request is invalid!");
            }

            user.Name = updateDTO.Name;
            user.Lastname = updateDTO.LastName;
            ValidationResult result = _userValidation.Validate(user);

            if (!result.isValid)
            {
                throw new Exception(result.Message);
            }

            // Update Student sa tim Username.
            List<Student> students = await _unitOfWork.Students.GetStudentsForUser(updateDTO.Username);
            foreach (var el in students)
            {
                el.Name = updateDTO.Name;
                el.LastName = updateDTO.LastName;
            }

            await _unitOfWork.SaveAsync();

            return _mapper.Map<DisplayUserDTO>(user);
        }
    }
}
