using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace RefitLabWithXamarinForm.Services
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddSimpleService(this IServiceCollection services)
		{
			services.AddSingleton<IMyService, MyService>();

			return services;
		}

        public static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddTransient<MainPageModel>();

            return services;
        }

		public static IServiceCollection AddRestApi(this IServiceCollection services)
		{
            var host = "https://jsonplaceholder.typicode.com";
            services.AddRefitClient<IUserClient>()
           .ConfigureHttpClient(c =>
           {
               c.BaseAddress = new Uri(host);
               c.Timeout = TimeSpan.FromMinutes(10);
           })
           .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
           {
               ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
           });

            return services;
        }


    }
}

