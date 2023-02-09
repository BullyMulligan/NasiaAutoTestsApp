using System.Collections.Generic;
using System.Windows.Forms;

namespace NasiaAutoTestsApp
{
    public class FindContract
    {
        private string _buyerNumber;
        public List<bool> result = new List<bool>();
        private VendorTests _setup;
        private VendorTests _auth;
        private VendorTests _NewBuyerVendor;
        private VendorTests _BuyerStatus;
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

    }
}