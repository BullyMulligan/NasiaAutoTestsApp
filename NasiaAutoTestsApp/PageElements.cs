using System.Threading;
using OpenQA.Selenium;

namespace NasiaAutoTestsApp
{
    public class PagesElements
    {
        protected readonly By _fieldNewBuyerVendor = By.XPath("//section[@class='client-section']//input[@class='n-input__input-el']");//поле номера нового клиента
        protected readonly By _listButtonsSideBar = By.XPath("//aside[@class='sidebar']//ul//li");//сайдбар
        protected readonly By _fieldPhotoVendor = By.XPath("//input[@class='n-upload-file-input']");//поле вставки фото
        protected readonly By _messageErrorCardNotPhone = By.XPath("//div[@class='Vue-Toastification__toast Vue-Toastification__toast--error top-right']//div[@role='alert']");//сообщение о том, что карта не привязана к телефону
        protected readonly By _buttonNewBuyerVendor = By.XPath("//form[@class='client__phone']//button/span[text()='Получить СМС']");//кнопка создания нового клиента
        protected readonly By _buttonOtpNewBuyerVendor =By.XPath("//section[@class='client-section'][1]//button/span[text()='Подтвердить']");//кнопка отправить отп код
        protected readonly By _fieldCardNumberBuyerVendor = By.XPath("(//form[@class='client__card']//input[@class='n-input__input-el'])[1]");//поле карты клиента
        protected readonly By _fieldCardDataBuyerVendor = By.XPath("(//form[@class='client__card']//input[@class='n-input__input-el'])[2]");//поле даты карты клиента
        protected readonly By _buttonSavePhotosVendor =
            By.XPath("//div[@class='tab']//button/span[text()='Сохранить']");//кнопка сохранить фотографии
        protected readonly By _buttonCheckCardVendor =
            By.XPath("//section[@class='client-section block-card'][1]//button/span[text()='Получить СМС']");//кнопка, отправляющая данные карты на сервер

        protected readonly By _fieldOtpCardCode =
            By.XPath("//div[@class='row-wrap client__card-sms']//input[@class='n-input__input-el']");//поле ввода ОТП кода для проверки карты
        protected readonly By _buttonCheckCardOTPVendor =
            By.XPath("//section[@class='client-section block-card'][1]//button/span[text()='Подтвердить']");//кнопка, отправляющая данные карты на сервер
        
        protected readonly By _buttonSaveBuyerVendor = By.XPath("//div[@class='client__guarants-save']//button//span[text()='Сохранить']");//добавить покупателя
        protected readonly By _fieldName1 = By.Name("name_1");//поле имя доверителя 1
        protected readonly By _fieldName2 = By.Name("name_2");//поле имя доверителя 2
        protected readonly By _fieldGarantNumber1 = By.Name("guarantPhone-1");//поле ввода телефона доверителя 1
        protected readonly By _fieldGarantNumber2 = By.Name("guarantPhone-2");//поле ввода телефона доверителя 2


        protected readonly By _fieldVerifyBuyerVendor = By.XPath("//input[@class='n-input__input-el']");
        protected readonly By _buttonCheckVerifyVendor = By.XPath("//span[text()='Проверить пользователя']");


        protected readonly By _fieldNumberBuyerAddProduct = By.XPath("//section[@class='buyer']//div[@name='phone']//input[@class='n-input__input-el']");
        protected readonly By _buttonFindBuyerAddProduct = By.XPath("//button//span[text()='Найти покупателя']");
        protected readonly By _selectCategoryProduct = By.XPath("//div[@class='n-select dependence-select']");
        protected readonly By _selectElementCategory = By.XPath("//div[text()='Бытовая техника']");
        protected readonly By _selectCategoryCreatedFood = By.XPath("(//div[@class='n-select dependence-select'])[2]");
        protected readonly By _selectElementCreatedFood = By.XPath("//div[text()='Приготовление блюд']");
        protected readonly By _selectCategoryMicroVawe = By.XPath("(//div[@class='n-select dependence-select'])[3]");
        protected readonly By _selectElementMicroVawe = By.XPath("//div[text()='Микроволновая печь']");
        protected readonly By _selectCategoryDaewoo = By.XPath("(//div[@class='n-select dependence-select'])[4]");
        protected readonly By _selectElementDaewoo = By.XPath("(//div[@class='v-vl-visible-items'])[4]//div[1]");
        protected readonly By _selectCategoryModel = By.XPath("(//div[@class='n-select dependence-select'])[5]");
        protected readonly By _selectElementModel = By.XPath("(//div[@class='v-vl-visible-items'])[5]//div[1]");
        protected readonly By _fieldProductPrice = By.XPath("(//input[@class='n-input__input-el'])[3]");
        protected readonly By _buttonAddProduct = By.XPath("//span[text()='Добавить товар']");
        protected readonly By _listCalculate = By.XPath("//section[@class='calculate']//div[@class='n-base-selection-label']");
        protected readonly By _elementCalculate = By.XPath("//div[text()='9 месяц']");
        protected readonly By _buttonAddContract = By.XPath("//span[text()='Создать договор']");
        protected readonly By _buttonSaveContract = By.XPath("//span[text()='Сохранить']");
        
        
        
        protected readonly By _fieldFindByContractNumber = By.XPath("//input[@placeholder='Поиск по номер договора']");
        protected readonly By _fieldFindByPhoneNumber = By.XPath("//input[@placeholder='Поиск по номер телефона']");
        protected readonly By _tabListStatus = By.XPath("//div[@class='n-tabs-tab-wrapper']");//список вкладок со статусами контрактов
        
        public readonly By _exMessage = By.XPath("//span[@class='error-text']");
        protected readonly By _fieldOTPCode = By.XPath("//div[@class='row-wrap'][2]//input[@class='n-input__input-el']");
        protected readonly By _btnPrintClientAct = By.XPath("//a[@class='btn btn-orange mr-2']");
        private readonly By _photoClient = By.XPath("//div[@class='d-flex flex-row-reverse justify-content-between align-items-center custom-file']");
        private readonly By _act = By.XPath("//div[@class='d-flex flex-row-reverse justify-content-between align-items-center custom-file mb-3']");
        private readonly By _btnCancelContract = By.XPath("//button[@class='btn btn-red border-radius-8 w-100']");
        private readonly By _createContract = By.XPath("//a[@title='Договора']");
        protected readonly By _fieldIdVendor = By.XPath(" //input[@type='text']"); //поле ввода ID
        protected readonly By _fieldPasswordVendor = By.XPath(" //input[@type='password']");//поле ввода пароля
        protected readonly By _authButton = By.XPath("//button[@class='n-button n-button--primary-type n-button--medium-type']");//кнопка входа в кабинет вендор
        protected string _numberFoundContract = "4";
        protected readonly By _fieldFoundContract = By.XPath("//input[@type='text']");
        protected readonly By _btnFoundContract = By.XPath("//button[@type='submit']");
        protected readonly By _btnReasonCancelContract = By.XPath("//form[@enctype='multipart/form-data']//button[@type='submit']");
        protected readonly By _fieldReasonCancelContract = By.XPath("//form[@enctype='multipart/form-data']//textarea");
        protected readonly By _fieldSmsCode = By.XPath("//input[@id='cancelSmsCode']");
        protected readonly By _btnTotalCancelContract = By.XPath("//div/button[@class='btn btn-orange text-white modern-shadow mt-3 mx-auto w-100 px-5 py-3']");
        protected readonly By _btnBackCancelContract = By.XPath("//img[@src='https://lisa.paymart.uz/images/icons/icon_arrow_orange.svg']");
        protected readonly By _btnSmsCheckCancelContract = By.XPath("//div[@class='mx-auto']//button");
    
        protected readonly By _exСontractBuyer = By.XPath("//div[@style='word-break: break-word; width: 55%;']/span[@class='number']");
        protected readonly By _exReasonCancel = By.XPath("//h5[@class='reason__title']");
        protected readonly By _exReasonCancelAssert = By.XPath("//div[@class='font-weight-900 font-size-40 text w-75 text-center mx-auto']");
        protected readonly By _exSmsCodeCheck = By.XPath("//p[@class='text-red text-center mt-2']");
        protected readonly By _exInActive = By.XPath("//div[@class='order-status-container completed']");
        protected readonly By _exContracts = By.XPath("//div[@class='title']//h1"); 
        protected readonly By _listLeftMenu = By.XPath("//ul[@class='list-group list-group-flush mt-3']//a");//Левое меню
        protected readonly By _tableContract = By.XPath("//table[@class='products table-bordered']//tr//th");
        protected readonly By _orderStatusTab = By.XPath("//ul[@id='orderStatus']//li");
        protected readonly By _exNewContract = By.XPath("//div[@class='title']//h1");
        //New contract
        protected readonly By _fieldNumberBuyerNewContract = By.XPath("//div//input[@required='required']");
        protected readonly By _windowAccertNumber = By.XPath("//div//a[@class='dropdown-item']");
        protected readonly By _windowUserNotFound = By.XPath("//div[@class='dropdown-menu show user-info-dropdown']");
        protected readonly By _exNumberBuyer =By.XPath("//div[@class='font-weight-normal mb-2']");
        protected readonly By _btnActionCategory = By.XPath("//div[@bg-color='grey']");
        protected readonly By _selectProductCategory = By.XPath("//ul[@class='multiselect__content']/li");
        protected readonly By _fieldProducts = By.XPath("//input[@autocomplete='off']");
        protected readonly By _selectListNewContract = By.XPath("//select");
        protected readonly By _exContractIsCreated = By.XPath("//div[@class='mb-2']");
        protected readonly By _btnCreateContract = By.Id("submitOrder");
        protected readonly By _fieldImei = By.XPath("//input[@class='form-control modified  is-invalid']");
        protected readonly By _fieldInfoProduct = By.XPath("//div[@class='form-row align-items-center ']//input");
    
        //Cancel contract
        protected readonly By _btnContractCancel = By.XPath("//button[@class='btn btn-orange text-white modern-shadow mt-3 mx-auto w-100 px-5 py-3']");
        protected readonly By _exStatusContract = By.XPath("//div[@class='col-12 col-md-4 pr-0']//div");
   

        protected readonly By _listCatalogTitleRight = By.XPath("//div[@class='title-right']//a");
        protected readonly By _exFieldsErrors = By.XPath("//span[@class='validation-error']");
        protected readonly By _listItem = By.XPath("//div[@class='list']");
        protected readonly By _buttonListCategory = By.XPath("//div[@class='multiselect__select']");
        protected readonly By _listCategoryes = By.XPath("//div[@class='row align-items-end']");
        protected readonly By _listCategoryProduct = By.XPath("//div[@class='form-group col-12 col-sm-3 position-relative']");
        protected readonly By _listIntoSetCategory = By.XPath("//li[@class='multiselect__element']/span");
        protected readonly By _listInstruction = By.XPath("//div[@class='instruction']/a");
        protected readonly By _languageSwitch = By.XPath("//a[@class='dropdown-toggle']/img");

        protected readonly By _switchLanguage = By.XPath("//div[@class='dropdown-menu dropdown-menu-right show']/a/img");

        //Debugger
        protected readonly By _btnDebuggerMaximize = By.XPath("//a[@class='phpdebugbar-maximize-btn']");
        protected readonly By _btnDebuggerMinimize = By.XPath("//a[@class='phpdebugbar-minimize-btn']");
        protected readonly By _listDebuggerMessages = By.XPath("//div[@class='phpdebugbar-panel phpdebugbar-active']//ul//li");
        //NewBuyer
        protected readonly By _fieldNewBuyer = By.Id("inputPhone");
        protected readonly By _fieldSmsCodeNewBuyer = By.Id("phoneInputSMSCode");
        protected readonly By _rowNewBuyer = By.XPath("//div[@id = 'newBuyer']//div[@class= 'form-row']");
        protected readonly By _btnSendSms = By.XPath("//button[@class='btn btn-orange']");
        protected readonly By _fieldCardBuyer = By.Id("inputCardNumber");
        protected readonly By _fieldCardDate = By.Id("inputCardExp");
        protected readonly By _fieldSmsCheck = By.Id("sms-code-input");
        protected readonly By _attachPassportSelfie = By.Id("passport_selfie");
        protected readonly By _attachPassportFirstPage = By.Id("passport_first_page");
        protected readonly By _attachPassportAdressPage = By.Id("passport_with_address");
        protected readonly By _btnSavePhotos = By.XPath("//div[@class='form-controls']//button");
        protected readonly By _fieldContactFaceOne =By.Id("name");
        protected readonly By _fieldContactFaceNumber = By.Id("phone");
        protected readonly By _btnAddContact = By.XPath("//div[@class='form-controls mt-0 pt-0']//button");
        protected readonly By _exNewBuyerIsReg = By.XPath("//div[@class='alert alert-info']");
        protected readonly By _exWaitingForModerate = By.XPath("//div[@class='alert alert-info']");
        protected readonly By _exClientIsReg = By.XPath("//div[@class='font-weight-900 font-size-40 text']");
        protected readonly By _exVerify = By.XPath("//div[@class='value text-orange']");
    
    
    
        //Admin Side
        //Auth Page
        protected readonly By _btnAuthAdmin = By.XPath("//button[@class='btn btn-orange btn-block']");
        protected readonly By _fieldIdAuth = By.Id("inputPhone");
        protected readonly By _fieldPasswordAuth = By.Id("inputPassword");
        protected readonly By _expErrorEmtyNumber = By.XPath("//div[@class='error']");

        //Employees Page
        protected readonly By _expEmployees = By.XPath("//div[@class='d-flex flex-column']/h1");
    
        //Sidebar
        protected readonly By _btnSideBarOpen = By.Id("sidebar-toggle");
        protected readonly By _listSideBarMenu = By.XPath("//div[@class='menu']/ul/ul/li");
    
        //Clients Page
        protected readonly By _fieldSearchClient = By.XPath("//input[@type='search']");
        protected readonly By _btnSearchClient = By.XPath("//button[@class='btn btn-success btn-search']");
        protected readonly By _listScoringCient = By.XPath("//div[@class='scoring_katm']/div/input");
        protected readonly By _btnModerate = By.XPath("//button[@class='btn btn-outline-primary']");
        protected readonly By _btnAddCard = By.XPath("//button[@class='btn btn-light']");
        protected readonly By _fieldCardNumber = By.XPath("//input[@placeholder='8600 0000 0000 0000']");
        protected readonly By _fieldCadrdDate = By.XPath("//input[@placeholder='00/00']");
        protected readonly By _btnSendSmsScoring = By.XPath("//button[@class='btn btn-primary']");
        protected readonly By _fieldConfirmCode =By.Id("confirm-code");
        protected readonly By _btnCheckCode = By.XPath("//button[@class='btn btn-primary']");
        protected readonly By _btnScoring =By.XPath("//button[@style='cursor: pointer;']");
        protected readonly By _exStatusScoring = By.XPath("//td[@style='text-align: left;']");
        protected readonly By _exScoringIsPassed = By.XPath("//table[@class='table']//th");
        protected readonly By _exClientTab = By.XPath("//td[@valign='top']");
    
        //Buyers Page
    
        protected readonly By _exAuthBuyer = By.XPath("//h1");
        protected readonly By _buyerSideBar = By.XPath("//div[@class='sidebar_content']/ul/li");
        protected readonly By _btnPublicOfert = By.XPath("//li//a[@target='_blank']");
        protected readonly By _listContracts = By.XPath("//div[@class='row']//div[@class='title']/h4");
        protected readonly By _switchBuyerLang = By.XPath("//li[@class='icon_link lang']/a");
    
        //MySoliq Page
        protected readonly By _buttonLanguageRu = By.XPath("//a[@class='rus ']");
        protected readonly By _buttonEnterCabinet = By.XPath("//a[@class='show_auth_modal_legal_entity_button']");
        protected readonly By _buttonsEsiOrUsb = By.XPath("//a[@class='thumbnail text-center dl-link']");
        protected readonly By _buttonsSignIn = By.XPath("//a[@class='btn btn-default sign-in']");
        protected readonly By _fieldcheckNumber = By.Id("checkNumber");//
        protected readonly By _buttonFindCheck = By.Id("findButton");//
        protected readonly By _fieldVat = By.XPath("//input[@name='vat']");//
        protected readonly By _arrowIkpu = By.XPath("//span[@class='select2-selection__arrow']");//
        protected readonly By _fieldIkpu = By.XPath("//input[@class='select2-search__field']");//
        protected readonly By _labelSelectIkpu = By.XPath("//li[@role='option']");//
        protected readonly By _buttonsUnitInn = By.XPath("//button[@class='btn dropdown-toggle bs-placeholder btn-default']");
        protected readonly By _labelUnit = By.XPath("//span[@class='text']");
        protected readonly By _loadPdf = By.Id("zipFile");
        protected readonly By _buttonCalculate = By.Id("btnCalc");
        protected readonly By _fieldInn = By.XPath("//input[@type='text']");
        protected readonly By _buttonSend = By.Id("btnSend");
        protected readonly By _fieldVatTotal=By.XPath("//input[@name='sumVatTotal']");
        protected readonly By _buttonsSelect = By.XPath("//button[@title='Выберите']");

    }
}