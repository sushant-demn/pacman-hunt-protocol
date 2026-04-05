using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Photon.Pun.Demo.PunBasics;
public class lobbymanager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField roomName;
    [SerializeField] private TextMeshProUGUI roomCode;
    [SerializeField] private TMP_InputField playerName;
    public GameObject lobbyCanvas;
    public GameObject roomCanvas;
    public GameObject joinCanvas;
    public GameObject hostCanvas;
    public GameObject characterSelectCanvas;

    private PlayerListManager plManager;
    void Start()
    {
        // lobbyCanvas.SetActive(true);
        // roomCanvas.SetActive(false);
        // joinCanvas.SetActive(false);
        // hostCanvas.SetActive(false);
        // characterSelectCanvas.SetActive(false);
        plManager = GetComponent<PlayerListManager>();
    }

    public void HostGame()
    {
        if (string.IsNullOrEmpty(roomName.text) || string.IsNullOrEmpty(playerName.text))
        {
            Debug.LogWarning("Room name is empty");
            return;
        }
        RoomOptions options = new RoomOptions { MaxPlayers = 5 };
        PhotonNetwork.NickName = playerName.text;
        PhotonNetwork.CreateRoom(roomName.text, options);
    }

    public void JoinGame()
    {
        if (string.IsNullOrEmpty(roomName.text) || string.IsNullOrEmpty(playerName.text))
        {
            Debug.LogWarning("Room name is empty");
            return;
        }
        PhotonNetwork.NickName = playerName.text;
        PhotonNetwork.JoinRoom(roomName.text);

    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("joined room" + PhotonNetwork.CurrentRoom.Name);
        DisplayCharacterSelectCanvas();
        plManager.UpdatePlayerListUI();
        roomCode.text = PhotonNetwork.CurrentRoom.Name;
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
