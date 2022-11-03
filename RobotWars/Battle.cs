using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotWars {
	public static class Battle {

		public static string Run(string size, string startPos, string instructionLine) {
			var moveMgr = new MovementManager(size);
			var robot = new Robot(moveMgr, startPos);
			foreach (var instructionLetter in instructionLine) { // TODO: simplify
				var code = instructionLetter.ToString();
				var instruction = Enum.TryParse<Instructions>(code, out var ins)
					? ins
					: throw new Exception($"Please pass the correct instruction instead {code}");
				robot.Execute(instruction);
			}
			return $"{robot}";
		}

	}
}
