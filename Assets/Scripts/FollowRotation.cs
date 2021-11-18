using UnityEngine;

public class FollowRotation : MonoBehaviour {
  public Transform target;
  void Update() {
    transform.rotation = Quaternion.LookRotation(target.position - transform.position);
  }
}
