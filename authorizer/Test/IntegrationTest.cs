using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
			var inputPaths = Directory.GetFiles("scenarios", "*input");
			var errors = new List<Exception>();

			foreach (var inputPath in inputPaths)
			{
				try
				{
					testInput(inputPath);
				}
				catch (Exception e)
				{
					errors.Add(e);
				}
			}

			if (errors.Any())
			{
				throw new AggregateException(errors);
			}
		}

		private static void testInput(String inputPath)
		{
			var name = inputPath.Substring(0, inputPath.Length - 6);
			var expectedOutputPath = $"{name}_output";

			var input = File.ReadAllLines(inputPath);
			var expectedOutputs = File.ReadAllLines(expectedOutputPath);

			var processor = new Processor(input);

			var actualOutputs = processor.Interpret().ToArray();

			Assert.AreEqual(
				expectedOutputs.Length, actualOutputs.Length,
				$"\n\nCase {name}" +
				$"\nExpected: {expectedOutputs.Length} lines" +
				$"\nActual: {actualOutputs.Length} lines"
			);

			for (var o = 0; o < expectedOutputs.Length; o++)
			{
				var expectedOutput = expectedOutputs[o];
				var actualOutput = actualOutputs[o];
				Assert.AreEqual(
					expectedOutput, actualOutput,
					$"\n\nCase {name}, line {o + 1}:" +
					$"\nExpected: {expectedOutput}" +
					$"\nActual: {actualOutput}"
				);
			}
		}
	}
}