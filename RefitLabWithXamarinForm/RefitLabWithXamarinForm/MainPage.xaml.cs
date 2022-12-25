using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using RefitLabWithXamarinForm.Services;
using Xamarin.Forms;

namespace RefitLabWithXamarinForm
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<MainPageModel>();
        }
    }
}
