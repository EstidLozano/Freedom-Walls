using UnityEngine;
using UnityEngine.UI;
public class CvsCtrl : MonoBehaviour {
  public Text txtTime;
  public static float time = 0;
  void Start() {
    time = 0;
    txtTime.text = "0";
  }
  void Update() {
    time += Time.deltaTime;
    txtTime.text = time.ToString("F2");
  }
}
