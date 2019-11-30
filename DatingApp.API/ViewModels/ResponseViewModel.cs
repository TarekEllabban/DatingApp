using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.ViewModels
{
    public class ResponseViewModel
    {
        public bool IsSucceeded { get; set; }
        public string Data { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
    }
}
