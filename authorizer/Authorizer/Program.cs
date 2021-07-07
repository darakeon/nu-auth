using System;

namespace Authorizer
{
	class Program
	{
		public static void Main(String[] args)
		{
			var input = Console.In.ExtractLines();
			
			var output = new Processor(input).Interpret();

			foreach (var line in output)
			{
				Console.WriteLine(line);
			}
		}
	}
}
