using UnityEngine;
public class PlayerCollision : MonoBehaviour {
  public CameraShake cameraShake;
  Move Move;
  Rigidbody rb;
  void Start() {
    Move = GetComponent<Move>();
    rb = GetComponent<Rigidbody>();
  }
  void OnCollisionEnter(Collision col) {
    string tag = col.gameObject.tag;
    if (tag == "Ground") {
      Move.setInGround(true);
    } else if (tag == "Obstacle" || tag == "Wall") {
      ContactPoint contact = col.contacts[0];
      Vector3 reflect = Vector3.Reflect(rb.velocity, contact.normal);
      rb.velocity = reflect;
      StartCoroutine(cameraShake.shake(1f, 0.1f));
      // rb.rotation = Quaternion.LookRotation(-reflect);
      FindObjectOfType<ManageGame>().gameOver();
    }
  }
  void OnCollisionExit(Collision col) {
    if(col.gameObject.tag == "Ground") {
      Move.setInGround(false);
    }
  }
}
