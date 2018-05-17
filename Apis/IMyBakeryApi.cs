
using CakeMaker.Models;
using CakeMaker.Services.Apis.ResponseModels;
using Mb.Core.Models;
using Mb.Core.Models.Search;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CakeMaker.Services.Apis
{
    [Headers("Authorization: Bearer")]
    public interface IMyBakeryApi
    {
        //This is used with IMyBakery Api
        //[Headers("Content-Type: application/x-www-form-urlencoded")]
        //[Post("/connect/token")]
        //Task<dynamic> Login([Body(BodySerializationMethod.UrlEncoded)] LoginParameters LoginParameters);

        //[Post("/api/order")]
        //Task<int> MakeOrder([Body] Order order);

        //[Get("/api/order/customer/{id}")]
        //Task<List<Order>> GetOrders([AliasAs("id")] int customerId);

        //[Get("/api/shop")]
        //Task<List<Shop>> GetShops(double latitude, double longitude, double radius, int customerId);
        //[Get("/api/shop/{id}")]
        //Task<Shop> GetShop([AliasAs("id")] int id);


        ////ProductCategory will include its products list
        //[Get("/api/productCategory/{id}")]
        //Task<ProductCategory> GetProductCategory([AliasAs("id")] int productCategoryId);

        //[Get("/api/product/{productCategoryId}")]
        //Task<List<Product>> GetProducts([AliasAs("productCategoryId")] int productCategoryId);

        //[Get("/api/chatMessage/{roomName}")]
        //Task<List<ChatMessage>> GetMessages([AliasAs("roomName")] string roomName);

        ////Customer
        //[Get("/api/customer/{id}")]
        //Task<Customer> GetCustomer([AliasAs("id")] string customerId);

        //[Put("/api/customer")]
        //Task<Customer> UpdateCustomer([Body] Customer customer);

        //[Put("/api/customer/image/{customerId}")]
        //Task<String> UpdateCustomer([AliasAs("customerId")] int customerId, [Body] MultipartFormDataContent body);


        //[Post("/api/image/{shopCode}")]
        //Task<string> UploadImage([AliasAs("shopCode")] string shopCode, [Body] MultipartFormDataContent body);

        //[Get("/api/order/by-id/{id}")]
        //Task<Order> GetOrderById([AliasAs("id")] int orderId);

        //[Get("/api/shop/search")]
        //Task<List<CustomerSearchResult>> SearchStore([AliasAs("q")] string queryString, [AliasAs("lat")] double lat, [AliasAs("lon")] double lon);

        //[Get("/api/productCategory/by-shop/{shopId}")]
        //Task<List<ProductCategory>> GetProductCategories([AliasAs("shopId")] int shopId);

        ////voucher
        //[Get("/api/voucher/check/{code}/{shopId}")]
        //Task<VoucherResult> ApplyVoucher([AliasAs("code")] string code, [AliasAs("shopId")] int shopId);

        //---------This used with ApiManager -------------

        //return dynamic object
        [Headers("Content-Type: application/x-www-form-urlencoded")]
        [Post("/connect/token")]
        Task<HttpResponseMessage> Login([Body(BodySerializationMethod.UrlEncoded)] LoginParameters LoginParameters);


        //return int
        [Post("/api/order")]
        Task<HttpResponseMessage> MakeOrder([Body] Order order);

        //return List<Order>
        [Get("/api/order/customer/{id}")]
        Task<HttpResponseMessage> GetOrders([AliasAs("id")] int customerId);

        //return List<Shop>
        [Get("/api/shop")]
        Task<HttpResponseMessage> GetShops(double latitude, double longitude, double radius, int customerId);

        //return shop
        [Get("/api/shop/{id}")]
        Task<HttpResponseMessage> GetShop([AliasAs("id")] int id);


        //ProductCategory will include its products list
        //return ProductsCategory
        [Get("/api/productCategory/{id}")]
        Task<HttpResponseMessage> GetProductCategory([AliasAs("id")] int productCategoryId);

        //return List<Product>
        [Get("/api/product/{productCategoryId}")]
        Task<HttpResponseMessage> GetProducts([AliasAs("productCategoryId")] int productCategoryId);

        //return List<ChatMessage>
        [Get("/api/chatMessage/{roomName}")]
        Task<HttpResponseMessage> GetMessages([AliasAs("roomName")] string roomName);

        //Customer
        [Get("/api/customer/{id}")]
        Task<HttpResponseMessage> GetCustomer([AliasAs("id")] string customerId);
        //return Customer
        [Put("/api/customer")]
        Task<HttpResponseMessage> UpdateCustomer([Body] Customer customer);
        //return string
        [Put("/api/customer/image/{customerId}")]
        Task<HttpResponseMessage> UpdateCustomer([AliasAs("customerId")] int customerId, [Body] MultipartFormDataContent body);
        
        //return string
        [Post("/api/image/{shopCode}")]
        Task<HttpResponseMessage> UploadImage([AliasAs("shopCode")] string shopCode, [Body] MultipartFormDataContent body);

        //return Order
        [Get("/api/order/by-id/{id}")]
        Task<HttpResponseMessage> GetOrderById([AliasAs("id")] int orderId);

        //return List<CustomerSearchResult>
        [Get("/api/shop/search")]
        Task<HttpResponseMessage> SearchStore([AliasAs("q")] string queryString, [AliasAs("lat")] double lat, [AliasAs("lon")] double lon);

        //return List<ProductCategory>
        [Get("/api/productCategory/by-shop/{shopId}")]
        Task<HttpResponseMessage> GetProductCategories([AliasAs("shopId")] int shopId);

        //voucher return VoucherResult
        [Get("/api/voucher/check/{code}/{shopId}")]
        Task<HttpResponseMessage> ApplyVoucher([AliasAs("code")] string code, [AliasAs("shopId")] int shopId);
    }
}