using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_Course_project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Call the base implementation first
            base.OnStartup(e);

            // Add your custom logic here
            // For example, show a custom startup window or perform some initialization tasks
            int x = 32;
        }
    }

}
