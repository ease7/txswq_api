using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TxsWeb.Models
{
    public class ResultValue
    {
        public int Status { get; set; }

        public string Message { get; set; }


        public object Value { get; set; }
    }
}