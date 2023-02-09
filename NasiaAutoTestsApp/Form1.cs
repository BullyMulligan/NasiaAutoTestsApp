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
        public static string Instance;
        public Form1()
        {
            InitializeComponent();
        }

        
        //переключатель позитивных тестов. Включает и выключает все позитивные тесты
        void SwitchPositiveTests()
        {
            if (checkPositiveVendor.Checked)
            {
                checkedListBoxAuthVendor.SetItemChecked(0,true);
            }
            else
            {
                checkedListBoxAuthVendor.SetItemChecked(0,false);
            }
        }
        //переключатель позитивных тестов. Включает и выключает все позитивные тесты
        void SwitchNegstiveTests()
        {
            if (checkNegativeVendor.Checked)
            {
                checkedListBoxAuthVendor.SetItemChecked(1,true);
                checkedListBoxAuthVendor.SetItemChecked(2,true);
                checkedListBoxAuthVendor.SetItemChecked(3,true);
                checkedListBoxAuthVendor.SetItemChecked(4,true);
            }
            else
            {
                checkedListBoxAuthVendor.SetItemChecked(1,false);
                checkedListBoxAuthVendor.SetItemChecked(2,false);
                checkedListBoxAuthVendor.SetItemChecked(3,false);
                checkedListBoxAuthVendor.SetItemChecked(4,false);
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
        }

        //включается при изменении чека позитивных тестов

        private void checkPositiveVendor_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = (CheckBox)sender;
            if (checkBox == checkPositiveVendor)
            {
                SwitchPositiveTests();
                return;
            }
            if (checkBox == checkNegativeVendor)
            {
                SwitchNegstiveTests();
            }
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
            if (checkedListBox == checkedListBoxAuthVendor)
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

        }
    }
}