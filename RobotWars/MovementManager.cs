using System;

namespace RobotWars {

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
}
