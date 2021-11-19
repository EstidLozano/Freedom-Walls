using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinCtrl : MonoBehaviour {
  public Text txtScore;
  public Text txtRecord;
  public Button btnPlay, btnBack;
  void Start() {
    float score = CvsCtrl.time;
    string key = "Time" + MazeGenerator.size;
    float record = PlayerPrefs.GetFloat(key);
    txtScore.text = "Tiempo: " +  score.ToString("F2");
    txtRecord.text = "Mejor Tiempo: " +  record.ToString("F2");
    btnPlay.onClick.AddListener(() => {
      SceneManager.LoadScene("Game");
    });
    btnBack.onClick.AddListener(() => {
      SceneManager.LoadScene("Home");
    });
  }
}
