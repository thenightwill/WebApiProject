using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Response<T> where T: class
    {
        public bool RequestState { get; set; }
        public string ErrorMessage { get; set; }
        public List<T> Registers { get; set; }
        public T Register { get; set; }

    }
}
