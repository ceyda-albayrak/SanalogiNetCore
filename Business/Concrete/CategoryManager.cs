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
            Policy.HandleResult<IRestResponse>(message => message.StatusCode != System.Net.HttpStatusCode.NotFound && message.StatusCode != System.Net.HttpStatusCode.OK)
                .CircuitBreaker(5, TimeSpan.FromMinutes(30));

        public IResult<List<Category>> GetCategories(string url)
        {
            if (CircuitBreakerPolicy.CircuitState == CircuitState.Open)
            {
                throw new Exception(Messages.ServiceUnavailable);
            }
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = CircuitBreakerPolicy.Execute(() => client.Execute(request));
            if (!response.IsSuccessful && string.IsNullOrEmpty(response.Content))
            {
                System.Diagnostics.Debug.WriteLine(Messages.Unsuccesfull);
            }
           return  JsonConvert.DeserializeObject<Result<List<Category>>>(response.Content);
          
        }
      

    }
            
        
}

     


    

