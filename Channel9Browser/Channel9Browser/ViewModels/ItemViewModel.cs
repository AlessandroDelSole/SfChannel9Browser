using Channel9Browser.Model;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Channel9Browser.ViewModels
{
    public class ItemViewModel : INotifyPropertyChanged
    {

        private bool isBusy;
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Item> Items { get; set; }

        public ObservableCollection<Item> FilteredItems { get; set; }

        public CalendarEventCollection VideoDates { get; set; }

        public ItemViewModel()
        {
            this.Items = new ObservableCollection<Item>();
            this.VideoDates = new CalendarEventCollection();
        }

        public void FilterByDate(DateTime date)
        {
            this.FilteredItems = new ObservableCollection<Item>(this.Items.Where(i => i.PubDate.Date == date.Date));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string name = "") =>
               PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public async Task InitializeAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var result = await ItemService.QueryRssAsync(new System.Threading.CancellationToken());
                if (result != null)
                {
                    this.Items = null;
                    this.Items = new ObservableCollection<Item>(result);
                    this.VideoDates.Clear();

                    foreach (var item in this.Items)
                    {
                        var videoEvent = new CalendarInlineEvent();
                        videoEvent.StartTime = item.PubDate;
                        videoEvent.EndTime = item.PubDate;
                        videoEvent.Subject = item.Title;
                        videoEvent.Color = Color.Maroon;

                        this.VideoDates.Add(videoEvent);
                    }
                }
            }
            catch (Exception)
            {

                return;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
