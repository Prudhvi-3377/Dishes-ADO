using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace ADODISHES.Filter
{
	public class CustomFilter: ActionFilterAttribute
	{
		private readonly Stopwatch _stopwatch = new Stopwatch();
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			
				_stopwatch.Start();
			
		}
		public override void OnActionExecuted(ActionExecutedContext context)
		{
			_stopwatch.Stop();
			var elapsedTime = _stopwatch.ElapsedMilliseconds;
			Console.WriteLine($"Action executed in {elapsedTime} ms and executed on {context.ActionDescriptor.DisplayName}");

		}
	}
}
