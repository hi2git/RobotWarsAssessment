using System;

namespace RobotWars {
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

	enum Directions { N, E, S, W }

	enum Instructions { L, R, M }
}
