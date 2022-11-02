using NUnit.Framework;

using RobotWars;

namespace RobotWars.Tests {

	[TestFixture]
	public class RunTests {


		[Test]
		[TestCase("5 5", "1 2 N", "LMLMLMLMM", "1 3 N")]
		[TestCase("5 5", "3 3 E", "MMRMMRMRRM", "5 1 E")]
		public void RunTest(string size, string position, string instructionLine, string expected) { 
			var actual = Battle.Run(size, position, instructionLine);
			Assert.That(actual, Is.EqualTo((expected)));
		}

	}
}