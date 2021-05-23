using InitializeSystem;
using UnityEngine;

namespace ToolsSystem.Action.Additional
{
	public class AudioDisable : MonoBehaviour, IInitialize<AudioSource>
	{
		public void Assign(AudioSource unitModel)
		{
			_audioSource = unitModel;
		}

		private AudioSource _audioSource;

		private void Update()
		{
			if (!_audioSource.isPlaying)
				gameObject.SetActive(false);
		}
	}
}