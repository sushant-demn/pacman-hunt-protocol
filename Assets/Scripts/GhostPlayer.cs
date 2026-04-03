using Photon.Pun;
using UnityEngine;

public class GhostPlayer : MonoBehaviourPun
{
    private Movement movement;

    private void Awake()
    {
        movement = GetComponent<Movement>();
    }

    void Update()
    {
        if (!photonView.IsMine) return;

        if (Input.GetKey(KeyCode.UpArrow))
            movement.SetDirection(Vector2.up);
        else if (Input.GetKey(KeyCode.DownArrow))
            movement.SetDirection(Vector2.down);
        else if (Input.GetKey(KeyCode.LeftArrow))
            movement.SetDirection(Vector2.left);
        else if (Input.GetKey(KeyCode.RightArrow))
            movement.SetDirection(Vector2.right);
    }
}