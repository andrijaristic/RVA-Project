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

            // #TODO: Improve and/or simplify logic.
            //List<Student> students = await _unitOfWork.Students.GetStudentsForUser(loginDTO.Username);
            //Student student = students[0];
            //students.Clear();
            //Student student = await _unitOfWork.Students.GetStudentForUser(loginDTO.Username);

            //List<StudentResult> results = await _unitOfWork.StudentResults.GetExamsForStudent(student.Id);

            //List<Exam> exams = new List<Exam>();
            //foreach (var el in results)
            //{
            //    //Exam exam = await _unitOfWork.Exams.GetExamAsync(el.ExamId);
            //    Exam exam = await _unitOfWork.Exams.GetExamComplete(el.ExamId);
            //    if (exam != null)
            //    {
            //        exams.Add(exam);
            //    }
            //}

            // Priority => #TODO: Simplify. 17/08 - Fixed with repo function above.
            //foreach (var el in exams)
            //{
            //    foreach (var _el in results)
            //    {
            //        if (_el.ExamId == el.Id)
            //        {
            //            el.StudentResults.Add(_el);
            //        }
            //    }
            //}

            //authDTO.Exams = exams;

            return authDTO;
        }

        // Make a new Student class entity every time STUDENT is registered.
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

            Student student = _mapper.Map<Student>(registerDTO);
            student.UserUsername = user.Username;

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.Students.AddAsync(student);
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

            // Update Student sa tim Username.
            List<Student> students = await _unitOfWork.Students.GetStudentsForUser(updateDTO.Username);
            foreach (var el in students)
            {
                el.Name = updateDTO.Name;
                el.LastName = updateDTO.LastName;
            }

            await _unitOfWork.SaveAsync();

            return user;
        }
    }
}
