using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
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
        private Auth positiveAuth;
        private NewBuyerVendor positiveNewBuyer;
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
            positiveAuth = new Auth(fieldPositiveLoginVendor.Text, fieldPossitivePassVendor.Text, fieldInstanceVendor.Text,
                fieldNegativeLoginVendor.Text, fieldNegativePasswordVendor.Text, checkedListBoxAuthVendor);
            positiveAuth.StartTests();
            positiveNewBuyer = new NewBuyerVendor(fieldPositiveLoginVendor.Text, fieldPossitivePassVendor.Text,
                fieldInstanceVendor.Text, fieldBuyerNumberVendor.Text, fieldNegativeBuyerNumberVendor.Text,
                checkedListNewBuyerVendor,fieldCardNumberVendor.Text,fieldCardDateVindor.Text,openPhotoVendor.FileName);
            positiveNewBuyer.StartTests();
            label4.Text = positiveNewBuyer._card;
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
                return;
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
    }
}