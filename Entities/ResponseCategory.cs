using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ResponseCategory
    {
        public bool ResponseStatus { get; set; }
        public object ErrorGUID { get; set; }
        public object Message { get; set; }
        public List<Category> Results { get; set; }
    }
}
