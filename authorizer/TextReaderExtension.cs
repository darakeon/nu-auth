using System;
using System.Collections.Generic;
using System.IO;

namespace Authorizer
{
	public static class TextReaderExtension
	{
		public static IList<String> ExtractLines(this TextReader reader)
		{
			String line;
			var lines = new List<String>();

			do
			{
				line = reader.ReadLine();

				if (line != null)
				{
					lines.Add(line);
				}
			} while (line != null);

			return lines;
		}
	}
}