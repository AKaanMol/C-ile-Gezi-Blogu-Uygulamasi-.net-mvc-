using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_BusinessLayer
{
    public class BusinessLayerResult<T> where T : class
    {
        public List<string> Erorrs { get; set; }
        public T Result { get; set; }
        public BusinessLayerResult()
        {
            Erorrs = new List<string>();
        }

    }
}
