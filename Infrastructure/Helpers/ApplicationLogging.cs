using Infrastructure.DTO.V1.Response;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helpers
{
    public class ApplicationLogging
    {
        private static Logger logger { get; } = LogManager.GetCurrentClassLogger();

        public static ErrorResponse RegistrarError(Exception ex)
        {
            Guid id = Guid.NewGuid();
            logger.Error(ex, $"Error - {id}");
            return new ErrorResponse() { Id = id, Error = $"Error interno en el servidor. Consulte con soporte el código de error. {ex.Message}" };
        }
    }
}
