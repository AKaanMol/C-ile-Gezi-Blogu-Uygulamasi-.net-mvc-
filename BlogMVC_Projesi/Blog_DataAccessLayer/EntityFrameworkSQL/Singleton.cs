using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_DataAccessLayer.EntityFrameworkSQL
{
    public class Singleton
    {
        protected static BlogContext _context;
        private static object _lock = new object();
        protected Singleton()
        {
            CreateContext();

        }
        private static void CreateContext()
        {
            if (_context == null)
            {
                lock (_lock)
                {
                    if (_context == null)
                    {
                        _context = new BlogContext();
                    }

                }

            }

        }

    }
}
