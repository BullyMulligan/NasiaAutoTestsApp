using System;
using System.Threading;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;
using OpenQA.Selenium.Chrome;


namespace NasiaAutoTestsApp
{
    //в данном классе запускаются тесты селениум. Тесты модульные, то есть, чтобы запустить создание нового клиента,
    //необходимо запустить преднастройку драйвера, затем пройти авторизацию, а затем сам тест
    public class VendorTests:PagesElements
    {
        private DataBase _responce;
        private string _login;
        private string _password;
        private string _buyer;
        private Overloads _test= new Overloads();
        private string _instance = Form1.Instance;
        public string _otpCode;
        private string _card;
        private string _date;
        private string _photo;
        private bool _equal;
        private int _contract;
        public bool result;
        public Screenshot screenshot;
        private NewBuyerVendor.PasportData _pasportData;

        public VendorTests()
        {
        }
        public VendorTests(string login,string password)
        {
            _login = login;
            _password = password;
        }
        
        public VendorTests(string buyer)
        {
            _buyer=buyer;
        }
        public VendorTests(string buyer,int contract)
        {
            _buyer=buyer;
            _contract=contract;
        }
        public VendorTests(string buyer,string otpCode,string card,string date,string photo)
        {
            _buyer = buyer;
            _card = card;
            _date = date;
            _otpCode = otpCode;
            _photo = photo;
        }
        public VendorTests(string buyer,string card,string date,string photo,NewBuyerVendor.PasportData pasportData)
        {
            _buyer = buyer;
            _card = card;
            _date = date;
            _photo = photo;
            _pasportData = pasportData;
        }
        public VendorTests(string buyer,string card,string date,string photo,bool equal)
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

        public VendorTests(string login,string password,string buyer,string card,string date,string photo)
        {
            _login = login;
            _password = password;
            _buyer = buyer;
            _card = card;
            _date = date;
            _photo = photo;
        }

        //метод преднастройки тестов. Открывает браузер, настраивает таймспаны
        public void Setup()
        {
            //настраиваем фейковую камеру
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--use-fake-device-for-media-stream");
            options.AddArgument("--use-fake-ui-for-media-stream");
            options.AddArgument($"--use-file-for-fake-video-capture={AppDomain.CurrentDomain.BaseDirectory}/stream.mjpeg");
            
            Form1.Driver = new OpenQA.Selenium.Chrome.ChromeDriver(options);//открываем Хром
            Form1.Driver.Navigate().GoToUrl($"https://{_instance}.uzumnasiya.uz/ru");//вводим адрес сайта
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
        public void NewBuyer(int status,int limit)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Form1.Driver, TimeSpan.FromSeconds(10));
                //удаляем пользователя из базы
                _responce = new DataBase("10.20.33.5", "paym_tera", "dev-base", "Xe3nQx287");
                _responce.DeleteUser(_buyer);
                //нажимаем на кнопку сайдабара(3), вводим номер покупателя и жмем кнопку "отправить ОТП-код"
                _test.Click(_listButtonsSideBar,3);
                _test.Click(_buttonNextStep);
                
                //переходим на фрейм
                IWebElement iframeElement = Form1.Driver.FindElement(By.XPath("//iframe[@allow='camera;']"));
                Form1.Driver.SwitchTo().Frame(iframeElement);

                _test.JSClick(_fieldNewBuyerVendor);
                
                
                _test.SendKeys(_fieldNewBuyerVendor,_buyer);
                
                //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(_buttonNewBuyerVendor));
                _test.Click(_checkboxAgree);
                _test.Click(_buttonNewBuyerVendor);
                
                //если мы не передаем конкретный отп в тест, то берем его из базы
                if (_otpCode==null)
                {
                    _responce = new DataBase("10.20.33.5", "paym_tera", "dev-base", "Xe3nQx287");
                    _responce.request = "select code from otp_enter_code_attempts WHERE phone = 998"+_buyer;
                    Thread.Sleep(500);
                    _responce.GetOTP();
                    _otpCode = _responce.code;
                    Thread.Sleep(1000);
                }
                //вводим цифры отп-кода в раздельные поля
                for (int i = 0; i < _otpCode.Length; i++)
                {
                    Form1.Driver.FindElements(_fieldOtp)[i].SendKeys(_otpCode[i].ToString());
                }
                if(Form1.Driver.FindElements(By.XPath("//div[@class='Vue-Toastification__container top-right']//div[@role='alert']")).Count>0){return;}
                
                //заполняем паспортные данные
                _test.SendKeys(_fieldID,_pasportData._id);
                _test.SendKeys(_fieldSerial,_pasportData._serial);
                _test.SendKeys(_fieldBirthday,_pasportData._birthday);
                
                _test.Click(_buttonNextPassport);
                
                //Далее фоткаемся на фейковую вебкамеру
                _test.Click(_buttonCreatePhoto);
                _test.Click(_buttonWebCam);
                _test.Click(_buttonWebCamNext);
                
                //ждем, пока не появятся поздравления об одобрении кредита и жмем "увеличить кредит"
                while (Form1.Driver.FindElements(By.XPath("//h1")).Count!=1) {}
                Thread.Sleep(1000);
                _test.Click(_buttonBoostCredit);
                
                //заполняем карту и жмем "продолжить"
                
                _test.SendKeys(_fieldCard,_card);
                _test.SendKeys(_fieldCardDate,_date);
                _test.Click(_checkboxAgree);
                _test.Click(_buttonAddCardNext);
                
                if(Form1.Driver.FindElements(By.XPath("//div[@class='Vue-Toastification__container top-right']//div[@role='alert']")).Count>0){return;}
                
                //*****************************************************************************************//
                
                
                //берем отп из базы, он должен отличаться от предыдущего кода
                while (_responce.code==_otpCode)
                {
                    _responce.request = "select code from otp_enter_code_attempts WHERE phone = 998"+_buyer;
                    Thread.Sleep(500);
                    _responce.GetOTP();
                }
                _otpCode = _responce.code;

                //вводим цифры отп-кода в раздельные поля
                for (int i = 0; i < _otpCode.Length; i++)
                {
                    Form1.Driver.FindElements(_fieldOtp)[i].SendKeys(_otpCode[i].ToString());
                }
                
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
                _test.Click(_buttonSaveGuarants);

                //ждем, пока не появится сообщении об обработке заявки
                while (Form1.Driver.FindElements(By.XPath("//h3[@class='clock__title title']")).Count!=1){}
                
                //меняем статус клиента, задаем лимит и жмем кнопку "обновить статус"
                DataBase setStatus = new DataBase("10.20.33.5", "paym_tera", "dev-base", "Xe3nQx287");
                setStatus.SetStatusBuyer(_buyer,status);
                setStatus.SetLimitBuyer(_buyer,limit);
                _test.Click(_buttonUpdateStatus);
            }
            catch (Exception e){}
        }

        public void BuyerStatus()
        {
            Thread.Sleep(500);
            Form1.Driver.Navigate().GoToUrl($"https://{Form1.Instance}.paymart.uz/client/status");
            _test.SendKeys(_fieldVerifyBuyerVendor, _buyer);
            _test.Click(_buttonCheckVerifyVendor);
            Thread.Sleep(1000);
        }

        public void NewProduct()
        {
            try
            {
                Thread.Sleep(500);
                _test.SendKeys(_fieldNumberBuyerAddProduct,_buyer);
                _test.Click(_buttonFindBuyerAddProduct);
                _test.Click(_selectCategoryProduct);
                _test.Click(_selectElementCategory);
                _test.Click(_selectCategoryCreatedFood);
                _test.Click(_selectElementCreatedFood);
                _test.Click(_selectCategoryMicroVawe);
                _test.Click(_selectElementMicroVawe);
                _test.Click(_selectCategoryDaewoo);
                _test.AClick(_selectElementDaewoo);
                _test.Click(_selectCategoryModel);
                _test.AClick(_selectElementModel);
            
                Thread.Sleep(1000);
                _test.SendKeys(_fieldProductPrice,"10000");
                _test.Scroll(1000,1000);
                _test.Click(_listCalculate);
                Thread.Sleep(500);
                _test.AClick(_elementCalculate);
                _test.Click(_buttonAddContract);
                _test.Click(_buttonSaveContract);
            }
            catch (Exception e){}
        }

        public void FindByContractNumber()
        {
            Form1.Driver.Navigate().GoToUrl($"https://{Form1.Instance}.paymart.uz/contracts");
            _responce = new DataBase("10.20.33.5", "paym_kayden", "dev-base", "Xe3nQx287");
            _responce.GetContractId(_buyer);
            var contract = _responce.code;
            _test.SendKeys(_fieldFindByContractNumber,contract);
        }

        public void FindByBuyerNumber(int tab)
        {
            Form1.Driver.Navigate().GoToUrl($"https://{Form1.Instance}.paymart.uz/contracts");
            
            _test.SendKeys(_fieldFindByPhoneNumber,"+998"+_buyer);
            _test.Click(_tabListStatus,1);
            Thread.Sleep(1000);
            _test.Click(_tabListStatus,tab);
            while(Form1.Driver.FindElements(By.XPath("(//p)[2]")).Count==0){}
            
            
        }
        public bool ActualExpected(string actual,string expected)
        {
            if (expected.Replace(" ","")==Form1.Driver.FindElement(By.XPath(actual)).Text.Replace(" ",""))
            {
                return true;
            }

            MessageBox.Show(actual);
            return false;
        }
        private void Exit()
        {
            Form1.Driver.Quit();
        }

        public void CreateFolder(string name,string parentPath)
        {
            
            string folderName = name;
            string path = Path.Combine(parentPath,folderName);
            
            //проверяем наличие папки, если ее не существует, то создаем ее
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            Screenshoot(path);
            Exit();
        }

        private void Screenshoot(string path)
        {
            screenshot = ((ITakesScreenshot)Form1.Driver).GetScreenshot();
            string now = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            screenshot.SaveAsFile($"{path}\\{now}.jpeg");
        }

        private void FakeWebcam()
        {
            var videoCapture = new VideoCapture(@"C:\Users\ipopov\RiderProjects\NasiaAutoTestsApp\NasiaAutoTestsApp\photo.jpg");
            MessageBox.Show("+");
            var image = videoCapture.QueryFrame().ToImage<Bgr, byte>();
            var options = new ChromeOptions();
            options.AddArgument("--use-fake-ui-for-media-stream");

            var driver = new ChromeDriver(options);


            var webCamElement = driver.FindElement(By.XPath("//div[@class='camera__preview']"));
            var base64Image = Convert.ToBase64String(image.ToJpegData());
            ((IJavaScriptExecutor)driver).ExecuteScript($"arguments[0].src = 'data:image/jpeg;base64,{base64Image}'", webCamElement);
        }

        public void TestWebcam()
        {

            // Инициализируем драйвер Chrome
            IWebDriver driver = new ChromeDriver();
            
            // Создаем объект, содержащий настройки для эмуляции медиаустройств
            ChromeOptions options = new ChromeOptions();
            
            options.AddArgument("--use-fake-device-for-media-stream");
            options.AddArgument("--use-fake-ui-for-media-stream");
            options.AddArgument("--use-file-for-fake-video-capture=C:/Users/ipopov/RiderProjects/NasiaAutoTestsApp/NasiaAutoTestsApp/bin/Debug/stream.mjpeg");
            

            // Запускаем драйвер с заданными настройками
            driver = new ChromeDriver(options);

            // Открываем сайт для тестирования функционала вебкамеры
            driver.Navigate().GoToUrl("https://www.onlinemictest.com/webcam-test/");
        }
    }
}