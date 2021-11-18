using UnityEngine;
public class Move : MonoBehaviour {
  private float mouseSensitivity = 50f;
  private float speed = 200f;
  private Rigidbody rb;
  void Start() {
    rb = GetComponent<Rigidbody>();
  }
  void Update() {
    Vector3 mov = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    rb.velocity = transform.rotation * mov * speed * Time.deltaTime;
    Vector3 rot = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
    transform.Rotate(rot * mouseSensitivity * Time.deltaTime);
  }
}
