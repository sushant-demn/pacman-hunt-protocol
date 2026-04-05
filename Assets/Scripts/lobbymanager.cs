using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Photon.Pun.Demo.PunBasics;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class lobbymanager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField roomName;
    [SerializeField] private TextMeshProUGUI roomCode;
    [SerializeField] private TMP_InputField playerName;
    [SerializeField] private Button[] characterButtons;
    [SerializeField] private TextMeshProUGUI[] characterPlayers;
    public GameObject lobbyCanvas;
    public GameObject roomCanvas;
    public GameObject joinCanvas;
    public GameObject hostCanvas;
    public GameObject characterSelectCanvas;

    private PlayerListManager plManager;
    void Start()
    {
        canvasToggle(true, false, false, false, false);
        plManager = GetComponent<PlayerListManager>();
    }

    void canvasToggle(bool lobby, bool room, bool join, bool host, bool characterSelect)
    {
        lobbyCanvas.SetActive(lobby);
        roomCanvas.SetActive(room);
        joinCanvas.SetActive(join);
        hostCanvas.SetActive(host);
        characterSelectCanvas.SetActive(characterSelect);
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
        RefreshCharacterButtons();
        roomCode.text = PhotonNetwork.CurrentRoom.Name;
    }

    public void DisplayJoinCanvas()
    {
        canvasToggle(false, true, true, false, false);
    }

    public void DisplayHostCanvas()
    {
        canvasToggle(false, true, false, true, false);
    }

    public void DisplayCharacterSelectCanvas()
    {
        canvasToggle(false, false, false, false, true);
    }

    public void SelectCharacter(int characterIndex)
    {
        if (!characterButtons[characterIndex].interactable)
        {
            Debug.LogWarning("That character is already taken!");
            return;
        }
        Debug.Log("Selected Character Index: " + characterIndex);
        Hashtable playerCustomProps = new Hashtable();

        // 2. Add our chosen character index to the table under a key named "CharacterIndex"
        playerCustomProps["CharacterIndex"] = characterIndex;

        // 3. Push this data to the Photon network so everyone knows what we picked
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerCustomProps);
    }

    private void RefreshCharacterButtons()
    {
        // 1. Reset: Turn ALL buttons ON first
        for (int i = 0; i < 5; i++)
        {
            characterButtons[i].interactable = true;
            characterPlayers[i].text = "";

        }

        // 2. Loop through every player currently in the room
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            // 3. Check if this player has picked a character yet
            if (player.CustomProperties.ContainsKey("CharacterIndex"))
            {
                int takenIndex = (int)player.CustomProperties["CharacterIndex"];

                // 4. Turn OFF the button for that specific character
                if (takenIndex >= 0 && takenIndex < characterButtons.Length)
                {
                    characterButtons[takenIndex].interactable = false;
                    characterPlayers[takenIndex].text = player.NickName;
                }
            }
        }
    }

    // This triggers automatically for EVERYONE in the room whenever ANYONE changes a property
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        // Example: Check if the property that changed was the CharacterIndex
        if (changedProps.ContainsKey("CharacterIndex"))
        {
            Debug.Log(targetPlayer.NickName + " changed their character to index " + changedProps["CharacterIndex"]);
            RefreshCharacterButtons();

            // Here you could update a UI image next to their name in the lobby to show their new choice!
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        RefreshCharacterButtons();
    }

}
