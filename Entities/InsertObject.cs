using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
   public  class InsertObject<T> where T : class
    {
        public string objiD { get; set; }
        public T Objects { get; set; }
    }
}
