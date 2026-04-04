using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class lobbymanager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField roomName;

    void Start()
    {

    }

    public void HostGame()
    {
        if (string.IsNullOrEmpty(roomName.text))
        {
            Debug.LogWarning("Room name is empty");
            return;
        }
        RoomOptions options = new RoomOptions { MaxPlayers = 5 };
        PhotonNetwork.CreateRoom(roomName.text, options);
    }

    public void JoinGame()
    {
        if (string.IsNullOrEmpty(roomName.text))
        {
            Debug.LogWarning("Room name is empty");
            return;
        }
        PhotonNetwork.JoinRoom(roomName.text);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("joined room" + PhotonNetwork.CurrentRoom.Name);
    }
}
