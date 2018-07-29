using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestApiLeseTests
{
    class WebReader
    {
        private List<Uri> UriList { get; set; }

        public List<String> Response { get; set; }

        public WebReader()
        {
        }

        public WebReader(Uri uri)
        {
            this.UriList.Add(uri);
        }

        public WebReader(List<Uri> UriList)
        {
            this.UriList = UriList;
        }

       public async void letsGetGoing()
        {
            foreach (Uri uri in UriList)
            {
                string fuckme = await ReadFromSite(uri);
            }
        }
        

        public async Task<String> ReadFromSite(Uri uri)
        {            
            //HttpClient client = new HttpClient();
            using (WebClient client = new WebClient())
            {
                try
                {
                    return await client.DownloadStringTaskAsync(uri);
                }
                catch (WebException e)
                {

                    if (e.Status == WebExceptionStatus.ProtocolError)
                    {
                        Console.WriteLine(((HttpWebResponse)e.Response).StatusCode);
                        Console.WriteLine(((HttpWebResponse)e.Response).StatusDescription);
                        Console.WriteLine("=============================================================");
                    }
                    
                    throw;
                }
                

                /*if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"The Server did not accept the request! Response:{response.StatusCode}");
                }

                string jsonString = await response.Content.ReadAsStringAsync();

                return jsonString;*/
            }
        }

    }
}
