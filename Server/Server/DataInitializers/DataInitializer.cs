using Server.Interfaces.DataInitializerInterfaces;

namespace Server.DataInitializers
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserDataInitializer _userDataInitializer;
        private readonly IStudentDataInitializer _studentDataInitializer;
        private readonly ISubjectDataInitializer _subjectDataInitializer;
        private readonly IStudentResultDataInitializer _studentResultDataInitializer;
        private readonly IExamDataInitializer _examDataInitializer;

        public DataInitializer(IUserDataInitializer userDataInitializer, IStudentDataInitializer studentDataInitializer, ISubjectDataInitializer subjectDataInitializer, IStudentResultDataInitializer studentResultDataInitializer, IExamDataInitializer examDataInitializer)
        {
            _userDataInitializer = userDataInitializer;
            _studentDataInitializer = studentDataInitializer;
            _subjectDataInitializer = subjectDataInitializer;
            _studentResultDataInitializer = studentResultDataInitializer;
            _examDataInitializer = examDataInitializer; 
        }

        public void InitializeData()
        {
            _userDataInitializer.InitializeData();
            _studentDataInitializer.InitializeData();
            _subjectDataInitializer.InitializeData();
            _examDataInitializer.InitializeData();
            _studentResultDataInitializer.InitializeData();
        }
    }
}
