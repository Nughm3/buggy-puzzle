using UnityEngine;

public class ZIndicator : MonoBehaviour
{
    public static Vector3 pos;
    public static bool show = false;
    SpriteRenderer sprite;

    void Awake() {
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update() {
        transform.position = pos;
        if (show) sprite.color = new Color(1,1,1,1);
        else sprite.color = new Color(1,1,1,0);
    }

    public void Reset() {
        show = false;
        sprite.color = new Color(1,1,1,0);
    }

}