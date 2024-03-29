using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace NasiaAutoTestsApp
{
    public class Auth
    {
        
        private string _login;
        private string _password;
        private string _negativeLogin;
        private string _negativePassword;
        private CheckedListBox _check;
        private VendorTests auth;
        private VendorTests _setup;
        public List<bool> result= new List<bool>();
        public List<Image> screens = new List<Image>();
        private Screen screenshot = new Screen();

        public Auth(string login, string password, string negativeLogin, string negativePassword,
            CheckedListBox check)
        {
            _login = login;
            _password = password;
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
            screens.Add(screenshot.CreateScreenshot("Positive",auth));
        }
        void StartNegativeLogin()
        {
            InitializationSetup();
            auth = new VendorTests( _negativeLogin, _password);
            auth.Auth();
            CheckTests("//div[@class='Vue-Toastification__toast Vue-Toastification__toast--error top-right']//div[@role='alert']","Выбранное значение для partner id некорректно.");
            screens.Add(screenshot.CreateScreenshot("NegativeLogin",auth));
            
        }
        void StartNegativePassword()
        {
            InitializationSetup();
            auth = new VendorTests( _login, _negativePassword);
            auth.Auth();
            CheckTests("//div[@class='Vue-Toastification__toast Vue-Toastification__toast--error top-right']//div[@role='alert']","Некорректный пароль");
            screens.Add(screenshot.CreateScreenshot("NegativePassword",auth));
        }
        void StartNullLogin()
        {
            InitializationSetup();
            auth = new VendorTests( "", _password);
            auth.Auth();
            CheckTests("//div[@class='n-form-item-feedback__line']","Пожалуйста заполните это поле!");
            screens.Add(screenshot.CreateScreenshot("NullLogin",auth));
        }
        void StartNullPassword()
        {
            InitializationSetup();
            auth = new VendorTests( _login, "");
            auth.Auth();
            Thread.Sleep(200);
            CheckTests("//div[@class='n-form-item-feedback__line']","Пожалуйста заполните это поле!");
            screens.Add(screenshot.CreateScreenshot("NullPassword",auth));
        }
        //метод, запускающий тесты в зависимости от выбранных чек-листов.
        public void StartTests()
        {
            if (_check.GetItemChecked(0))
            {
                StartPositiveTest();
            }
            else
            {
                screens.Add(null);
            }
            if (_check.GetItemChecked(1))
            {
                StartNegativeLogin();
            }
            else
            {
                screens.Add(null);
            }
            if (_check.GetItemChecked(2))
            {
                StartNegativePassword();
            }
            else
            {
                screens.Add(null);
            }
            if (_check.GetItemChecked(3))
            {
                StartNullLogin();
            }
            else
            {
                screens.Add(null);
            }
            if (_check.GetItemChecked(4))
            {
                StartNullPassword();
            }
            else
            {
                screens.Add(null);
            }
        }

        
        void CheckTests(string actual, string expected)
        {
            if (auth.ActualExpected(actual,expected))
            {
                result.Add(true);
            }
            else
            {
                result.Add(false);
            }
        }
        void InitializationSetup()
        {
            _setup = new VendorTests();
            _setup.Setup();
        }
        
    }
}