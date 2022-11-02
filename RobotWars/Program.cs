using RobotWars;

Console.WriteLine(Battle.Run("5 5", "1 2 N", "LMLMLMLMM"));
Console.WriteLine(Battle.Run("5 5", "3 3 E", "MMRMMRMRRM"));

Console.Write("\r\nPlease press Enter to exit");
Console.ReadLine();


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
			var result = $"{robot}";
			return result;
		}

	}

	public interface IMovementManager {

		/// <summary>Returns X coordinate within limitations</summary>
		/// <returns>X coordinate within limitations</returns>
		int GetX(int x);

		/// <summary>Returns Y coordinate within limitations</summary>
		/// <returns>Y coordinate within limitations</returns>
		int GetY(int y);
	
	}

	class MovementManager : IMovementManager {

		public MovementManager(string size) {
			var coords = size.Split();
			this.X = int.TryParse(coords[0], out var x) ? x : throw new Exception("Please pass the correct X coordinate of the arena");
			this.Y = int.TryParse(coords[1], out var y) ? y : throw new Exception("Please pass the correct Y coordinate of the arena");
		}

		private int X { get; }
		private int Y { get; }

		/// <inheritdoc/>
		public int GetX(int x) {
			var result = Math.Min(x, this.X);
			return Math.Max(result, 0);
		}

		/// <inheritdoc/>
		public int GetY(int y) {
			var result = Math.Min(y, this.Y);
			return Math.Max(result, 0);
		}
	}

	class Robot {
		private readonly IMovementManager _moveMgr;

		public Robot(IMovementManager moveMgr, string startPos) {
			_moveMgr = moveMgr;
			var coords = startPos.Split(); // TODO: check format
			this.X = int.TryParse(coords[0], out var x) ? x : throw new Exception("Please pass the correct X coordinate for the robot placement");
			this.Y = int.TryParse(coords[1], out var y) ? y : throw new Exception("Please pass the correct Y coordinate for the robot placement");
			this.Direction = Enum.TryParse<Directions>(coords[2], out var direction)
				? direction
				: throw new Exception("Please pass correct direction for robot placement");
		}

		public int X { get; private set; }
		public int Y { get; private set; }
		public Directions Direction { get; private set; }

		public void Execute(Instructions instruction) {
			switch (instruction) {
				case Instructions.L:
					this.TurnLeft();
					break;
				case Instructions.R:
					this.TurnRight();
					break;
				default:
					this.Move();
					break;
			}
		}

		public override string ToString() => $"{this.X} {this.Y} {this.Direction}";

		private void Move() {
			this.X += this.Direction switch {
				Directions.E => 1,
				Directions.W => -1,
				_ => 0
			};
			this.X = _moveMgr.GetX(this.X);
			this.Y += this.Direction switch {
				Directions.N => 1,
				Directions.S => -1,
				_ => 0,
			};
			this.Y = _moveMgr.GetY(this.Y);
		}

		private void TurnLeft() => this.Direction = this.Direction switch {
			Directions.N => Directions.W,
			Directions.E => Directions.N,
			Directions.S => Directions.E,
			Directions.W => Directions.S,
			_ => throw new NotImplementedException()
		};

		private void TurnRight() => this.Direction = this.Direction switch {
			Directions.N => Directions.E,
			Directions.E => Directions.S,
			Directions.S => Directions.W,
			Directions.W => Directions.N,
			_ => throw new NotImplementedException()
		};
	}

	public enum Directions { N, E, S, W }

	public enum Instructions { L, R, M }
}