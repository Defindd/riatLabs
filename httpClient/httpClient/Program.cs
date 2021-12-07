using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
namespace httpClient
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();
        static async Task Main()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://127.0.0.1:8060/ping");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"Ответ на /ping: {responseBody}");
                response = await client.GetAsync("http://127.0.0.1:8060/getinputdata");
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Ответ на /getinputdata: {responseBody}");
                var json = new JsonView(responseBody);
                Console.WriteLine($"Вычисленный ответ на задание: {json.output}");

                response = await client.GetAsync($"http://127.0.0.1:8060/writeanswer?answer=\"{json.output}\"");
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Оценка ответа: {responseBody}");
                Console.Read();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\n Ошибка");
                Console.WriteLine(e.Message);
            }
        }
    }
}
