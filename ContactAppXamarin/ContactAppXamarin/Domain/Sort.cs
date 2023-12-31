using System;
using System.Collections.Generic;
using System.Text;

namespace ContactAppXamarin.Domain
{
    public class Sort
    {
        public Sort(string property, bool isSortAscending)
        {
            Property = property;
            IsSortAscending = isSortAscending;
        }

        public string Property { get; set; }
        public bool IsSortAscending { get; set; }

    }
}
