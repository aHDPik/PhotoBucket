using System;
using System.IO;
using System.Net.Http;

namespace PhotoClient
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            HttpClient client = new HttpClient();
            MultipartFormDataContent content = new MultipartFormDataContent();
            FileStream fs = new FileStream(@"C:\Users\Andrew\OneDrive\Изображения\channels.png", FileMode.Open);
            content.Add(new StreamContent(fs), "Avatar");
            content.Add(new StringContent("Petya"), "Username");
            await client.PostAsync("http://localhost/Home/CreateUserPage", content);
        }
    }
}
