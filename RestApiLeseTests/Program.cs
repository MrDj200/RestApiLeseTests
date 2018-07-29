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

        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static HttpClient client = new HttpClient();
        static async Task MainAsync(string[] args)
        {
            client.BaseAddress = new Uri("https://api.warframe.market/v1/items");
            HttpResponseMessage httpResponse = client.GetAsync(client.BaseAddress).GetAwaiter().GetResult();
            string jsonString = httpResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            BaseItems baseItems = BaseItems.FromJson(jsonString);

            

            baseItems.Payload.Items.En.OrderBy(e => e.ItemName);
            Console.Title = baseItems.Payload.Items.En.Where(c => c.UrlName.Contains("prime")).ToList().Count + "Items";

            Dictionary<String, List<Order>> fuck = await TestMethod(baseItems.Payload.Items.En.Where(c => c.UrlName.Contains("prime")));


            Console.ReadLine();
        }

        private static async Task<Dictionary<String, List<Order>>> TestMethod(IEnumerable<MarketAPI.En> list)
        {
            WebReader reader = new WebReader();
            Dictionary<String, List<Order>> OrderList = new Dictionary<string, List<Order>>();

            IEnumerable<Task> fuckme6 = list.Select(async item => 
            {
                Uri tempUri = new Uri($"{client.BaseAddress}/{item.UrlName}/orders");

                string jsonStringFuck = await reader.ReadFromSite(tempUri);

                Orders orders = Orders.FromJson(jsonStringFuck);

                //orders.Payload.Orders.RemoveAll(o => o.User.Status != Status.Online);
                orders.Payload.Orders.RemoveAll(o => o.Platform != Platform.Pc);
                orders.Payload.Orders.RemoveAll(o => o.OrderType != OrderType.Sell);
                orders.Payload.Orders.RemoveAll(o => o.Visible != true);

                orders.Payload.Orders.OrderBy(o => o.Platinum);


                if (orders.Payload.Orders.Count >= 7)
                {
                    //orders.Payload.Orders.RemoveRange(5, orders.Payload.Orders.Count - 1);
                    OrderList.Add(item.UrlName, orders.Payload.Orders.Take(5).ToList());
                }
            });

            await Task.WhenAll(fuckme6);

            /*ParallelLoopResult fuckMe5 = Parallel.ForEach(list, async (item) =>
            {
                Uri tempUri = new Uri($"{client.BaseAddress}/{item.UrlName}/orders");
                //HttpResponseMessage shit = client.GetAsync(tempUri).GetAwaiter().GetResult();
                //string jsonStringFuck = shit.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                string jsonStringFuck = await reader.ReadFromSite(tempUri);

                Orders orders = Orders.FromJson(jsonStringFuck);

                //orders.Payload.Orders.RemoveAll(o => o.User.Status != Status.Online);
                orders.Payload.Orders.RemoveAll(o => o.Platform != Platform.Pc);
                orders.Payload.Orders.RemoveAll(o => o.OrderType != OrderType.Sell);
                orders.Payload.Orders.RemoveAll(o => o.Visible != true);

                orders.Payload.Orders.OrderBy(o => o.Platinum);


                if (orders.Payload.Orders.Count >= 7 )
                {
                    orders.Payload.Orders.RemoveRange(5, orders.Payload.Orders.Count - 1);
                    OrderList.Add(item.UrlName, orders.Payload.Orders);
                }
            });*/

            return OrderList;

        }

    }
}
