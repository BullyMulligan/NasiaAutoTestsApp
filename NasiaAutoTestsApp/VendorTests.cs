using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace NasiaAutoTestsApp
{
    //в данном классе запускаются тесты селениум. Тесты модульные, то есть, чтобы запустить создание нового клиента,
    //необходимо запустить преднастройку драйвера, затем пройти авторизацию, а затем сам тест
    public class VendorTests:PagesElements
    {
        private DataBase _responce;
        private string _instance;
        private string _login;
        private string _password;
        private string _buyer;
        private Overloads _test= new Overloads();
        public string _otpCode;
        private string _card;
        private string _date;
        private string _photo;
        private bool _equal;
        public bool result;

        public VendorTests(string instance)
        {
            _instance = instance;
        }
        public VendorTests(string login,string password)
        {
            _login = login;
            _password = password;
        }
        public VendorTests(string buyer,string otpCode,string card,string date,string photo)
        {
            _buyer = buyer;
            _card = card;
            _date = date;
            _otpCode = otpCode;
            _photo = photo;
        }
        public VendorTests(string buyer,string card,string date,string photo,bool equal)
        {
            _buyer = buyer;
            _card = card;
            _date = date;
            _photo = photo;
            _equal = equal;
        }
        public VendorTests(string buyer,string otpCode,string card,string date,string photo,bool equal)
        {
            _buyer = buyer;
            _card = card;
            _date = date;
            _photo = photo;
            _equal = equal;
        }
        public VendorTests(string buyer,string card,string date,string photo)
        {
            _buyer = buyer;
            _card = card;
            _date = date;
            _photo = photo;
        }
        public VendorTests(string instance,string login,string password,string buyer,string card,string date,string photo,bool equal)
        {
            _login = login;
            _password = password;
            _instance = instance;
            _buyer = buyer;
            _card = card;
            _date = date;
            _photo = photo;
            _equal = equal;

        }
        public VendorTests(string instance,string login,string password,string buyer, string otpCode,string card,string date,string photo)
        {
            _login = login;
            _password = password;
            _instance = instance;
            _buyer = buyer;
            _otpCode = otpCode;
            _card = card;
            _date = date;
            _photo = photo;

        }
        public VendorTests(string instance,string login,string password,string buyer,string card,string date,string photo)
        {
            _login = login;
            _password = password;
            _instance = instance;
            _buyer = buyer;
            _card = card;
            _date = date;
            _photo = photo;
        }

        //метод преднастройки тестов. Открывает браузер, настраивает таймспаны
        public void Setup()
        {
            Form1.Driver = new OpenQA.Selenium.Chrome.ChromeDriver();//открываем Хром
            Form1.Driver.Navigate().GoToUrl($"https://{_instance}.paymart.uz/ru");//вводим адрес сайта
            Form1.Driver.Manage().Window.Maximize();//открыть в полном окне
            Form1.Driver.Manage().Timeouts().ImplicitWait=TimeSpan.FromSeconds(10);
            Form1.Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
            Form1.Driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(10);
        }

        //метод авторизации
        public void  Auth()
        {
            _test.SendKeys(_fieldIdVendor,_login);
            _test.SendKeys(_fieldPasswordVendor,_password);
            _test.Click(_authButton);
        }

        //метод создания нового пользователя
        public void NewBuyer()
        {
            try
            {
                //удаляем пользователя из базы
                _responce = new DataBase("10.20.33.5", "paym_kayden", "dev-base", "Xe3nQx287");
                _responce.DeleteUser(_buyer);
                
                //нажимаем на кнопку сайдабара(3), вводим номер покупателя и жмем кнопку "отправить ОТП-код"
                _test.Click(_listButtonsSideBar,3);
                _test.SendKeys(_fieldNewBuyerVendor,_buyer);
                //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(_buttonNewBuyerVendor));
                _test.Click(_buttonNewBuyerVendor);

                //если мы не передаем конкретный отп в тест, то берем его из базы
                if (_otpCode==null)
                {
                    _responce = new DataBase("10.20.33.5", "paym_kayden", "dev-base", "Xe3nQx287");
                    _responce.request = "select code from otp_enter_code_attempts WHERE phone = 998"+_buyer;
                    Thread.Sleep(500);
                    _responce.GetOTP();
                    _otpCode = _responce.code;
                }
                
                //вставляем его в поле и жмем кнопку "Отправить"
                _test.SendKeys(_fieldOTPCode,_otpCode);
                _test.Click(_buttonOtpNewBuyerVendor);
                
            
                //вводим номер карты, дату и жмем на кнопку запроса ОТП
                _test.SendKeys(_fieldCardNumberBuyerVendor,_card);
                _test.SendKeys(_fieldCardDataBuyerVendor,_date);
                //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(_buttonCheckCardVendor));
                _test.Click(_buttonCheckCardVendor);

                if (Form1.Driver.FindElements(_messageErrorCardNotPhone).Count==0)
                {
                    //берем отп из базы, он должен отличаться от предыдущего кода
                    while (_responce.code==_otpCode)
                    {
                        _responce.request = "select code from otp_enter_code_attempts WHERE phone = 998"+_buyer;
                        Thread.Sleep(500);
                        _responce.GetOTP();
                    }
                }
                else
                {
                    return;
                }
                _otpCode = _responce.code;

                
                //заполняем поле отп кодом и жмем кнопку "отправить"
                _test.SendKeys(_fieldOtpCardCode,_otpCode);
                _test.Click(_buttonCheckCardOTPVendor);

                //далее следует блок с заливкой фотографий
                for (int i = 0; i < 3; i++)
                {
                    _test.SendKeys(_fieldPhotoVendor,i, _photo);
                }
                //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(_buttonSavePhotosVendor));
                Thread.Sleep(500);
                _test.Click(_buttonSavePhotosVendor);
                
                //заполняем поля с доверителями
                _test.SendKeys(_fieldName1,"Брэд Питт");
                _test.SendKeys(_fieldName2,"Хлеб Пита");
                
                //если телефоны доверителей не должны совпадать
                if (!_equal)
                {
                    _test.SendKeys(_fieldGarantNumber1,"940937094");
                    _test.SendKeys(_fieldGarantNumber2,"940937093");
                    _test.SendKeys(_fieldGarantNumber1,"9");
                    _test.SendKeys(_fieldGarantNumber2,"9");
                }
                
                //если должны
                else
                {
                    _test.SendKeys(_fieldGarantNumber1,"9940937094");
                    _test.SendKeys(_fieldGarantNumber2,"9940937094");
                    _test.SendKeys(_fieldGarantNumber1,"9");
                    _test.SendKeys(_fieldGarantNumber2,"9");
                }
            
                //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable((_buttonSaveBuyerVendor)));
                _test.Click(_buttonSaveBuyerVendor);
                


            }
            catch (Exception e)
            {
            }
            
        }

        public void FindContract(int contract)
        {
            Auth();
        }

        public bool ActualExpected(string actual,string expected)
        {
            if (expected.Replace(" ","")==Form1.Driver.FindElement(By.XPath(actual)).Text.Replace(" ",""))
            {
                Exit();
                return true;
            }
            Exit();
            return false;
        }
        private void Exit()
        {
            Form1.Driver.Quit();
        }
    }
}