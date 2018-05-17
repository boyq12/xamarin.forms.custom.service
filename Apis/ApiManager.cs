using CakeMaker.Helpers;
using CakeMaker.ViewModels;
using Fusillade;
using Mb.Core.Models;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Polly;
using Prism.Navigation;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMaker.Services.Apis
{
    public class ApiManager : IApiManager

    {
        IConnectivity _connectivity = CrossConnectivity.Current;
        public bool IsConnected { get; set; }
        public bool IsReachable { get; set; }
        IApiService<IMyBakeryApi> myBakeryApi;

        Dictionary<int, CancellationTokenSource> runningTasks = new Dictionary<int, CancellationTokenSource>();
        Dictionary<int, Task<HttpResponseMessage>> taskContainer = new Dictionary<int, Task<HttpResponseMessage>>();
        public ApiManager(IApiService<IMyBakeryApi> _myBakeryApi)
        {
            myBakeryApi = _myBakeryApi;
            IsConnected = _connectivity.IsConnected;
            _connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        void OnConnectivityChanged(object senser, ConnectivityChangedEventArgs args)
        {
            IsConnected = args.IsConnected;
            if (!IsConnected)
            {
                var items = runningTasks.ToList();
                foreach (var item in items)
                {
                    item.Value.Cancel();
                    runningTasks.Remove(item.Key);
                }
            }
        }

        protected async Task<TData> RemoteRequestAsync<TData>(Task<TData> task)
            where TData : HttpResponseMessage,
            new()
        {
            TData data = new TData();
            if (!IsConnected)
            {
                var stringResponse = "There is not a network connection.";
                data.StatusCode = HttpStatusCode.BadRequest;
                data.Content = new StringContent(stringResponse);
                await PopupMessage("Lỗi mạng", stringResponse);
                return data;
            }
            IsReachable = await _connectivity.IsRemoteReachable(Constants.ApiHostName);
            if (!IsReachable)
            {
                var stringResponse = "There is not an internet connection.";
                data.StatusCode = HttpStatusCode.BadRequest;
                data.Content = new StringContent(stringResponse);
                await PopupMessage("Lỗi mạng", stringResponse);
                return data;
            }
            data = await Policy
                .Handle<WebException>()
                .Or<ApiException>()
                .Or<TaskCanceledException>()
                .WaitAndRetryAsync
                (
                    retryCount: 1,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                )
                .ExecuteAsync(async () =>
                {
                    var result = await task;
                    if (result.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        App.ISocialService.Logout();
                        await App.AppNavigationService.NavigateAsync("Login");
                    }
                    return result;
                });
            return data;
        }

        public async Task PopupMessage(string popupTitle, string message)
        {
            await App.AppNavigationService.ClearPopupStackAsync();
            await App.AppNavigationService.GoBackToRootAsync();
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add(MessagePopupPageViewModel.TITLE, popupTitle);
            parameters.Add(MessagePopupPageViewModel.MESSAGE, message);
            var a = App.Current.MainPage;
            parameters.Add(Helpers.Constants.NavigationModeParam, NavigationMode.New);
            await App.AppNavigationService.NavigateAsync("MessagePopupPage", parameters, useModalNavigation: false, animated: true);
        }

        public async Task<HttpResponseMessage> GetShops(double latitude, double longitude, double radius, int customerId)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>(myBakeryApi.GetApi(Priority.UserInitiated).GetShops(latitude, longitude, radius, customerId));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<HttpResponseMessage> Login(LoginParameters LoginParameters)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>(myBakeryApi.GetApi(Priority.UserInitiated).Login(LoginParameters));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<HttpResponseMessage> GetOrders(int customerId)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>(myBakeryApi.GetApi(Priority.UserInitiated).GetOrders(customerId));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<HttpResponseMessage> GetProductCategories(int shopId)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>(myBakeryApi.GetApi(Priority.UserInitiated).GetProductCategories(shopId));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<HttpResponseMessage> GetShop(int id)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>(myBakeryApi.GetApi(Priority.UserInitiated).GetShop(id));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<HttpResponseMessage> GetProductCategory(int productCategoryId)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>(myBakeryApi.GetApi(Priority.UserInitiated).GetProductCategory(productCategoryId));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<HttpResponseMessage> GetProducts(int productCategoryId)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>(myBakeryApi.GetApi(Priority.UserInitiated).GetProducts(productCategoryId));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<HttpResponseMessage> GetMessages(string roomName)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>(myBakeryApi.GetApi(Priority.UserInitiated).GetMessages(roomName));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<HttpResponseMessage> GetCustomer(string customerId)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>(myBakeryApi.GetApi(Priority.UserInitiated).GetCustomer(customerId));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<HttpResponseMessage> UpdateCustomer(Customer customer)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>(myBakeryApi.GetApi(Priority.UserInitiated).UpdateCustomer(customer));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<HttpResponseMessage> UpdateCustomer(int customerId, MultipartFormDataContent body)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>(myBakeryApi.GetApi(Priority.UserInitiated).UpdateCustomer(customerId, body));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<HttpResponseMessage> UploadImage(string shopCode, MultipartFormDataContent body)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>(myBakeryApi.GetApi(Priority.UserInitiated).UploadImage(shopCode, body));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<HttpResponseMessage> GetOrderById(int orderId)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>(myBakeryApi.GetApi(Priority.UserInitiated).GetOrderById(orderId));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<HttpResponseMessage> ApplyVoucher(string code, int shopId)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>(myBakeryApi.GetApi(Priority.UserInitiated).ApplyVoucher(code, shopId));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<HttpResponseMessage> MakeOrder(Order order)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>(myBakeryApi.GetApi(Priority.UserInitiated).MakeOrder(order));
            runningTasks.Add(task.Id, cts);
            return await task;
        }

        public async Task<HttpResponseMessage> SearchStore(string queryString, double lat, double lon)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>(myBakeryApi.GetApi(Priority.UserInitiated).SearchStore(queryString, lat, lon));
            runningTasks.Add(task.Id, cts);
            return await task;
        }
    }
}