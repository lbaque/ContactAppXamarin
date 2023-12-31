using Infrastructure.DTO.V1.Response;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Helpers
{
    public class Helpers
    {
        public static ErrorResponse GenerarError(Exception ex)
        {

            Guid id = Guid.NewGuid();
            return new ErrorResponse() { Id = id, Error = $"{ex.Message} inner: {ex.InnerException?.Message}" };
        }

    }
}
