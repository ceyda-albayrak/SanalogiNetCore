using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constant;
using Core.Utilities.Results;
using Core.Utulities;
using Entities;
using Newtonsoft.Json;
using Polly;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;
using RestSharp;

namespace Business
{
    public class CategoryManager : ICategoryService
    {
        private static readonly CircuitBreakerPolicy<IRestResponse> CircuitBreakerPolicy =
            Policy.HandleResult<IRestResponse>(message => message.StatusCode != System.Net.HttpStatusCode.OK)
                .CircuitBreaker(2, TimeSpan.FromMinutes(5));

        public IDataResult<List<Category>> GetCategories()
        {
            if (CircuitBreakerPolicy.CircuitState == CircuitState.Open)
            {
                System.Diagnostics.Debug.WriteLine("if circuit braker (servise ulaşılmadı  yada hata veriyor)");
                return new ErrorDataResult<List<Category>>(new List<Category>(), "SERVise ulaşılamıyor");
            }
            try
            {
                var client = new RestSharp.RestClient(ServicePath.ibbHal);
                client.Timeout = 2000; // 2 second
                var requestGetCategory = new RestSharp.RestRequest(
                    $"api/HalManager/getCategories",
                    Method.GET);

                IRestResponse responseCategory = CircuitBreakerPolicy.Execute(() => client.Execute(requestGetCategory));
                if (responseCategory.IsSuccessful && !string.IsNullOrEmpty(responseCategory.Content))
                {
                    var a = JsonConvert.DeserializeObject<ResponseCategory>(responseCategory.Content);
                    return new SuccessDataResult<List<Category>>(a.Results);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("catch");
            }
            return new ErrorDataResult<List<Category>>(new List<Category>(), "Boş");
        }
    }   
}

     


    

