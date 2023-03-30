using App;
using Microsoft.Extensions.Logging;

var handler = new Handler(LoggerFactory.Create(logging => logging.AddConsole()));
handler.Handle_Login();