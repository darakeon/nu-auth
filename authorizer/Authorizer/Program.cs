using System;
using System.IO;
using System.Linq;

namespace Authorizer
{
	class Program
	{
		public static int Main(String[] args)
		{
			var input = Console.In.ExtractLines();

			if (!input.Any())
			{
				if (args.Length == 0)
				{
					Console.WriteLine("No content received");
					return 1;
				}

				var path = args[0];
				Console.WriteLine(path);

				if (!File.Exists(path))
				{
					Console.WriteLine("No content received");
					return 1;
				}

				input = File.ReadAllLines(path);
			}
			
			var output = new Processor(input).Interpret();

			foreach (var line in output)
			{
				Console.WriteLine(line);
			}

			return 0;
		}
	}
}
