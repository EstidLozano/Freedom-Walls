using UnityEngine;
public class RayCtrl : MonoBehaviour {
  private Vector3 initPos;
  private float frecuency = 1.5f;
  private float magnitude = 4.5f;
  void Start() {
    initPos = transform.position;
  }
  void Update() {
    transform.position = initPos + transform.right * Mathf.Sin(Time.time * frecuency) * magnitude;
  }
}

  
