using DemoLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormUI
{
    public partial class Dashboard : Form
    {
        ShoppingCartModel cart = new ShoppingCartModel();

        public Dashboard()
        {
            InitializeComponent();
            PopulateCartWithDemoData();
        }

        private void PopulateCartWithDemoData()
        {
            cart.Items.Add(new ProductModel { ItemName = "Cereal", Price = 3.63M });
            cart.Items.Add(new ProductModel { ItemName = "Milk", Price = 2.95M });
            cart.Items.Add(new ProductModel { ItemName = "Strawberries", Price = 7.51M });
            cart.Items.Add(new ProductModel { ItemName = "Blueberries", Price = 8.84M });
        }

        private void messageBoxDemoButton_Click(object sender, EventArgs e)
        {
            //we pass those three methods as delegates basically
            //we with help of delegates are passing three methods in diffrent UI(DemoLibrary) class library
            //these mathods are available there with help of delegates
            decimal total = cart.GenerateTotal(SubtotalAlert, CalculateLeveledDiscount, PrintOutDiscountAlert);
            MessageBox.Show($"The total is {total:C2}");
        }

        private void textBoxDemoButton_Click(object sender, EventArgs e)
        {
            //pass three methods as delegates but this time inline. so bascailly another UI DemoLibrary project can access these
            //when these three inline methods will be called from ShippingCartModel.GenerateTotal method it will pass needed values here
            // life "subTotal","message", "products"
            decimal total = cart.GenerateTotal(
                (subtotal) => subTotalTextBox.Text = $"{subtotal:C2}",
                (products, subtotal) => subtotal - (products.Count * 2),
                (message) => { });
            totalTextBox.Text = $"{total:C2}";
        }
        //same methods as we were using in Console app. just instead of Console.WriteLine will be MessageBox.Show
        private void PrintOutDiscountAlert(string message)
        {
            MessageBox.Show(message);
        }
        private void SubtotalAlert(decimal subTotal)
        {
            MessageBox.Show($"The subtotal is {subTotal:C2}");
        }
        //same (delegate method as was in console app. That second (delegate) method that we will pass to 
        //GenerateTotal method. this is Func<List<ProductModel>,decimal,decimal>. passing list and decimal which will be 
        //subTotal. and that returns decimal
        private decimal CalculateLeveledDiscount(List<ProductModel> products, decimal subTotal)
        {
            //thats total with discount. to subtotal minus how much items you have. pvz 20 - 2 = 20
            return subTotal - products.Count;
        }
    }
}
