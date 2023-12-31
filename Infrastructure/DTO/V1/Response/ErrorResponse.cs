using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO.V1.Response
{
    public class ErrorResponse
    {
        public Guid Id { get; set; }
        public string Error { get; set; }
    }
}
