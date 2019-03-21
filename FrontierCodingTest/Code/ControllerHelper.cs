using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FrontierCodingTest
{
    public static class ControllerHelper
    {
        public static async Task<T> HttpGetDataAsync<T>() // Keep it generic, instead of specialized.
        {
            // Dont forget usings for resource cleanup
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(Settings.Endpoint_Accounts)) // See Settings class for comments.
                {
                    response.EnsureSuccessStatusCode(); // Throw exception if the HTTP status code was not in the 200s
                    using (HttpContent content = response.Content)
                    {
                        return await DeserializeJson<T>(response.Content.ReadAsStringAsync());
                    }
                }
            }
        }

        public static Task<T> DeserializeJson<T>(Task<string> json)
        {
            return json.ContinueWith<T> // Dont await here, this method is not async. Just wrap in a continuation task.
            (
                (tsk) =>
                {
                    // Its okay if the result is retreived synchronously.
                    // Since this is wrapped in a task, it can be awaited on asynchronously.
                    return JsonConvert.DeserializeObject<T>(tsk.Result);
                }
            );
        }

    }
}