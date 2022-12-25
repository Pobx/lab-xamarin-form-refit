using System;
using System.ComponentModel;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using RefitLabWithXamarinForm.Services;
using Xamarin.Forms;

namespace RefitLabWithXamarinForm
{
    public class MainPageModel : INotifyPropertyChanged
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

        public ICommand GetStringCommand { get; private set; }
        public ICommand GetUsersCommand { get; private set; }
        private readonly IMyService _myservice;
        private readonly IUserClient _userClient;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageModel(IMyService myService, IUserClient userClient)
        {
            _myservice = myService ?? throw new ArgumentNullException(nameof(myService));
            _userClient = userClient ?? throw new ArgumentNullException(nameof(userClient));
            myString = "Waiting Click...";
            userString = "Waiting Click get users...";
            GetStringCommand = new Command(() => GetMyString());
            GetUsersCommand = new Command(async () => await GetUsers());

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

