using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FundConnRec.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FundConnRec.MVC.Connections
{
    public class ApiClient
    {
        private readonly HttpClient httpClient;
        public ApiClient(HttpClient client)
        {
            httpClient = client;
        }

        private async Task<T> GetAsync<T>(Uri requestUrl)
        {
            var response = await httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }

        private async Task<T> PostAsync<T>(Uri requestUrl, T content)
        {
            var response = await httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }

        private async Task<T> PutAsync<T>(Uri requestUrl, T content)
        {
            var response = await httpClient.PutAsync(requestUrl.ToString(), CreateHttpContent(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }

        private async Task DeleteAsync(Uri requestUrl)
        {
            var response = await httpClient.DeleteAsync(requestUrl.ToString());
            response.EnsureSuccessStatusCode();
            await response.Content.ReadAsStringAsync();
        }

        private HttpContent CreateHttpContent<T>(T content)
        {
            var json = JsonConvert.SerializeObject(content);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var requestUrl = new Uri(httpClient.BaseAddress.ToString() + "api/products");
            return await GetAsync<List<Product>>(requestUrl);
        }

        public async Task<Product> GetProduct(int id)
        {
            var requestUrl = new Uri(httpClient.BaseAddress.ToString() + String.Format("api/products/{0}", id));
            return await GetAsync<Product>(requestUrl);
        }

        public async Task<Product> PutProduct(Product product)
        {
            var requestUrl = new Uri(httpClient.BaseAddress.ToString() + String.Format("api/products/{0}",product.ProductId));

            return await PutAsync<Product>(requestUrl, product);
        }

        public async Task<Product> PostProduct(Product product)
        {
            var requestUrl = new Uri(httpClient.BaseAddress.ToString() + "api/products");
            return await PostAsync<Product>(requestUrl, product);
        }

        public async Task DeleteProduct(int id)
        {
            var requestUrl = new Uri(httpClient.BaseAddress.ToString() + String.Format("api/products/{0}", id));
            await DeleteAsync(requestUrl);
        }
    }
}
