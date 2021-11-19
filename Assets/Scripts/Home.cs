using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Home : MonoBehaviour {
  public Button btnPlay, btnTutorial, btnCredits, btnExit;
  void Start() {
    btnPlay.onClick.AddListener(() => {
      SceneManager.LoadScene("Game");
    });
    btnTutorial.onClick.AddListener(() => {
      SceneManager.LoadScene("Tutorial");
    });
    btnCredits.onClick.AddListener(() => {
      SceneManager.LoadScene("Credits");
    });
    btnExit.onClick.AddListener(() => {
      Application.Quit();
    });
  }
}
