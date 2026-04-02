using UnityEngine;


[RequireComponent(typeof(movementScript))]
public class pacman : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;

    public movementScript movement { get; private set; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        this.movement = GetComponent<movementScript>();
    }

    private void Update()
    {
        // Set the new direction based on the current input
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            movement.setDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            movement.setDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            movement.setDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            movement.setDirection(Vector2.right);
        }

        // Rotate pacman to face the movement direction
        float angle = Mathf.Atan2(movement.direction.y, movement.direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }
}
