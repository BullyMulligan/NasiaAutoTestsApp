using System;
using System.Collections.Generic;
using System.IO;
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
            CreateScreenshot("Positive");
        }
        void StartNegativeLogin()
        {
            InitializationSetup();
            auth = new VendorTests( _negativeLogin, _password);
            auth.Auth();
            CheckTests("//div[@class='Vue-Toastification__toast Vue-Toastification__toast--error top-right']//div[@role='alert']","Выбранное значение для partner id некорректно.");
            CreateScreenshot("NegativeLogin");
        }
        void StartNegativePassword()
        {
            InitializationSetup();
            auth = new VendorTests( _login, _negativePassword);
            auth.Auth();
            CheckTests("//div[@class='Vue-Toastification__toast Vue-Toastification__toast--error top-right']//div[@role='alert']","Некорректный пароль");
            CreateScreenshot("NegativePassword");
        }
        void StartNullLogin()
        {
            InitializationSetup();
            auth = new VendorTests( "", _password);
            auth.Auth();
            CheckTests("//div[@class='n-form-item-feedback__line']","Пожалуйста заполните это поле!");
            CreateScreenshot("NullLogin");
        }
        void StartNullPassword()
        {
            InitializationSetup();
            auth = new VendorTests( _login, "");
            auth.Auth();
            Thread.Sleep(200);
            CheckTests("//div[@class='n-form-item-feedback__line']","Пожалуйста заполните это поле!");
            CreateScreenshot("NullPassword");
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

        void CreateScreenshot(string name)
        {
            string parentPath = AppDomain.CurrentDomain.BaseDirectory;
            string folderName = "Auth";
            string path = Path.Combine(parentPath,folderName);
            
            //проверяем наличие папки, если ее не существует, то создаем ее
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            auth.CreateFolder(name,path);
        }


    }
}