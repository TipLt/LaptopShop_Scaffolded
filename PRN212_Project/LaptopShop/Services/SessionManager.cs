using LaptopShop.Model;

namespace LaptopShop.Services
{
    // Singleton Pattern: Ensures only one instance manages the session
    public class SessionManager
    {
        private static SessionManager? _instance;
        private static readonly object _lock = new object();

        public User? CurrentUser { get; private set; }

        private SessionManager() { }

        public static SessionManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SessionManager();
                        }
                    }
                }
                return _instance;
            }
        }

        public void Login(User user)
        {
            CurrentUser = user;
        }

        public void Logout()
        {
            CurrentUser = null;
        }

        public bool IsLoggedIn()
        {
            return CurrentUser != null;
        }

        public bool HasRole(string role)
        {
            return CurrentUser?.Role == role;
        }

        public bool HasAnyRole(params string[] roles)
        {
            if (CurrentUser == null) return false;
            foreach (var role in roles)
            {
                if (CurrentUser.Role == role) return true;
            }
            return false;
        }
    }
}
