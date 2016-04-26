using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace cart
{
	public partial class Page1 : ContentPage
    {

        public string qty = "0";
        public string total = "0";

        static List<product> products = new List<product>();
        static List<billentry> bill = new List<billentry>();

        public Page1()
        {
            InitializeComponent();
            //pk1 = this.FindByName<Picker>("pk1");

            products.Add(new product { name = "oil", unit = "ltr", price = 80 });
            products.Add(new product { name = "soap", unit = "pc", price = 30 });
            products.Add(new product { name = "maggi", unit = "pc", price = 10 });
            products.Add(new product { name = "sugar", unit = "kg", price = 200 });
            products.Add(new product { name = "salt", unit = "pc", price = 20 });
            products.Add(new product { name = "spice", unit = "pc", price = 60 });
            products.Add(new product { name = "toothpaste", unit = "pc", price = 30 });
            products.Add(new product { name = "flour", unit = "kg", price = 50 });
            foreach (var x in products)
            {
                pk1.Items.Add(x.name + " : " + x.price + "rs per " + x.unit);
            }
            pk1.SelectedIndex = 1;
        }
        void Plus(object sender, EventArgs e)
        {
            tqty.Text = (Int32.Parse(tqty.Text) + 1).ToString();
        }
        void Minus(object sender, EventArgs e)
        {
            tqty.Text = (Int32.Parse(tqty.Text) - 1).ToString();
        }

        async void NextPage1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page2());
        }
        void NewEntry(object sender, EventArgs e)
        {
            var res = bill.Find(x => (x.name == products[pk1.SelectedIndex].name));
            if (res == null)
            {
                bill.Add(new billentry
                {
                    name = products[pk1.SelectedIndex].name,
                    unit = products[pk1.SelectedIndex].unit,
                    price = products[pk1.SelectedIndex].price,
                    qty = Int32.Parse(tqty.Text),
                    total = Int32.Parse(tqty.Text) * products[pk1.SelectedIndex].price

                });
            }
            else
            {
                int idx = bill.FindIndex(x => (x.name == products[pk1.SelectedIndex].name));
                bill[idx].qty += Int32.Parse(tqty.Text);
                bill[idx].total = bill[idx].qty * bill[idx].price;
            }
            gv.ItemsSource = null;
            gv.ItemsSource = bill;
            Cal_Total();
            pker.IsVisible = false;
            bexpand.IsVisible = true;
        }
        async void OnItemTap(object sender, ItemTappedEventArgs e)
        {
            var x = e.Item as billentry;
            var ack = await DisplayAlert("Remove from List", "Remove "+x.name+"?", "Yes", "No");
            if (ack == true)
            {
                var listitem = (from itm in bill
                                where itm.name == x.name
                                select itm)
                            .FirstOrDefault<billentry>();
                bill.Remove(listitem);
                gv.ItemsSource = null;
                gv.ItemsSource = bill;
                Cal_Total();
            }
        }
        async void OnClick1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page2());
        }
        void OnClick2(object sender, EventArgs e)
        {
            ToolbarItem tbi = (ToolbarItem)sender;
            this.DisplayAlert("Selected!", "Kappa", "OK");
        }
        void Expand(object sender, EventArgs e)
        {
            pker.IsVisible = true;
            bexpand.IsVisible = false;
        }
        void Cal_Total()
        {
            int t = 0;
            foreach (var x in bill)
            {
                t += x.total;
            }
            ltotal.Text = t.ToString();
        }
        public void DeleteEntry(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button)sender;
            var listitem = (from itm in bill
                            where itm.name == item.CommandParameter.ToString()
                            select itm)
                            .FirstOrDefault<billentry>();
            bill.Remove(listitem);
            gv.ItemsSource = null;
            gv.ItemsSource = bill;
            Cal_Total();
        }
    }
    class product
    {
        public string name { get; set; }
        public string unit { get; set; }
        public int price { get; set; }


    }
    class billentry
    {
        public string name { get; set; }
        public string unit { get; set; }
        public int price { get; set; }
        public int qty { get; set; }
        public int total { get; set; }
    }
}
