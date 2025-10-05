using System.Collections.Generic;

namespace BlockGame.New.Core
{
	public class StageConfig
	{
		public int rowSize;

		public int colSize;

		public int obstacleFrenquency;

		public Dictionary<int, int> scoreBase;

		public Dictionary<int, Dictionary<int, int>> difficultyLevel;
	}
}
