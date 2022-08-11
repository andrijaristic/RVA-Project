using Server.Interfaces.DataInitializerInterfaces;

namespace Server.DataInitializers
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserDataInitializer _userDataInitializer;

        public DataInitializer(IUserDataInitializer userDataInitializer)
        {
            _userDataInitializer = userDataInitializer;
        }

        public void InitializeData()
        {
            _userDataInitializer.InitializeData();
        }
    }
}
