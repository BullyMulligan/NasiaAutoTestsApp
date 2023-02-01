using System.Collections.Generic;
using System.Threading;
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
        private VendorTests _setup;
        private List<bool> result= new List<bool>();

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
            InitializationSetup();
            auth = new VendorTests( _login, _password);
            auth.Auth();
            
            CheckTests("//div[@class='page-title']//h2","Новый договор");
        }
        void StartNegativeLogin()
        {
            InitializationSetup();
            auth = new VendorTests( _negativeLogin, _password);
            auth.Auth();
            CheckTests("//div[@class='Vue-Toastification__toast Vue-Toastification__toast--error top-right']//div[@role='alert']","Выбранное значение для ID партнера некорректно.");
            
        }
        void StartNegativePassword()
        {
            InitializationSetup();
            auth = new VendorTests( _login, _negativePassword);
            auth.Auth();
            CheckTests("//div[@class='Vue-Toastification__toast Vue-Toastification__toast--error top-right']//div[@role='alert']","Некорректный пароль");
        }
        void StartNullLogin()
        {
            InitializationSetup();
            auth = new VendorTests( "", _password);
            auth.Auth();
            CheckTests("//div[@class='n-form-item-feedback__line']","Пожалуйста заполните это поле!");

        }
        void StartNullPassword()
        {
            InitializationSetup();
            auth = new VendorTests( _login, "");
            auth.Auth();
            Thread.Sleep(200);
            CheckTests("//div[@class='n-form-item-feedback__line']","Пожалуйста заполните это поле!");
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

        void CheckTests(string actual, string expected)
        {
            if (auth.ActualExpected(actual,expected))
            {
                result.Add(true);
                MessageBox.Show("Тест прошел успешно");
            }
            else
            {
                result.Add(false);
                MessageBox.Show("Тест провален");
            }
        }
        void InitializationSetup()
        {
            _setup = new VendorTests(_instance);
            _setup.Setup();
        }


    }
}