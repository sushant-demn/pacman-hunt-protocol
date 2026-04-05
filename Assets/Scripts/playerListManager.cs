using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro; // Assuming you are using TextMeshPro for UI

public class PlayerListManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI playerListText; // Assign a UI Text element in Inspector

    // Call this whenever you need to refresh the UI
    public void UpdatePlayerListUI()
    {
        // 1. Clear the current text
        playerListText.text = "Players in Room:\n\n\n";

        // 2. Loop through every player in the room
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.IsMasterClient)
            {
                playerListText.text += player.NickName + " (Host)\n";
                continue;
            }
            // 3. Add their name to the text string
            playerListText.text += player.NickName + "\n";

            // Optional: You can also check if they are the host
        }
    }

    // It's a good idea to run this right when you successfully join the room
    public override void OnJoinedRoom()
    {
        UpdatePlayerListUI();
    }

    // Triggered automatically whenever a NEW player joins your room
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined the room!");

        // Refresh the UI to show the new person
        UpdatePlayerListUI();
    }

    // Triggered automatically whenever a player leaves or disconnects
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log(otherPlayer.NickName + " left the room.");

        // Refresh the UI to remove the person who left
        UpdatePlayerListUI();
    }
}