using System;
using System.Collections.Generic;
using System.Text;

namespace ContactAppXamarin.Domain
{
    public class BadRequestDTO
    {
        public string title { get; set; }
        public int status { get; set; }
        public string traceId { get; set; }
        public object errors { get; set; }
    }
}
