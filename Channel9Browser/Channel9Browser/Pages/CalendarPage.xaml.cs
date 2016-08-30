using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Channel9Browser.Pages
{
    public partial class CalendarPage : ContentPage
    {
        private DateTime selectedDate;

        public CalendarPage()
        {
            InitializeComponent();

            this.calendar.OnCalendarTapped += Calendar_OnCalendarTapped;
            this.BindingContext = App.viewModel;
            this.calendar.BindingContext = App.viewModel.VideoDates;
        }

        private void Calendar_OnCalendarTapped(object sender, CalendarTappedEventArgs args)
        {
            SfCalendar calendar = args.Calendar;
            selectedDate = args.datetime;

        }

        private async void GoButton_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new FilteredVideosPage(selectedDate));
        }
    }
}
