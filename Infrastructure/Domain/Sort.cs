using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Domain
{
    public class Sort
    {
        public Sort() { }
        public Sort(string property, bool isSortAscending)
        {
            Property = property;
            IsSortAscending = isSortAscending;
        }

        public string Property { get; set; }
        public bool IsSortAscending { get; set; }

    }
}
