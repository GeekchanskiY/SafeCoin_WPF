using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_Course_project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static App Me => ((App)Application.Current);
        public int x = 0;
        public static HttpClient client = new HttpClient();
        protected override void OnStartup(StartupEventArgs e)
        {
            // Call the base implementation first
            base.OnStartup(e);

            // Add your custom logic here
            // For example, show a custom startup window or perform some initialization tasks
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            x = 32;
        }
        public async Task<string> get_json(string url)
        {
            HttpResponseMessage response = await client.GetAsync(url);
            string response_text = await response.Content.ReadAsStringAsync();
            return response_text;
        }
    }

}
