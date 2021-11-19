using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Credits : MonoBehaviour {
  public Button btnBack;
  void Start() {
    btnBack.onClick.AddListener(() => {
      SceneManager.LoadScene("Home");
    });
  }
}
