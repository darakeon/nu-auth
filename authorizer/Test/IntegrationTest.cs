using System.IO;
using Authorizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
	[TestClass]
	public class IntegrationTest
	{
		[TestMethod]
		public void TestAllFiles()
		{
			var inputPaths = Directory.GetFiles("scenarios", "input*");

			foreach (var inputPath in inputPaths)
			{
				var expectedOutputPath = inputPath.Replace("input_", "output_");

				var input = File.ReadAllLines(inputPath);
				var expectedOutputs = File.ReadAllLines(expectedOutputPath);

				var processor = new Processor(input);

				var actualOutputs = processor.Interpret();

				Assert.AreEqual(expectedOutputs.Length, actualOutputs.Count);

				for (var o = 0; o < expectedOutputs.Length; o++)
				{
					var expectedOutput = expectedOutputs[o];
					var actualOutput = actualOutputs[o];
					Assert.AreEqual(expectedOutput, actualOutput);
				}
			}
		}
	}
}
