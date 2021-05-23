using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
	public class UIManagerMenu : MonoBehaviour
	{
		public void LoadScene(int numberScene) => SceneManager.LoadScene(numberScene);
		public void Exit() => Application.Quit();
	}
}