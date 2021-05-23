using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace DefaultNamespace
{
	public class ResourcesLevelManager
	{
		private readonly string _pathToSave;
		private Dictionary<int, string> _dictionary;
		
		public ResourcesLevelManager(string pathToSave)
		{
			_pathToSave = pathToSave;
			var levelsPath = Path.Combine(Application.dataPath, pathToSave);
			if (!File.Exists(levelsPath))
			{
				Debug.LogError("Levels file not found");
				return;
			}
			
			_dictionary = JsonConvert.DeserializeObject<Dictionary<int, string>>(File.ReadAllText(levelsPath));
		}

		public void AddNewLevel(int level, LevelInfo levelInfo)
		{
			var serializeLevelInfo = JsonConvert.SerializeObject(levelInfo);

			if (ReferenceEquals(_dictionary, null))
			{
				_dictionary = new Dictionary<int, string>();
				_dictionary.Add(level, serializeLevelInfo);
				return;
			}
			
			foreach (int pair in _dictionary.Keys)
			{
				if (pair != level)
					continue;

				_dictionary[pair] = serializeLevelInfo;
				return;
			}
			
			_dictionary.Add(level, serializeLevelInfo);
		}

		public LevelInfo GetLevel(ref int level)
		{
			int currentLevelKey = -1;
			
			try
			{
				currentLevelKey = _dictionary.Keys.OrderBy(key => key).Skip(level - 1).First();
			}
			catch (InvalidOperationException)
			{
				currentLevelKey = _dictionary.Keys.First();
				level = 1;
			}
			var levelInfo = _dictionary[currentLevelKey];

			if (ReferenceEquals(levelInfo, null))
			{
				Debug.Log("There is no level for this key");
				return null;
			}

			var deserializeLevelInfo = JsonConvert.DeserializeObject<LevelInfo>(levelInfo);
			
			return deserializeLevelInfo;
		}

		public void Save()
		{
			var data = JsonConvert.SerializeObject(_dictionary);
			File.WriteAllText(Path.Combine(Application.dataPath, _pathToSave), data);
		}
	}
}