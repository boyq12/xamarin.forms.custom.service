using Mb.Core.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CakeMaker.Services.Apis
{
    public interface IApiManager
    {
        Task<HttpResponseMessage> GetShops(double latitude, double longitude, double radius, int customerId);
        Task<HttpResponseMessage> Login(LoginParameters LoginParameters);
        Task<HttpResponseMessage> GetOrders(int customerId);
        Task<HttpResponseMessage> GetProductCategories(int shopId);
        Task<HttpResponseMessage> GetShop(int id);
        Task<HttpResponseMessage> GetProductCategory(int productCategoryId);
        Task<HttpResponseMessage> GetProducts(int productCategoryId);
        Task<HttpResponseMessage> GetMessages(string roomName);
        Task<HttpResponseMessage> GetCustomer(string customerId);
        Task<HttpResponseMessage> UpdateCustomer(Customer customer);
        Task<HttpResponseMessage> UpdateCustomer(int customerId, MultipartFormDataContent body);
        Task<HttpResponseMessage> UploadImage(string shopCode, MultipartFormDataContent body);
        Task<HttpResponseMessage> GetOrderById(int orderId);
        Task<HttpResponseMessage> ApplyVoucher(string code, int shopId);
        Task<HttpResponseMessage> MakeOrder(Order order);
        Task<HttpResponseMessage> SearchStore(string queryString, double lat, double lon);
    }
}
