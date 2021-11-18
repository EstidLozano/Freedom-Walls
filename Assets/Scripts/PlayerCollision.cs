using UnityEngine;
public class PlayerCollision : MonoBehaviour {
  public CameraShake cameraShake;
  public ParticleSystem particleRay;
  Move Move;
  Rigidbody rb;
  void Start() {
    Move = GetComponent<Move>();
    rb = GetComponent<Rigidbody>();
  }
  void OnCollisionEnter(Collision col) {
    string tag = col.gameObject.tag;
    if (tag == "Wall") {
      ContactPoint contact = col.contacts[0];
      Vector3 reflect = Vector3.Reflect(rb.velocity, contact.normal);
      rb.velocity = reflect;
      // rb.rotation = Quaternion.LookRotation(-reflect);
      StartCoroutine(cameraShake.shake(1f, 0.1f));
      FindObjectOfType<ManageGame>().gameOver();
    }
  }

  void OnTriggerEnter(Collider col) {
    string tag = col.gameObject.tag;
    if (tag == "Ray") {
      // Instantiate(particleRay, col.ClosestPointOnBounds(transform.position), Quaternion.identity);
      FindObjectOfType<ManageGame>().gameOver();
    } else if (tag == "Finish") {
      FindObjectOfType<ManageGame>().finish();
    }
  }
}
