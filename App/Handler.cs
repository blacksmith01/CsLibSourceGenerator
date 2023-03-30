using LibGame;
using LibGame.Message;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public class Handler
    {
        ILogger<Handler> _logger;
        public Handler(ILoggerFactory factory)
        {
            _logger = factory.CreateLogger<Handler>();
        }

        public ErrorCodes Handle_Login()
        {
            var p1 = GameDefines.ToEntityTypes(EntityTypeFlags.Player);
            var p2 = GameDefines.ToEntityTypeFlags(EntityTypes.Player);
            var p3 = ErrorCodeNames.Auth_Login_InvalidGameVersion;

            _logger.Info("info p0");
            _logger.Info("info p1 {}", p1);
            _logger.Info("info p2 {}, {}", p1, p2);
            _logger.Info("info p3 {}, {}, {}", p1, p2, p3);

            return ErrorCodes.Srv_Auth_Login_InvalidGameVersion;
        }
    }
}
