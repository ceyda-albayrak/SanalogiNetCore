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
        IDataResult<List<Category>> GetCategories();
    }
}
