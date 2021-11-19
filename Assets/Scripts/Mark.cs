using UnityEngine;
using System.Collections.Generic;
public class Mark : MonoBehaviour {
  public GameObject markPrefab;
  private LinkedList<GameObject> marks;
  void Start() {
    marks = new LinkedList<GameObject>();
  }
  void Update() {
    if (Input.GetKeyDown("v")) {
      if (marks.Count == 0) {
        Instantiate(markPrefab, transform.position, Quaternion.identity);
      } else {
        Destroy(marks.First.Value);
        marks.RemoveFirst();
      }
    }
  }
  void OnTriggerEnter(Collider col) {
    if (col.gameObject.tag == "Mark") {
      marks.AddLast(col.gameObject);
    }
  }
  void OnTriggerExit(Collider col) {
    if (col.gameObject.tag == "Mark") {
      marks.Remove(col.gameObject);
    }
  }
}
