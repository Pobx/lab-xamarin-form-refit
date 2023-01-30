using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using RefitLabWithXamarinForm.Services;
using Xamarin.Forms;

namespace RefitLabWithXamarinForm.ViewModels

{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private string _myString;
        public string myString
        {
            get
            {
                return _myString;
            }
            set
            {
                if (value != null)
                {
                    _myString = value;
                    OnPropertyChanged(nameof(myString));
                }
            }
        }

        private string _users;
        public string userString
        {
            get
            {
                return _users;
            }
            set
            {
                if (value != null)
                {
                    _users = value;
                    OnPropertyChanged(nameof(userString));
                }
            }
        }

        private string _identityResponse;
        public string identityResponse
        {
            get
            {
                return _identityResponse;
            }
            set
            {
                if (value != null)
                {
                    _identityResponse = value;
                    OnPropertyChanged(nameof(identityResponse));
                }
            }
        }

        public ICommand GetStringCommand { get; private set; }
        public ICommand GetUsersCommand { get; private set; }
        public ICommand LoginCommand { get; private set; }
        private readonly IMyService _myservice;
        private readonly IUserClient _userClient;
        private readonly IIdentityClient _identityClient;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageViewModel(IMyService myService, IUserClient userClient, IIdentityClient identityClient)
        {
            _myservice = myService ?? throw new ArgumentNullException(nameof(myService));
            _userClient = userClient ?? throw new ArgumentNullException(nameof(userClient));
            _identityClient = identityClient ?? throw new ArgumentNullException(nameof(identityClient));

            myString = "Waiting Click...";
            userString = "Waiting Click get users...";
            identityResponse = "Waiting Click Loginnnnn....";
            GetStringCommand = new Command(() => GetMyString());
            GetUsersCommand = new Command(async () => await GetUsers());
            LoginCommand = new Command(async () => await Login());
        }

        private void GetMyString()
        {
            myString = _myservice.GetString();
        }

        private async Task GetUsers()
        {
            var response = await _userClient.GetUsers();
            if (!response.IsSuccessStatusCode)
            {
                userString = "Api Error: Can not get data from api resource...";
                return;
            }

            userString = JsonSerializer.Serialize(response.Content);
        }

        private async Task Login()
        {
            var data = new Dictionary<string, string> {
                {"grant_type", "guest"},
                {"scope", "guest openid profile offline_access"},
                {"client_id", "client.kma.token"},
                {"client_secret", "9076b40d-9a81-47df-9cf0-ed219982d8af"},
                {"encryptData", "46PU1SfC+xZ+48tW+hT5+HIOYpHclijQIjAaXMpsoS9e+n9UZMkXbdqiupt/y6TPbJvYkGdxzqZY64NU7fLP4h3/SnqDMMmzTFbEBZgl1W1tlp6AKs9n5AeJVX2VimpX5i45loyCOKtNVYhGlkyOLnbXq7u+BhgBo92zkQBHHhMnS1a/2mQczH5YUIawXDWyuxhEj1RyzDSnCSbwV0o6sqVSolgpz58oKrm1wdoXOIaGxzuaivaZ2qdmHDFu1ZNzAaENJyYfBkQz8L1Z4zcmNMkvIpCoXoIFDYmbck9uPlnCteWxCWZvHpmRiJHP3qZqgP6/0wAKI2fBM0tMdAypPVylEhn8cfDyHtl7XGwdsIqMg5p2a6rGWshmYP7aLbodA/JN4oVMcYAsRihLmgqrDqQd04Q90SA2PVkV+sTfY2uXPczUC9UEXmR1Jz5AdeOtfPI++YWsxFRJ9N9MfnI2ITtXOkE4aBrTMg/IfGWx1lYnO8uWIKVmh4zlm1EQccLinJLF85y/dclCuLzUKilFza7bRzfeZAnMSdZUJTFbeP3l3K8tDEt2V6c+g1xuUXjZBVXU0WNTU3Rc1/eP51NLqWLwJQZGEptvDoDbVWYEwaVxcL4XX8B+GmeU/8UHPfk7tNiUA71HESYTtAl5j7Hh2oeVPRu/hhAkAiIx4oG41pWGu04+Sb9Qh5nVw6aJsO/ZICj3l6z8/3BepHYXdy/RFZp3cQWVRPyAmNQ530iUS2ddCjbuw/cmf/wbmCpVuRQfx1PXpWFl9fHBEU0ZXV8aRqAhVEbYHrt6HnHtusSLpc3t/f3ba1s82jMOGSuxDUldnbcNMZWMIZtM01ClZ43GaxxFLd/5n2YVRFsB+AFkpt+/zMApOneVlT0p03idmhgAkzNEJm4tbRugPaF4uNaXwuCGIGglTnsUL1D3vs4daFkKMTAKPMejTdFSw3L2rYXpPA9Enfrz+kShg3p39SZ5I9O98Np7J/IEP6L2C8PWoa+R8qNxCVN8aMYtZVSMrMq2EmEx30Wub/FhXRXGvLlMQDqJDOUIf/edzbpsvFgS9XWKPpQuPwQQMVLDdedVg9vbsy1CjW8KLH7qaBhWT1P+Aw==" },
            };

            var headers = new Dictionary<string, string> { { "Cookie", "ASP.NET_SessionId=vfy3lyeugbihmnb4p2lhy4md;" } };

            Console.WriteLine($"Start request...");
            identityResponse = "Waiting...";
            var response = await _identityClient.AuthenGuest(data, headers);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error: {response.Error.Content}");
                identityResponse = response.Error.Content;
                return;
            }

            Console.WriteLine($"Finished request...");
            identityResponse = JsonSerializer.Serialize(response.Content);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

