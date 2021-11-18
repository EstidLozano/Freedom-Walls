using UnityEngine;
public class Follow : MonoBehaviour {
  public Transform target;
  public Move playerMove;
  public float rotSpeed = 1f, movSpeed = 1f;
  public Vector3 offset;
  void LateUpdate() {
    float dt = Mathf.Clamp(Time.deltaTime, 0.0001f, 0.1f);
    Vector3 pos = target.position + (target.rotation * offset);
    transform.position = Vector3.Lerp(transform.position, pos, dt * movSpeed);
    transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, dt * rotSpeed);
  }
}