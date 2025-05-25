using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ADODISHES.Filter
{
	public class CustomFilterinterface : IAsyncActionFilter
	{
		private readonly ILogger<CustomFilterinterface> _logger;
		private readonly Stopwatch _stopwatch = new Stopwatch();


		public CustomFilterinterface(ILogger<CustomFilterinterface> logger)
		{

			_logger = logger;
		}
	public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			_stopwatch.Start();
			_logger.LogInformation("Action execution started at: {time}", DateTime.UtcNow);
			// Proceed to the next action filter or action method
			 await next();
			// After the action method has executed
			_stopwatch.Stop();
			_logger.LogInformation("Action execution finished at: {time}", DateTime.UtcNow);
			_logger.LogInformation("Action execution duration: {duration} ms", _stopwatch.ElapsedMilliseconds);
		}
	}
}

