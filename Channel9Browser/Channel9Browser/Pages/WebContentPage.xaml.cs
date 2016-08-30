using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Channel9Browser.Pages
{
    public partial class WebContentPage : ContentPage
    {
        string link;

        public WebContentPage(string link)
        {
            InitializeComponent();

            this.link = link;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            WebView1.Source = link;
        }
    }
}
