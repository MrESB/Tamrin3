

using Newtonsoft.Json;
using System.Diagnostics;
using Tamrin3;


await RunInBackground(TimeSpan.FromSeconds(60), () => InitAsync());

async Task RunInBackground(TimeSpan timeSpan, Action action)
{
    var periodicTimer = new PeriodicTimer(timeSpan);
    do
    {
        action();
    }
    while (await periodicTimer.WaitForNextTickAsync());
    {
        action();
    }
}



async Task InitAsync()
{
    HttpClient httpClient = new HttpClient();
    string stringAPI = "https://api.wallex.ir/v1/currencies/stats";

    HttpResponseMessage response = await httpClient.GetAsync(stringAPI);

    if (response.IsSuccessStatusCode)
    {
        string apiresponse = await response.Content.ReadAsStringAsync();
        ApiModel apiModel = JsonConvert.DeserializeObject<ApiModel>(apiresponse);
        List<ResultItem> resultItem = apiModel.result;
        foreach (var item in resultItem)
        {
            Console.WriteLine($"Rank: {item.rank} \n");
            Console.WriteLine($"Key: {item.key} \n");
            Console.WriteLine($"Name: {item.name_en}  \n");
            Console.WriteLine($"Price: {item.price}  \n");
            Console.WriteLine($"This currency has {item.percent_change_1h} changed in last hour. \n");
            Console.WriteLine($"Updated at: {item.updated_at}  \n");
            try
            {
                double? ExpectedPrice()
                {
                    double? price24hago = item.price + item.price_change_24h;
                    double? changeperminute = ((price24hago - item.price) / 24);
                    double? expectedprice = item.price + changeperminute;
                    return expectedprice;
                }
                Console.WriteLine("expected price after one hour: {0} ", ExpectedPrice());
            }
            catch (Exception ex)
            {
                Console.WriteLine("expected price: API sending Null");
            }
            Console.WriteLine("----------------------------------------------------------------------------------");



        }
    }
}

