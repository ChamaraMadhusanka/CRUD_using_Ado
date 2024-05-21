using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_using_Ado.Models
{
    public class Responce
    {
        public HttpStatusCode statusCode { get; set; }
        public string message { get; set; }
        public object content { get; set; }
    }
}
