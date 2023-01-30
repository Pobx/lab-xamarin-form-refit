using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using RefitLabWithXamarinForm.ViewModels;

namespace RefitLabWithXamarinForm.Services
{
	public static class ServiceRegistration
	{
        //private const string authenUrl = "https://10.0.2.2:5005"; for testing on localhost by android device
        private const string authenUrl = "https://kmauat.krungsrimobile.com/v4/identity-dev";
        private const string resourceUrl = "https://jsonplaceholder.typicode.com";


        public static IServiceCollection AddSimpleService(this IServiceCollection services)
		{
			services.AddSingleton<IMyService, MyService>();

			return services;
		}

        public static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddTransient<MainPageViewModel>();

            return services;
        }

		public static IServiceCollection AddRestApi(this IServiceCollection services)
		{
            services.AddRefitClient<IUserClient>()
           .ConfigureHttpClient(c =>
           {
               c.BaseAddress = new Uri(resourceUrl);
               c.Timeout = TimeSpan.FromMinutes(10);
           })
           .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
           {
               ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
           });

            return services;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddRefitClient<IIdentityClient>()
           .ConfigureHttpClient(c =>
           {
               c.BaseAddress = new Uri(authenUrl);
               c.Timeout = TimeSpan.FromMinutes(10);
           })
           .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
           {
               UseCookies = false,
               ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
           });

            return services;
        }
    }
}

