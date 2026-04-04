using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class lobbymanager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField roomName;
    [SerializeField] private TMP_InputField playerName;
    public GameObject lobbyCanvas;
    public GameObject roomCanvas;
    public GameObject joinCanvas;
    public GameObject hostCanvas;
    public GameObject characterSelectCanvas;
    void Start()
    {
        lobbyCanvas.SetActive(true);
        roomCanvas.SetActive(false);
        joinCanvas.SetActive(false);
        hostCanvas.SetActive(false);
        characterSelectCanvas.SetActive(false);
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

    public void DisplayJoinCanvas()
    {

        roomCanvas.SetActive(true);
        joinCanvas.SetActive(true);
        lobbyCanvas.SetActive(false);
        hostCanvas.SetActive(false);
        characterSelectCanvas.SetActive(false);
    }

    public void DisplayHostCanvas()
    {
        roomCanvas.SetActive(true);
        hostCanvas.SetActive(true);
        joinCanvas.SetActive(false);
        lobbyCanvas.SetActive(false);
        characterSelectCanvas.SetActive(false);
    }

    public void DisplayCharacterSelectCanvas()
    {
        characterSelectCanvas.SetActive(true);
        roomCanvas.SetActive(false);
        joinCanvas.SetActive(false);
        lobbyCanvas.SetActive(false);
        hostCanvas.SetActive(false);
    }
}
