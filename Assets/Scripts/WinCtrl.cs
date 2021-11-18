using UnityEngine;
using UnityEngine.UI;

public class WinCtrl : MonoBehaviour {
  public Text txtScore;
  public Text txtRecord;
  void Start() {
    float score = CvsCtrl.time;
    float record = PlayerPrefs.GetFloat("highscore");
    txtScore.text = "Score: " +  score.ToString("F2");
    txtRecord.text = "Record: " +  record.ToString("F2");
  }
}
