using NUnit.Framework;

namespace RobotWars.Tests {

	[TestFixture]
	public class RunTests {


		[Test]
		[TestCase("5 5", "1 2 N", "LMLMLMLMM", "1 3 N")]
		[TestCase("5 5", "3 3 E", "MMRMMRMRRM", "5 1 E")]
		[TestCase("5 7", "3 3 N", "MMMMMMMMMM", "3 7 N")]
		[TestCase("3 5", "3 3 W", "MMMMMMMMMM", "0 3 W")]
		public void RunTest(string size, string position, string instructionLine, string expected) { 
			var actual = Battle.Run(size, position, instructionLine);
			Assert.That(actual, Is.EqualTo((expected)));
		}

	}
}