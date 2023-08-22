using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simple_Api.Helpers
{
    public class GenericHelper
    {
        public static string ConvertToUpper(object id)
        {
            return (id == null) ? null : id.ToString().ToUpper();
        }
    }
}