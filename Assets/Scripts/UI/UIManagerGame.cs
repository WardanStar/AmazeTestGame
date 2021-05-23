using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
	public class UIManagerGame : MonoBehaviour
	{
		[SerializeField] private GameController _gameController;
		[SerializeField] private Text _levelText;
		[SerializeField] private Text _tapText;
		[SerializeField] private Button _tapButton;
		
		public void Exit() => SceneManager.LoadScene(0);

		public void LoadLevel()
		{
			_levelText.text = $"Level {_gameController.Level}";
			_tapText.gameObject.SetActive(false);
			_tapButton.gameObject.SetActive(false);
			_gameController.LoadLevel();
		} 
		
		private void Start()
		{
			_gameController.DoWin += OnGameControllerDoWin;
		}

		private void OnGameControllerDoWin(int level)
		{
			_levelText.text = $"Level {level} Competed!";
			_tapText.gameObject.SetActive(true);
			_tapButton.gameObject.SetActive(true);
		}
	}
}