using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Server.Dto.UserDto;
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

        public async Task<AuthenticatedDTO> Login(LoginDTO loginDTO)
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

            AuthenticatedDTO authDTO = _mapper.Map<AuthenticatedDTO>(user);
            authDTO.Token = token;

            return authDTO;
        }

        public async Task<User> RegisterNewUser(RegisterDTO registerDTO)
        {
            User user = _mapper.Map<User>(registerDTO);
            ValidationResult result = _userValidation.Validate(user);

            if (!result.isValid)
            {
                throw new Exception(result.Message);
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.UserType = Enums.EUserType.STUDENT;
            //user.Exams = new List<int>();
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveAsync();

            return user;
        }

        public async Task<User> UpdateUser(UpdateDTO updateDTO)
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

            await _unitOfWork.SaveAsync();

            return user;
        }
    }
}
