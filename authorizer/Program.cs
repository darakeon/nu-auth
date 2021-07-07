using System;

namespace Authorizer
{
	class Program
	{
		public static void Main(String[] args)
		{
			var lines = Console.In.ExtractLines();

			var converter = new Processor();

			foreach (var line in lines)
			{
				converter.Interpret(line);
			}
		}
	}
}
