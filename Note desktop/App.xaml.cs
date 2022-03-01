using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Note_desktop.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Note_desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Ioc.Default.ConfigureServices(
                    new ServiceCollection()
                    .AddSingleton<NoteVM>()
                    .BuildServiceProvider());

        }
        public NoteVM NoteVM
        {
            get { return Ioc.Default.GetService<NoteVM>(); }
        }
    }
}
