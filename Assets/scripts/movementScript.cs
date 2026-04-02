using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class movementScript : MonoBehaviour
{
    public float speed = 8.0f;
    public float speedMultiplier = 1.0f;

    public Vector2 initDirection;
    public LayerMask walls;
    public Rigidbody2D someRigidBody { get; private set; }

    public Vector2 direction { get; private set; }

    public Vector2 nextDirection { get; private set; }

    public Vector3 startingPostion { get; private set; }
    private void Awake()
    {
        this.someRigidBody = GetComponent<Rigidbody2D>();
        this.startingPostion = this.transform.position;
    }

    private void Start()
    {
        resetState();
    }
    public void resetState()
    {
        this.speedMultiplier = 1.0f;
        this.direction = this.initDirection;
        this.nextDirection = Vector2.zero;
        this.transform.position = this.startingPostion;
        this.enabled = true;
    }

    private void Update()
    {
        if(nextDirection != Vector2.zero)
        {
            setDirection(nextDirection);
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = this.someRigidBody.position;
        Vector2 translation = this.direction * this.speedMultiplier * this.speed * Time.fixedDeltaTime;
        this.someRigidBody.MovePosition(position + translation);
    }

    public void setDirection(Vector2 direction, bool forced = false)
    {
        if (forced || !checkOccupiedTile(direction))
        {
            this.direction = direction;
            nextDirection = Vector2.zero;
        }
        else
        {
            nextDirection = direction;
        }
    }

    public bool checkOccupiedTile(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0f, direction, 1.5f, this.walls);
        return hit.collider != null;
        // return true;
    }

}
