using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CvsCtrl : MonoBehaviour {
  public Text txtTime;
  public Button btnPause;
  public Image panel;
  public Scrollbar scrollMusic, scrollSensivity;
  public Button btnHome;
  public static float time = 0;
  void Start() {
    time = 0;
    txtTime.text = "0";
    panel.gameObject.SetActive(false);
    btnPause.onClick.AddListener(() => {
      if (Time.timeScale == 1) {
        Time.timeScale = 0;
        btnPause.GetComponentInChildren<Text>().text = ">";
        panel.gameObject.SetActive(true);
      } else {
        Time.timeScale = 1;
        btnPause.GetComponentInChildren<Text>().text = "| |";
        panel.gameObject.SetActive(false);
      }
    });
    btnHome.onClick.AddListener(() => {
      Time.timeScale = 1;
      SceneManager.LoadScene("Home");
    });

    scrollMusic.value = PlayerPrefs.GetFloat("Music", 0.5f);
    AudioListener.volume = scrollMusic.value;
    scrollMusic.onValueChanged.AddListener((float value) => {
      AudioListener.volume = value;
      PlayerPrefs.SetFloat("Music", value);
    });
    scrollSensivity.value = PlayerPrefs.GetFloat("MouseSensivity", 0.5f);
    Move.mouseSensitivity = scrollSensivity.value * 50f + 25f;
    scrollSensivity.onValueChanged.AddListener((float value) => {
      Move.mouseSensitivity = value * 50f + 25f;
      PlayerPrefs.SetFloat("MouseSensivity", Move.mouseSensitivity);
    });

  }
  void Update() {
    time += Time.deltaTime;
    txtTime.text = time.ToString("F2");
  }
}
