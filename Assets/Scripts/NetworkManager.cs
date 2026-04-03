using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private string roomName = "Room1";
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected");

        PhotonNetwork.JoinOrCreateRoom(roomName,
            new RoomOptions { MaxPlayers = 5 },
            TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");

        int id = PhotonNetwork.LocalPlayer.ActorNumber;

        if (id == 1)

            PhotonNetwork.Instantiate("PacmanPlayer", new Vector3(0, -3, 0), Quaternion.identity);
        else
            PhotonNetwork.Instantiate("GhostPlayer", new Vector3(2, 0, 0), Quaternion.identity);

    }
}