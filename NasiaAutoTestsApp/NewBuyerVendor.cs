using System.Collections.Generic;
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
            _newBuyer = new VendorTests(_buyer,_card,_date,_photo);
            //запускаем
            _newBuyer.NewBuyer();
            
            //проверяем, верен ли результат теста
            CheckTests("//div[@class='n-form-item-blank']/h1","Покупатель успешно отправлен на модерацию!");
            DataBase setStatus = new DataBase("10.20.33.5", "paym_kayden", "dev-base", "Xe3nQx287");
            setStatus.SetStatusBuyer(_buyer,4);
            setStatus.SetLimitBuyer(_buyer,1000000);
        }
        
        //пустое поле ввода мешает нам закончить тест
        void startNullPhoneTests()
        {
            InitializationSetup();
            _newBuyer = new VendorTests( _login, _password, "",_card,_date,_photo);
            _newBuyer.NewBuyer();
            
            //проверяем, правильно ли выполнен тест
            CheckTests("//span[@class='error-text']","Пожалуйста заполните это поле!");
            
        }

        //пробуем нажать на кнопку отправки ОТП кода, оставив поле ОТП пустым
        void StartNullOTPTest()
        {
            InitializationSetup();
            _newBuyer = new VendorTests( _buyer,"",_card,_date,_photo);
            _newBuyer.NewBuyer();
            
            CheckTests("(//span[@class='error-text'])[2]","Пожалуйста заполните это поле!");
        }

        //пробуем ввести неверный ОТП-код
        void StartNegativeOTPTest()
        {
            InitializationSetup();
            _newBuyer = new VendorTests( _buyer,"1234",_card,_date,_photo);
            _newBuyer.NewBuyer();
            CheckTests("//div[@role='alert']","СМС код неверный");
        }

        //какрта и телефон пользователя не связаны друг с другом
        void StartNegativePhoneWithCard()
        {
            InitializationSetup();
            _newBuyer = new VendorTests(_negativeBuyer,_card,_date,_photo);
            
            _newBuyer.NewBuyer();
            //MessageBox.Show("");
            CheckTests("//div[@class='Vue-Toastification__toast Vue-Toastification__toast--error top-right']//div[@role='alert']","Телефон клиента не совпадает с телефоном смс информирования карты");
        }

        //номера телефонов гарантов идентичны
        void StartEqualsGuarantsTests()
        {
            InitializationSetup();
            _newBuyer = new VendorTests(_buyer,_card,_date,_photo,true);
            _newBuyer.NewBuyer();
            CheckTests("//div[@label='Номер телефона']//span[@class='error-text']","Нельзя вводить одинаковые номера! ");
        }
        
        //метод, запускающий тесты в зависимости от того, включен ли данный тест
        public void StartTests()
        {
            if (_check.GetItemChecked(0))
            {
                StartPositiveTest();
            }
            if (_check.GetItemChecked(1))
            {
                startNullPhoneTests();
            }
            if (_check.GetItemChecked(2))
            {
                StartNullOTPTest();
            }
            if (_check.GetItemChecked(3))
            {
                StartNegativeOTPTest();
            }
            if (_check.GetItemChecked(4))
            {
                StartNegativePhoneWithCard();
            }
            if (_check.GetItemChecked(5))
            {
                StartEqualsGuarantsTests();
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