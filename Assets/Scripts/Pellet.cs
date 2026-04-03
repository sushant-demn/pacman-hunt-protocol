using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Collider2D))]
public class Pellet : MonoBehaviour
{
    public int points = 10;

    protected virtual void Eat()
    {
        GameManager.Instance.PelletEaten(points);
        gameObject.SetActive(false); // THIS WAS MISSING
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PhotonView pv = other.GetComponent<PhotonView>();

        if (pv == null || !pv.IsMine) return;

        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Eat();
        }
    }

}
