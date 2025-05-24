using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace ADODISHES.Filter
{
	public class CustomFilter : ActionFilterAttribute
	{
		private readonly Stopwatch stopwatch = new Stopwatch();


		public override void OnActionExecuting(ActionExecutingContext context)
		{
			stopwatch.Start();
		}

		public override void OnActionExecuted(ActionExecutedContext context)
		{
			stopwatch.Stop();
			var elapsedTime = stopwatch.ElapsedMilliseconds;
			Console.WriteLine($"Action {context.ActionDescriptor.DisplayName} executed in {elapsedTime} ms");

		}
	}
}
