using System;
using System.Collections.Generic;
using System.Text;

namespace ContactAppXamarin.Helpers
{
    public static class ServiceRoute
    {
        public static class Contact
        {
            private static readonly string _route = "API";
            public static class V1
            {
                private static readonly string version = "1.0";
                public static class Usuario
                {
                    public static string Fetch => $"{_route}/{version}/Usuario";
                    public static string FetchId(Guid id) => $"{_route}/{version}/Usuario/{id}";
                }
                public static class Contacto
                {
                    public static string Fetch => $"{_route}/{version}/Contacto";
                    public static string FetchId(Guid id) => $"{_route}/{version}/Contacto/{id}";
                }

            }
        }
    }
}
