using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Services
{
    public class CacheRequest
    {
        public string key { get; set; }
        public object value { get; set; }
    }
}
