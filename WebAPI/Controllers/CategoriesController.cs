using Business.Abstract;
using Core.Utulities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polly.CircuitBreaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Caching;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;
using MemoryCache = System.Runtime.Caching.MemoryCache;

namespace WebAPI.Controllers
{
   

    public class CategoriesController : ControllerBase
    {
        private ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [AllowAnonymous]
        [Route("GetCategories")]
        [HttpGet]
        public IActionResult GetCategories()
        {
            var cache = MemoryCache.Default;
            var categoryList = new List<Category>();
            categoryList = (List<Category>)cache.Get("categoryList");
     
            if (categoryList == null)
            {
                categoryList = _categoryService
                    .GetCategories("https://halfiyatlaripublicdata.ibb.gov.tr/api/HalManager/getCategories").Results;
                System.Diagnostics.Debug.WriteLine(
                    "Data were not in the cache.I got the from data source SLOW at " + DateTime.Now);
                var policy = new CacheItemPolicy().AbsoluteExpiration = DateTime.Now.AddMinutes(5);
                cache.Set("categoryList", categoryList, policy);
                return Ok(categoryList);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(
                    "Data were in the cache.I got the from data source FAST at " + DateTime.Now);
            }

            return Ok(categoryList);
        }


    }
}
