using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Channel9Browser.Model
{
    public class Item : INotifyPropertyChanged
    {
        private string title;
        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        private string link;
        public string Link
        {
            get
            {
                return link;
            }

            set
            {
                link = value;
                OnPropertyChanged(nameof(Link));
            }
        }

        private string author;
        public string Author
        {
            get
            {
                return author;
            }

            set
            {
                author = value;
                OnPropertyChanged(nameof(Author));
            }
        }

        private DateTime pubDate;
        public DateTime PubDate
        {
            get
            {
                return pubDate;
            }

            set
            {
                pubDate = value;
                OnPropertyChanged(nameof(PubDate));
            }
        }

        private string thumbnail;
        public string Thumbnail
        {
            get { return thumbnail; }
            set
            {
                thumbnail = value;
                OnPropertyChanged(nameof(Thumbnail));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }
    }
}
