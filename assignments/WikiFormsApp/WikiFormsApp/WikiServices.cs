using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Text;
using System.IO;

namespace WikiFormsApp
{
    public class WikiServices
    {
        public WikiServices()
        {
            // εδω ρυθμιζουμε το internet για να μπορει να συνδεθει με ασφαλεια
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
        }

        public async Task<WikiArticle> GetSummaryAsync(string term)
        {
            if (string.IsNullOrWhiteSpace(term)) return null;

            // αλλαζουμε τα κενα με κατω παυλες για να το καταλαβει η wikipedia
            string cleanTerm = term.Trim().Replace(" ", "_");
            string url = "https://el.wikipedia.org/api/rest_v1/page/summary/" + Uri.EscapeDataString(cleanTerm);

            try
            {
                using (WebClient wc = new WebClient())
                {
                    // λεμε στο προγραμμα να διαβαζει σωστα τα ελληνικα
                    wc.Encoding = Encoding.UTF8;

                    // εδω "κοροιδευουμε" τη wikipedia για να νομιζει οτι ειμαστε browser
                    wc.Headers.Add("user-agent", "Mozilla/5.0");

                    // κατεβαζουμε το κειμενο και το χωριζουμε σε κομματια
                    string json = await wc.DownloadStringTaskAsync(url);
                    JObject data = JObject.Parse(json);

                    return new WikiArticle
                    {
                        Title = data["title"]?.ToString(),
                        Extract = data["extract"]?.ToString(),
                        ThumbnailUrl = data["thumbnail"]?["source"]?.ToString(),
                        PageUrl = data["content_urls"]?["desktop"]?["page"]?.ToString()
                    };
                }
            }
            catch { return null; }
        }

        public async Task<Stream> GetImageStreamAsync(string url)
        {
            // αυτη η μεθοδος κατεβαζει μονο την εικονα
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Headers.Add("user-agent", "Mozilla/5.0");
                    byte[] data = await wc.DownloadDataTaskAsync(url);
                    return new MemoryStream(data);
                }
            }
            catch { return null; }
        }
    }
}