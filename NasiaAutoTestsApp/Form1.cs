using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using OpenQA.Selenium;

namespace NasiaAutoTestsApp
{
    public partial class Form1 : Form
    {
        public static IWebDriver Driver;
        private Log _log;
        private Auth _positiveAuth;
        private NewBuyerVendor _positiveNewBuyer;
        private BuyerStatus _positiveBuyerStatus;
        private NewProduct _newProduct;
        private FindContract _findContract;
        public static string Instance;
        public string checkStatus;
        public Form1()
        {
            InitializeComponent();
            CalculationStatusTests();
        }

        
        //переключатель позитивных тестов. Включает и выключает все позитивные тесты
        
        //переключатель позитивных тестов. Включает и выключает все позитивные тесты
        void SwitchTests()
        {
            List<CheckedListBox> list = new List<CheckedListBox>();
            list.Add(checkedListBoxAuthVendor);
            list.Add(checkedListNewBuyerVendor);
            list.Add(checkedListClientStatus);
            list.Add(checkedListFindContractVendor);
            list.Add(checkedListNewProduct);
            if (checkNegativeVendor.Checked)
            {
                OnAllCheckBoxes(list);
            }
            else
            {
                OffAllCheckBoxes(list);
            }
        }
        

        private void buttonStartVendorClick(object sender, EventArgs e)
        {
            InstanctiateStaticValue();
            _positiveAuth = new Auth(fieldPositiveLoginVendor.Text, fieldPossitivePassVendor.Text, 
                fieldNegativeLoginVendor.Text, fieldNegativePasswordVendor.Text, checkedListBoxAuthVendor);
            _positiveAuth.StartTests();
            ChangeColorChechBox(checkedListBoxAuthVendor,_positiveAuth.result);
            
            _positiveNewBuyer = new NewBuyerVendor(fieldPositiveLoginVendor.Text, fieldPossitivePassVendor.Text,
                fieldBuyerNumberVendor.Text, fieldNegativeBuyerNumberVendor.Text,
                checkedListNewBuyerVendor,fieldCardNumberVendor.Text,fieldCardDateVindor.Text,openPhotoVendor.FileName);
            _positiveNewBuyer.StartTests();
            ChangeColorChechBox(checkedListNewBuyerVendor, _positiveNewBuyer.result);

            _positiveBuyerStatus = new BuyerStatus(fieldPositiveLoginVendor.Text, fieldPossitivePassVendor.Text,fieldBuyerNumberVendor.Text,checkedListClientStatus,fieldCardNumberVendor.Text,fieldCardDateVindor.Text,openPhotoVendor.FileName);
            _positiveBuyerStatus.StartTests();
            ChangeColorChechBox(checkedListClientStatus,_positiveBuyerStatus.result);

            _newProduct = new NewProduct(fieldPositiveLoginVendor.Text, fieldPossitivePassVendor.Text, fieldBuyerNumberVendor.Text, checkedListNewProduct, fieldCardNumberVendor.Text, fieldCardDateVindor.Text, openPhotoVendor.FileName);
            _newProduct.StartTests();
            ChangeColorChechBox(checkedListNewProduct,_newProduct.result);

            //данный набор тестов не работает корректно, так как в сортировке существует баг
            _findContract = new FindContract(fieldPositiveLoginVendor.Text, fieldPossitivePassVendor.Text, fieldBuyerNumberVendor.Text, checkedListFindContractVendor, fieldCardNumberVendor.Text, fieldCardDateVindor.Text, openPhotoVendor.FileName);
            _findContract.StartTests();
            ChangeColorChechBox(checkedListFindContractVendor,_findContract.result);
        }

        //включается при изменении чека позитивных тестов

        private void checkPositiveVendor_CheckedChanged(object sender, EventArgs e)
        {
            SwitchTests();
            CalculationStatusTests();
            labelStatusTests.Text = checkStatus;
        }


        private void buttonOpenPhotoVendor_Click(object sender, EventArgs e)
        {
            if (openPhotoVendor.ShowDialog() == DialogResult.OK)
            {
                buttonStartTestsVendor.Visible = true;
            }
            else
            {
                MessageBox.Show("Выберите фото");
            }
        }
        //назначаем статические значения, которые будут браться один раз за тест из одного места
        public void InstanctiateStaticValue()
        {
            Instance = fieldInstanceVendor.Text;
            //поле инстанс и прочие значения, которые останутся едиными для всех тестов
        }
        //метод меняет цвет цеклиста, в зависимости от процента пройденных успешно тестов
        private void ChangeColorChechBox(CheckedListBox checkedListBox,List<bool> result)
        {
            var positiveResult = result.Where(a =>  a == true).ToList();
            if (result.Count==0)
            {
                return;
            }
            if (result.Count == positiveResult.Count)
            {
                checkedListBox.BackColor = Color.Aquamarine;
                return;
            }
            checkedListBox.BackColor = Color.Salmon;
        }
        //метод, включающий все чекбоксы. Передаем лист из всех чек-боксов
        private void OnAllCheckBoxes(List<CheckedListBox> list)
        {
            for (var i = 0; i < list.Count;i++)
            {
                for (int j = 0; j < list[i].Items.Count; j++)
                {
                    list[i].SetItemChecked(j,true);
                }
            }
        }
        private void OffAllCheckBoxes(List<CheckedListBox> list)
        {
            for (var i = 0; i < list.Count;i++)
            {
                for (int j = 0; j < list[i].Items.Count; j++)
                {
                    list[i].SetItemChecked(j,false);
                }
            }
        }

        private void CalculationStatusTests()
        {
            List<CheckedListBox> list = new List<CheckedListBox>();
            list.Add(checkedListBoxAuthVendor);
            list.Add(checkedListNewBuyerVendor);
            list.Add(checkedListClientStatus);
            list.Add(checkedListFindContractVendor);
            list.Add(checkedListNewProduct);
            int allTests = 0;
            int checkedTest = 0;
            for (int i = 0; i < list.Count; i++)
            {
                allTests += list[i].Items.Count;
                for (int j = 0; j < list[i].Items.Count; j++)
                {
                    if (list[i].GetItemChecked(j))
                    {
                        checkedTest++;
                    }
                }
            }
            checkStatus = $"Всего тестов:{allTests}\nВыбрано тестов:{checkedTest}";
        }
        
        //меняем статус выбранных тестов при смене состояния чека
        private void checkedListFindContractVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculationStatusTests();
            labelStatusTests.Text = checkStatus;
        }


        //при выборе элемента чек-бокса отображать скриншот
        private void checkedListBoxAuthVendor_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}