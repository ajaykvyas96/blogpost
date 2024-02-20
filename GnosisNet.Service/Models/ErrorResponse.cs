using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnosisNet.Service.Models
{
    public class ErrorResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string InnerException { get; set; }
    }
}
