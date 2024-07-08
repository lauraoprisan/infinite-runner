using UnityEngine;

public class MoveForward : MonoBehaviour {
    [SerializeField] private float speed;
    private Vector2 direction = Vector2.left;

    public void SetSpeed(float newSpeed) {
        speed = newSpeed;
    }

    public void SetDirection(Vector2 newDirection) {
        direction = newDirection.normalized;
    }

    void Update() {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}