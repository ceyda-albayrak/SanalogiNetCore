using Business.Abstract;
using Core.Utulities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polly.CircuitBreaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

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
            var result = _categoryService.GetCategories("https://halfiyatlaripublicdata.ibb.gov.tr/api/HalManager/getCategories");
            if (result.ResponseStatus != true)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }


    }
}
