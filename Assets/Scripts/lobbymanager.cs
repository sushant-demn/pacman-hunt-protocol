using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("Input Fields")]
    [SerializeField] private TMP_InputField roomName;
    [SerializeField] private TMP_InputField playerName;

    [Header("Room UI")]
    [SerializeField] private TextMeshProUGUI roomCode;

    [Header("Character Selection")]
    [SerializeField] private Button[] characterButtons;
    [SerializeField] private Button sButton;
    private GameObject startButton;
    [SerializeField] private TextMeshProUGUI[] characterPlayers;

    [Header("Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject lobbyPanel;
    [SerializeField] private GameObject hostPanel;
    [SerializeField] private GameObject joinPanel;
    [SerializeField] private GameObject roomPanel;

    [SerializeField] private PacmanSelector pacman;

    private PlayerListManager plManager;

    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        ShowPanel(mainMenuPanel);
        startButton = sButton.gameObject;
        startButton.SetActive(false);
        plManager = GetComponent<PlayerListManager>();

        if (plManager == null)
        {
            Debug.LogError("PlayerListManager is missing on this GameObject!");
        }
    }

    // =========================
    // PANEL CONTROL (STATE SYSTEM)
    // =========================
    void ShowPanel(GameObject panelToShow)
    {
        mainMenuPanel.SetActive(false);
        lobbyPanel.SetActive(false);
        hostPanel.SetActive(false);
        joinPanel.SetActive(false);
        roomPanel.SetActive(false);

        panelToShow.SetActive(true);

        if (pacman != null)
            pacman.gameObject.SetActive(false);
    }

    // =========================
    // MAIN MENU
    // =========================
    public void PlayGame()
    {
        ShowPanel(lobbyPanel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // =========================
    // NAVIGATION
    // =========================
    public void GoBackMainMenu()
    {
        ShowPanel(mainMenuPanel);
    }

    public void GoBackLobby()
    {
        ShowPanel(lobbyPanel);
    }

    public void GoToHost()
    {
        ShowPanel(hostPanel);
    }

    public void GoToJoin()
    {
        ShowPanel(joinPanel);
    }

    // =========================
    // NETWORKING
    // =========================
    public void HostGame()
    {
        if (string.IsNullOrEmpty(roomName.text) || string.IsNullOrEmpty(playerName.text))
        {
            Debug.LogWarning("HostGame failed: Missing input");
            return;
        }

        PhotonNetwork.NickName = playerName.text;

        RoomOptions options = new RoomOptions
        {
            MaxPlayers = 5
        };

        PhotonNetwork.CreateRoom(roomName.text, options);
    }

    public void JoinGame()
    {
        if (string.IsNullOrEmpty(roomName.text) || string.IsNullOrEmpty(playerName.text))
        {
            Debug.LogWarning("JoinGame failed: Missing input");
            return;
        }

        PhotonNetwork.NickName = playerName.text;

        PhotonNetwork.JoinRoom(roomName.text);
    }
    //to load a level
    public void toggleStart()
    {
        PhotonNetwork.LoadLevel("Pacman");
    }

    // =========================
    // PHOTON CALLBACKS
    // =========================
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room: " + PhotonNetwork.CurrentRoom.Name);

        ShowPanel(roomPanel);

        roomCode.text = PhotonNetwork.CurrentRoom.Name;

        if (plManager != null)
            plManager.UpdatePlayerListUI();

        RefreshCharacterButtons();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Join Room Failed: " + message);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Create Room Failed: " + message);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RefreshCharacterButtons();
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (changedProps.ContainsKey("CharacterIndex"))
        {
            Debug.Log(targetPlayer.NickName + " selected character " + changedProps["CharacterIndex"]);
            RefreshCharacterButtons();
        }
    }

    // =========================
    // CHARACTER SELECTION
    // =========================
    public void SelectCharacter(int characterIndex)
    {
        int currentChoice = -1;
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("CharacterIndex"))
            currentChoice = (int)PhotonNetwork.LocalPlayer.CustomProperties["CharacterIndex"];

        if (currentChoice == characterIndex)
        {
            Debug.Log("Deselecting character...");
            Hashtable clearProp = new Hashtable();
            clearProp["CharacterIndex"] = -1;
            if (PhotonNetwork.LocalPlayer.IsMasterClient)
                startButton.SetActive(false);
            PhotonNetwork.LocalPlayer.SetCustomProperties(clearProp);
            return;
        }
        if (!characterButtons[characterIndex].interactable)
        {
            Debug.LogWarning("Character already taken!");
            return;
        }

        if (PhotonNetwork.LocalPlayer.IsMasterClient)
            startButton.SetActive(true);
        Hashtable props = new Hashtable
        {
            { "CharacterIndex", characterIndex }
        };

        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
    }

    private void RefreshCharacterButtons()
    {
        // Reset all buttons
        for (int i = 0; i < characterButtons.Length; i++)
        {
            characterButtons[i].interactable = true;
            characterButtons[i].GetComponent<Image>().color = Color.white;
            characterPlayers[i].text = "";
        }

        // Disable taken characters
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.CustomProperties.ContainsKey("CharacterIndex"))
            {
                Debug.Log("character selected by" + player.NickName + " is " + player.CustomProperties["CharacterIndex"]);
                int index = (int)player.CustomProperties["CharacterIndex"];

                if (index >= 0 && index < characterButtons.Length)
                {
                    characterButtons[index].interactable = false;
                    characterPlayers[index].text = player.NickName;
                }
            }
        }
    }
}