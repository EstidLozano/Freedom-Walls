using UnityEngine;

public class AutoTile : MonoBehaviour {
  public Vector2 tileSize = new Vector2(1, 1);
  void Start() {
    Renderer renderer = GetComponent<Renderer>();
    Vector2 textureScale = renderer.material.mainTextureScale;
    textureScale.x = transform.localScale.x / tileSize.x;
    textureScale.y = transform.localScale.y / tileSize.y;
    renderer.material.mainTextureScale = textureScale;
  }
}
