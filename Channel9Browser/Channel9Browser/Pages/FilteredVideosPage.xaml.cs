using Channel9Browser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Channel9Browser.Pages
{
    public partial class FilteredVideosPage : ContentPage
    {
        private DateTime filterDate;

        public FilteredVideosPage(DateTime filterDate)
        {
            InitializeComponent();

            switch (Device.Idiom)
            {
                case TargetIdiom.Desktop:
                    this.RssView.RowHeight = 288;
                    break;
                default:
                    this.RssView.RowHeight = 123;
                    break;
            }

            this.filterDate = filterDate;
            App.viewModel.FilterByDate(filterDate);

            this.BindingContext = App.viewModel;
        }

        private async void RssView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selected = e.Item as Item;

            if (selected != null)
                await App.Current.MainPage.Navigation.PushAsync(new WebContentPage(selected.Link));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.RssView.ItemsSource = App.viewModel.FilteredItems;
        }
    }
}
