using System.Collections;
using UnityEngine;
public class Move : MonoBehaviour {
  public float mouseSensitivity = 100f;
  public float yRot = 0;
  Rigidbody rb;
  float speed = 500f;
  Vector3 jumpForce = 9f * Vector3.up;
  bool inGround;
  void Start() {
    rb = GetComponent<Rigidbody>();
    inGround = true;
  }
  void Update() {
    Vector3 mov = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    Vector3 vel = Quaternion.Euler(0, yRot, 0) * mov * speed * Time.deltaTime;
    vel.y = rb.velocity.y;
    rb.velocity = vel;
    if (Input.GetKeyDown(KeyCode.Space) && inGround) {
      rb.AddForce(jumpForce, ForceMode.Impulse);
    }
    float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
    yRot += mouseX;
    transform.RotateAround(transform.position, Vector3.up, mouseX);
  }
  public void setInGround(bool inGround) {
    this.inGround = inGround;
  }
}
