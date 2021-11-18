using UnityEngine;
using UnityEngine.SceneManagement;
public class ManageGame : MonoBehaviour {
  public GameObject player;
  public Follow cameraFollow;
  public CvsCtrl cvsCtrl;
  public bool playing = true;
  private Vector3 playerInitPos;
  void Start() {
    playerInitPos = player.transform.position;
  }
  public void gameOver() {
    if (playing) {
      playing = false;
      cameraFollow.enabled = false;
      player.GetComponent<Move>().enabled = false;
      Invoke("restart", 2f);
    }
  }
  public void finish() {
    if (playing) {
      playing = false;
      cvsCtrl.enabled = false;
      float score = CvsCtrl.time;
      float record = PlayerPrefs.GetFloat("highscore", -1);
      if (score < record || record == -1) {
        PlayerPrefs.SetFloat("highscore", score);
      }
      SceneManager.LoadScene("Win");
    }
  }
  void restart() {
    player.transform.position = playerInitPos;
    player.transform.rotation = Quaternion.identity;
    Rigidbody rb = player.GetComponent<Rigidbody>();
    rb.velocity = Vector3.zero;
    rb.angularVelocity = Vector3.zero;
    cameraFollow.enabled = true;
    player.GetComponent<Move>().enabled = true;
    cvsCtrl.enabled = true;
    playing = false;
  }
}
