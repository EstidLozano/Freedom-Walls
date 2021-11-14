using UnityEngine;
using UnityEngine.SceneManagement;
public class ManageGame : MonoBehaviour {
  public GameObject player;
  public Follow cameraFollow;
  private bool hasEnded = false;
  private Vector3 playerInitPos;
  void Start() {
    playerInitPos = player.transform.position;
  }
  public void gameOver() {
    if (!hasEnded) {
      hasEnded = true;
      cameraFollow.enabled = false;
      player.GetComponent<Move>().enabled = false;
      Invoke("restart", 2f);
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
    hasEnded = false;
  }
}
