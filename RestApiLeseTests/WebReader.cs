using System;
using System.Collections.Generic;
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
        

        private async Task<String> ReadFromSite(Uri uri)
        {
            //HttpClient client = new HttpClient();
            using (HttpClient client = new HttpClient())
            {    
                client.BaseAddress = uri;

                HttpResponseMessage response = await client.GetAsync(client.BaseAddress);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("The Server did not accept the request!");
                }

                string jsonString = await response.Content.ReadAsStringAsync();

                return jsonString;
            }
        }

    }
}
