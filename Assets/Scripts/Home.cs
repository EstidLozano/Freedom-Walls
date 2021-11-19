using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Home : MonoBehaviour {
  public Button btnPlay, btnSettings, btnCredits, btnExit;
  void Start() {
    btnPlay.onClick.AddListener(() => {
      SceneManager.LoadScene("Game");
    });
    btnCredits.onClick.AddListener(() => {
      SceneManager.LoadScene("Credits");
    });
    btnExit.onClick.AddListener(() => {
      Application.Quit();
    });
  }
}
