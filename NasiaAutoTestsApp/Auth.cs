using System.Windows.Forms;

namespace NasiaAutoTestsApp
{
    public class Auth
    {
        
        private string _login;
        private string _password;
        private string _instance;
        private string _negativeLogin;
        private string _negativePassword;
        private CheckedListBox _check;
        private VendorTests auth;

        public Auth(string login, string password, string instance, string negativeLogin, string negativePassword,
            CheckedListBox check)
        {
            _login = login;
            _password = password;
            _instance = instance;
            _negativeLogin = negativeLogin;
            _negativePassword = negativePassword;
            _check = check;
        }
        
        

        //методы с тестом авторизации разных сценариев
        void StartPositiveTest()
        {
            auth = new VendorTests(_instance, _login, _password);
            auth.Auth();
        }
        void StartNegativeLogin()
        {
            auth = new VendorTests(_instance, _negativeLogin, _password);
            auth.Auth();
        }
        void StartNegativePassword()
        {
            auth = new VendorTests(_instance, _login, _negativePassword);
            auth.Auth();
        }
        void StartNullLogin()
        {
            auth = new VendorTests(_instance, "", _password);
            auth.Auth();
        }
        void StartNullPassword()
        {
            auth = new VendorTests(_instance, _login, "");
            auth.Auth();
        }
        //метод, запускающий тесты в зависимости от выбранных чек-листов.
        public void StartTests()
        {
            if (_check.GetItemChecked(0))
            {
                StartPositiveTest();
            }
            if (_check.GetItemChecked(1))
            {
                StartNegativeLogin();
            }
            if (_check.GetItemChecked(2))
            {
                StartNegativePassword();
            }
            if (_check.GetItemChecked(3))
            {
                StartNullLogin();
            }
            if (_check.GetItemChecked(4))
            {
                StartNullPassword();
            }
        }


    }
}