using System.Collections.Generic;

namespace DefaultNamespace
{
	public class ChangeOfSizeField
	{
		public List<List<CubeInfo>> ChangeOfSize(List<List<CubeInfo>> list, int height, int wight)
		{
			list.Clear();
			CreateNewField(list, height, wight);
			UpdateBarriers(list);
			return list;
		}

		private void CreateNewField(List<List<CubeInfo>> list, int height, int wight)
		{
			for (int i = 0; i < height + 2; i++)
				list.Add(new List<CubeInfo>());
			
			foreach (var cubeInfos in list)
            {
            	for (int j = 0; j < wight + 2; j++)
            		cubeInfos.Add(new CubeInfo());
            }
		}

		private void UpdateBarriers(List<List<CubeInfo>> list)
		{
			foreach (var cubeInfos in list)
			{
				foreach (var cubeInfo in cubeInfos)
				{
					if (!cubeInfo.IsBarrier)
						continue;
					
					cubeInfo.IsBarrier = false;
				}
			}

			foreach (var cubeInfo  in list[0])
				cubeInfo.IsBarrier = true;

			foreach (var cubeInfo in list[list.Count - 1])
				cubeInfo.IsBarrier = true;

			foreach (var cubeInfos in list)
			{
				cubeInfos[0].IsBarrier = true;
				cubeInfos[cubeInfos.Count - 1].IsBarrier = true;
			}
		}
	}
}