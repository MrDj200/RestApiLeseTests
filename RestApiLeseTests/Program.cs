using RestApiLeseTests.WebObjects;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using MarketAPI;
using System.Threading.Tasks;
using System.Linq;
using MarketAPI.Orders;
using System.Collections.Generic;
using System.Threading;

namespace RestApiLeseTests
{
    class Program
    {
        static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            client.BaseAddress = new Uri("https://api.warframe.market/v1/items");
            HttpResponseMessage shit = client.GetAsync(client.BaseAddress).GetAwaiter().GetResult();
            string jsonString = shit.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            BaseItems baseItems = BaseItems.FromJson(jsonString);

            StringBuilder str = new StringBuilder();
            int index = 0;
            Uri tempUri;

            baseItems.Payload.Items.En.OrderBy(e => e.ItemName);
            Console.Title = baseItems.Payload.Items.En.Where(c => c.UrlName.Contains("prime")).ToList().Count + "Items";

            foreach (var item in baseItems.Payload.Items.En.Where(c => c.UrlName.Contains("prime")))
            {
                tempUri = new Uri($"{client.BaseAddress}/{item.UrlName}/orders");
                jsonString = testMethod(tempUri);
                Orders orders = Orders.FromJson(jsonString);

                //orders.Payload.Orders.RemoveAll(o => o.User.Status != Status.Online);
                orders.Payload.Orders.RemoveAll(o => o.Platform != Platform.Pc);
                orders.Payload.Orders.RemoveAll(o => o.OrderType != OrderType.Sell);
                orders.Payload.Orders.RemoveAll(o => o.Visible != true);

                orders.Payload.Orders.OrderBy(o => o.Platinum);


                //orders.Payload.Orders.RemoveRange(5, orders.Payload.Orders.Count);

                if (orders.Payload.Orders.Count > 0)
                {
                    Console.WriteLine($"{index}. {item.ItemName}: {orders.Payload.Orders.Count} times\n");
                }


                str.
                    Append($"{index++}. Name={item.ItemName} URLName= {item.UrlName}\n");
            }

            Console.WriteLine($"{str.ToString()}");
            Console.ReadLine();
        }


        public static void testMethod(Uri uri)
        {
            #region ThreadStuff
            WebReader shit = new WebReader(uri);
            Thread botThread = new Thread(shit.letsGetGoing);
            botThread.Start();
            #endregion
        }

    }
}
