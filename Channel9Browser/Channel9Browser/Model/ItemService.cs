using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;

namespace Channel9Browser.Model
{
    public static class ItemService
    {
        public static string FeedUri = "https://channel9.msdn.com/Blogs/MVP-VisualStudio-Dev/RSS";
        private static XNamespace mediaNS = XNamespace.Get("http://search.yahoo.com/mrss/");

        private static string GetThumbnail(XElement node)
        {
            var images = node.Descendants(mediaNS + "thumbnail");

            string imageUrl;

            switch(Device.Idiom)
            {
                case TargetIdiom.Phone:
                    imageUrl = images.FirstOrDefault().Attribute("url").Value;
                    break;
                default:
                    if (images.Count() > 1)
                    {
                        imageUrl = images.Skip(1).FirstOrDefault().Attribute("url").Value;
                    }
                    else
                    {
                        imageUrl = images.FirstOrDefault().Attribute("url").Value;
                    }
                    break;
             }
            return imageUrl;
        }

        // Query the RSS feed with LINQ and return an IEnumerable of Item
        public static async Task<IEnumerable<Item>> QueryRssAsync(CancellationToken token)
        {
            try
            {
                var client = new HttpClient();

                var data = await client.GetAsync(new Uri(FeedUri), token);
                var actualData = await data.Content.ReadAsStringAsync();

                var doc = XDocument.Parse(actualData);
                //var mediaNS = XNamespace.Get("http://search.yahoo.com/mrss/");
                var dcNS = XNamespace.Get("http://purl.org/dc/elements/1.1/");

                var query = (from video in doc.Descendants("item")
                             select new Item
                             {
                                 Title = video.Element("title").Value,
                                 Author = video.Element(dcNS + "creator").Value,
                                 Link = video.Element("link").Value,
                                 Thumbnail = GetThumbnail(video), //video.Descendants(mediaNS + "thumbnail").LastOrDefault().Attribute("url").Value,
                                 PubDate = DateTime.Parse(video.Element("pubDate").Value,
                                     System.Globalization.CultureInfo.InvariantCulture)
                             });

                return query;
            }
            catch (OperationCanceledException)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
