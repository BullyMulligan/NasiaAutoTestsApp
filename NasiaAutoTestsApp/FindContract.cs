using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using OpenQA.Selenium;

namespace NasiaAutoTestsApp
{
    public class FindContract
    {
        private string _buyerNumber;
        public List<bool> result = new List<bool>();
        private VendorTests _setup;
        private VendorTests _auth;
        private NewProduct _newProduct;
        private VendorTests _BuyerStatus;
        private VendorTests _findContract;
        private string _login;
        private string _password;
        private string _buyer;
        private string _card;
        private string _date;
        private string _photo;
        private int _contract;
        private CheckedListBox _check;
        private VendorTests _statusBuyer;
        private bool _avalible;
        private List<string> list;

        public FindContract(int contract)
        {
            _contract = contract;
        }
        public FindContract (string login, string password, string buyer, CheckedListBox check,string card,string date,string photo)
        {
            _login = login;
            _password = password;
            _check = check;
            _buyer = buyer;
            _card = card;
            _date = date;
            _photo = photo;
        }
        
        public void StartPositiveTest()
        {
            _newProduct = new NewProduct(_login, _password, _buyer,_card,_date,_photo);
            _newProduct.CreateNewProduct(4,10000000);
            _findContract = new VendorTests(_buyer);
            _findContract.FindByContractNumber();

            MessageBox.Show("");
            CheckTests("//span[@class='n-tag__content']","На модерации");
        }

        public void StartPositiveTestPhone()
        {
            InitializationSetup();
            _findContract = new VendorTests(_buyer);
            _findContract.FindByBuyerNumber(0);
            var responce = new DataBase("10.20.33.5", "paym_kayden", "dev-base", "Xe3nQx287");
            responce.GetContractsId(_buyer);
            CheckTests(responce);
        }
        public void StartTestFindStatusModerate()
        {
            InitializationSetup();
            _findContract = new VendorTests(_buyer);
            _findContract.FindByBuyerNumber(1);
            var responce = new DataBase("10.20.33.5", "paym_kayden", "dev-base", "Xe3nQx287");
            responce.GetContractsIdByStatus(_buyer,0);
            CheckTests(responce);
        }
        public void StartTestFindStatusSuccess()
        {
            InitializationSetup();
            _findContract = new VendorTests(_buyer);
            _findContract.FindByBuyerNumber(2);
            var responce = new DataBase("10.20.33.5", "paym_kayden", "dev-base", "Xe3nQx287");
            responce.GetContractsIdByStatus(_buyer,1);
            CheckTests(responce);
        }
        public void StartTestFindStatusExpired()
        {
            InitializationSetup();
            _findContract = new VendorTests(_buyer);
            _findContract.FindByBuyerNumber(5);
            var responce = new DataBase("10.20.33.5", "paym_kayden", "dev-base", "Xe3nQx287");
            responce.GetContractsIdByStatus(_buyer,4);
            CheckTests(responce);
        }
        public void StartTestFindStatusCanceled()
        {
            InitializationSetup();
            _findContract = new VendorTests(_buyer);
            _findContract.FindByBuyerNumber(6);
            var responce = new DataBase("10.20.33.5", "paym_kayden", "dev-base", "Xe3nQx287");
            responce.GetContractsIdByStatus(_buyer,5);
            CheckTests(responce);
        }
        
        public void StartTests()
        {
            if (_check.GetItemChecked(0))
            {
                StartPositiveTest();
            }
            if (_check.GetItemChecked(1))
            {
                StartPositiveTestPhone();
            }
            
            if (_check.GetItemChecked(2))
            {
                StartTestFindStatusModerate();
            }
            
            if (_check.GetItemChecked(3))
            {
                StartTestFindStatusSuccess();
            }
            
            if (_check.GetItemChecked(4))
            {
                StartTestFindStatusExpired();
            }
            
            if (_check.GetItemChecked(5))
            {
                StartTestFindStatusCanceled();
            }
            
        }
        void InitializationSetup()
        {
            _setup = new VendorTests();_setup.Setup();
            _auth = new VendorTests(_login, _password);_auth.Auth();
            Thread.Sleep(2000);
        }
        void CheckTests(string actual, string expected)
        {
            if (_findContract.ActualExpected(actual, expected))
            {
                result.Add(true);
                MessageBox.Show("+");
            }
            else
            {
                result.Add(false);
                MessageBox.Show("-"+list[0]);
            }
        }

        void CheckTests(DataBase responce)
        {
            CreateIdList();
            if (responce.ids.Count == 0 && list.Count==0)
            {
                result.Add(true);
                MessageBox.Show("+");
                Form1.Driver.Quit();
                return;
            }

            if (responce.ids.SequenceEqual(list))
            {
                result.Add(true);
                MessageBox.Show("+");
                
            }
            else
            {
                result.Add(false);
                MessageBox.Show("-"+" "+responce.ids[0]+list[0]);
            }
            Form1.Driver.Quit();
            
        }

        //метод, создающий лист номеров с ID клиента из браузера
        void CreateIdList()
        {
            var byXpath = By.XPath("//div[@class='contract__top']//div[1]//H5");
            list = new List<string>();
            //проходим столько раз, сколько номеров показывает браузер
            if (Form1.Driver.FindElements(byXpath).Count==0)
            {
                //если в браузере нет номеров, то выходим из метода
                return;
            }
            for (int i =0; i < Form1.Driver.FindElements(byXpath).Count; i++)
            {
                Overloads test = new Overloads();
                test.Scroll(0, 500);
                var digitWord = "";
                //"чистим" слово от букв, оставляя только цифры
                foreach (var ch in Form1.Driver.FindElements(byXpath)[i].Text)
                {
                    if (Char.IsNumber(ch))
                    {
                        digitWord += ch;
                    }
                }
                //добавляем в лист номеров все, кроме пустых строк
                if (digitWord!="")
                {
                    list.Add(digitWord);
                }
            }
            
            
        }
    }
}