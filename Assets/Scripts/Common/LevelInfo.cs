using System;
using System.Collections.Generic;

namespace DefaultNamespace
{
	[Serializable]
	public class LevelInfo
	{
		public List<List<CubeInfo>> List;
		public int HeightStartPoint;
		public int WidthStartPoint;

		public LevelInfo(List<List<CubeInfo>> list, int heightStartPoint, int widthStartPoint)
		{
			List = list;
			HeightStartPoint = heightStartPoint;
			WidthStartPoint = widthStartPoint;
		}
	}
}