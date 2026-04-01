using UnityEngine;
//using Photon.Pun;

public class PlayerMovement : MonoBehaviour //MonoBehaviourPun
{
    public float speed = 5f;
    public Rigidbody2D myRigidbody;

    void Update()
    {
        //if (!photonView.IsMine) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(h, v);
        myRigidbody.linearVelocity = move * speed;
    }
}