using Channel9Browser.Model;
using Channel9Browser.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Channel9Browser
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = App.viewModel;

            switch(Device.Idiom)
            {
                case TargetIdiom.Desktop:
                    this.RssView.RowHeight = 288;
                    break;
                default:
                    this.RssView.RowHeight = 123;
                    break;
            }
        }

        private async Task LoadDataAsync()
        {
            this.busyindicator.IsVisible = true;
            this.busyindicator.IsBusy = true;

            await App.viewModel.InitializeAsync();
            this.RssView.ItemsSource = App.viewModel.Items;

            this.busyindicator.IsVisible = false;
            this.busyindicator.IsBusy = false;
        }

        private async void RssView_Refreshing(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await LoadDataAsync();

        }

        private async void RssView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selected = e.Item as Item;

            if (selected != null)
                await App.Current.MainPage.Navigation.PushAsync(new WebContentPage(selected.Link));
        }

        private void FilterButton_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new CalendarPage());
        }
    }
}
