using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NasiaAutoTestsApp
{
    public class NewBuyerVendor
    {
        private string _login;
        private Log _log;
        private string _password;
        private CheckedListBox _check;
        private string _buyer;
        private string _negativeBuyer;
        private VendorTests _setup;
        private VendorTests _auth;
        private VendorTests _newBuyer;
        public string _card;
        private string _date;
        private string _photo;
        private bool _equal;
        public List<bool> result= new List<bool>();
        public List<Image> screens = new List<Image>();
        private Screen screenshot = new Screen();
        public PasportData _pasportData;

        public class PasportData
        {
            public string _serial;
            public string _id;
            public string _birthday;

            public PasportData(string serial, string id, string birthday)
            {
                _serial = serial;
                _id = id;
                _birthday = birthday;
            }
        }

        public NewBuyerVendor(string login, string password,string buyer,string negativeBuyer, CheckedListBox check,string card,string date,string photo)
        {
            _login = login;
            _password = password;
            _check = check;
            _buyer = buyer;
            _negativeBuyer=negativeBuyer;
            _card =card;
            _date = date;
            _photo = photo;
        }
        public NewBuyerVendor(string buyer,string card,string date,string photo)
        {
            _buyer = buyer;
            _card =card;
            _date = date;
            _photo = photo;
        }
        //методы запускающие разные сценарии теста
        
        //позитивный сценарий. Все данные верны, тест должен быть пройден и создать клиента
        public void StartPositiveTest()
        {
            //инициализируем данные настройки перед тестом
            InitializationSetup();
            //инициализируем данные самого теста
            _newBuyer = new VendorTests(_buyer,_card,_date,_photo,_pasportData);
            //запускаем
            _newBuyer.NewBuyer(4,1000000);
            
            //проверяем, верен ли результат теста
            CheckTests("//h1[@class='success__title title']","Поздравляем! Ваш лимит рассрочки: 1000000.00 cум");
            screens.Add(screenshot.CreateScreenshot("Positive",_newBuyer));
        }
        
        //пустое поле ввода мешает нам закончить тест
        void startNullPhoneTests()
        {
            InitializationSetup();
            _newBuyer = new VendorTests( _login, _password, "",_card,_date,_photo);
            _newBuyer.NewBuyer(4,1000000);
            
            //проверяем, правильно ли выполнен тест
            CheckTests("//h2[@class='title']","Добро пожаловать");
            screens.Add(screenshot.CreateScreenshot("NullPhone",_newBuyer));
            
        }

        //пробуем нажать на кнопку отправки ОТП кода, оставив поле ОТП пустым
        void StartNullOTPTest()
        {
            InitializationSetup();
            _newBuyer = new VendorTests( _buyer,"",_card,_date,_photo);
            _newBuyer.NewBuyer(4,1000000);
            
            CheckTests("(//h2[@class='title'])","Введите код из СМС");
            screens.Add(screenshot.CreateScreenshot("NullOTP",_newBuyer));
        }

        //пробуем ввести неверный ОТП-код
        void StartNegativeOTPTest()
        {
            InitializationSetup();
            _newBuyer = new VendorTests( _buyer,"1234",_card,_date,_photo);
            _newBuyer.NewBuyer(4,1000000);
            CheckTests("//div[@class='Vue-Toastification__toast Vue-Toastification__toast--error top-right']//div[@role='alert']","СМС код неверный");
            screens.Add(screenshot.CreateScreenshot("NegativeOTP",_newBuyer));
        }

        //какрта и телефон пользователя не связаны друг с другом
        void StartNegativePhoneWithCard()
        {
            InitializationSetup();
            _newBuyer = new VendorTests(_negativeBuyer,_card,_date,_photo,_pasportData);
            
            _newBuyer.NewBuyer(4,1000000);
            //MessageBox.Show("");
            CheckTests("//div[@class='Vue-Toastification__toast Vue-Toastification__toast--error top-right']//div[@role='alert']","Телефон клиента не совпадает с телефоном смс информирования карты");
            screens.Add(screenshot.CreateScreenshot("NegativePhone",_newBuyer));
        }

        //номера телефонов гарантов идентичны
        void StartEqualsGuarantsTests()
        {
            InitializationSetup();
            _newBuyer = new VendorTests(_buyer,_card,_date,_photo,true);
            _newBuyer.NewBuyer(4,1000000);
            CheckTests("//div[@label='Номер телефона']//span[@class='error-text']","Нельзя вводить одинаковые номера! ");
            screens.Add(screenshot.CreateScreenshot("Guarants",_newBuyer));
        }
        
        //метод, запускающий тесты в зависимости от того, включен ли данный тест
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
                startNullPhoneTests();
            }
            else
            {
                screens.Add(null);
            }
            if (_check.GetItemChecked(2))
            {
                StartNullOTPTest();
            }
            else
            {
                screens.Add(null);
            }
            if (_check.GetItemChecked(3))
            {
                StartNegativeOTPTest();
            }
            else
            {
                screens.Add(null);
            }
            if (_check.GetItemChecked(4))
            {
                StartNegativePhoneWithCard();
            }
            else
            {
                screens.Add(null);
            }
            if (_check.GetItemChecked(5))
            {
                StartEqualsGuarantsTests();
            }
            else
            {
                screens.Add(null);
            }
        }

        void CheckTests(string actual, string expected)
        {
            if (_newBuyer.ActualExpected(actual,expected))
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
            _setup = new VendorTests();_setup.Setup();
            _auth = new VendorTests(_login, _password);_auth.Auth();
        }
    }
}