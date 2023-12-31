using Infrastructure.DTO.V1.Response;
using Microsoft.Extensions.Logging;
using System;

namespace Infrastructure.Helpers
{
    public class Parametros
    {
        //public const string Error = "E";

        public static ErrorResponse AuditroriaError(ILogger looger, string txMensaje)
        {
            Guid id = Guid.NewGuid();
            looger.LogError(txMensaje, id);
            return new ErrorResponse()
            {
                Id = id,
                Error = txMensaje
            };
        }

        public static ErrorResponse AuditoriaError(ILogger looger, Exception ex)
        {
            Guid id = Guid.NewGuid();
            looger.LogError(ex, "Error", id);
            return new ErrorResponse()
            {
                Id = Guid.NewGuid(),
                Error = Extenders.Concatenar("Ex: ", ex.Message, " inner:", (ex.InnerException == null ? "" : ex.InnerException.Message))
            };
        }
    }
}
