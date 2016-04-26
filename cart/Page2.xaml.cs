using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace cart
{
	public partial class Page2 : ContentPage
    {
        public string pname = "Arteezy", punit = "ULTRA", pprice = "322";
        public Page2()
        {
            InitializeComponent();
        }
        public void NewProduct(object sender, EventArgs e)
        {
            string pr = String.Format("{0} : rs{1} per {2}.", tname.Text, tprice.Text, tunit.Text);

            DisplayAlert("success!", "new product added : \n" + pr, "ok");
        }
    }
}
