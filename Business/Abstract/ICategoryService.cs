using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        IResult<List<Category>> GetCategories(string url);
    }
}
