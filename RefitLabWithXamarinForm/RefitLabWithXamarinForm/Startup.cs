using System;
using Microsoft.Extensions.DependencyInjection;
using RefitLabWithXamarinForm.Services;

namespace RefitLabWithXamarinForm
{
    public static class Startup
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static IServiceProvider Init()
        {
            var serviceProvider = new ServiceCollection()
                .AddSimpleService()
                .AddViewModels()
                .AddRestApi()
                .AddIdentity()
                .BuildServiceProvider();

            ServiceProvider = serviceProvider;

            return serviceProvider;

        }
    }
}

