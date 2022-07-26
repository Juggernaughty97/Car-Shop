using CarClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarShopGUI
{
    public partial class Form1 : Form
    {
        Store myStore = new Store();
        BindingSource carInventoryBindingSource = new BindingSource();
        BindingSource CartBindingSource = new BindingSource();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Car c = new Car(txt_brand.Text, txt_model.Text, decimal.Parse(txt_price.Text));
                myStore.CarList.Add(c);
                carInventoryBindingSource.ResetBindings(false);
            }

            catch(FormatException)
            {
                MessageBox.Show("Please insert a decimal value in price (use ',')", "ERROR");
            }
            

            txt_brand.Clear();
            txt_model.Clear();
            txt_price.Clear();


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            carInventoryBindingSource.DataSource = myStore.CarList;
            CartBindingSource.DataSource = myStore.ShoppingList;

            lst_inv.DataSource = carInventoryBindingSource;
            lst_inv.DisplayMember = ToString();

            lst_cart.DataSource = CartBindingSource;
            lst_cart.DisplayMember = ToString();
        }

        private void btn_add_cart_Click(object sender, EventArgs e)
        {
            //get the select item from inventory

            Car selected = (Car) lst_inv.SelectedItem;
            
            //add that item to the cart

            myStore.ShoppingList.Add(selected);

            //update list box control
            CartBindingSource.ResetBindings(false);

            
        }

        private void btn_checkout_Click(object sender, EventArgs e)
        {
            decimal total = myStore.Checkout();

            lbl_total.Text = "$" + total.ToString();

            CartBindingSource.ResetBindings(false);
        }
    }
}
