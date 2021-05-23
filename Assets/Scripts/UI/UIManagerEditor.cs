using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
	public class UIManagerEditor : MonoBehaviour
	{
		[SerializeField] private CraftEditor _craftEditor;
		[SerializeField] private InputField _heightInputField;
		[SerializeField] private InputField _widthInputField;
		[SerializeField] private InputField _levelInputField;

		public void SetSize() => _craftEditor.ChangeOfSize(int.Parse(_heightInputField.text), int.Parse(_widthInputField.text));
		public void CallBarrier() => _craftEditor.AddElementsToFieldManager.CallBarrier();
		public void CallStartPoint() => _craftEditor.AddElementsToFieldManager.CallStartPoint();
		public void Save() => _craftEditor.Save(int.Parse(_levelInputField.text));
		public void Exit() => SceneManager.LoadScene(0);
	}
}