using Photon.Pun;
using UnityEngine;

public class levelManager : MonoBehaviourPunCallbacks
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject[] playerPrefabs;
    [SerializeField] Transform[] spawnPoints;

    void Start()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        int characterToSpawn = 0;
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("CharacterIndex"))
            characterToSpawn = (int)PhotonNetwork.LocalPlayer.CustomProperties["CharacterIndex"];
        if (characterToSpawn == -1) characterToSpawn = 1; //safety net if anyone doesn't choose anything so they will be ghost

        //figuring out which spawn point to choose ?
        // ActorNumbers start at 1, so we subtract 1 to get an array index starting at 0.
        // We use the Modulo operator (%) as a safety net so if there are 5 players and only 4 spawn points, Player 5 loops back to Spawn Point 0.
        int spawnIndex = (PhotonNetwork.LocalPlayer.ActorNumber - 1) % spawnPoints.Length;
        Transform mySpawnPoint = spawnPoints[spawnIndex];

        string prefabName = playerPrefabs[characterToSpawn].name;
        Debug.Log($"Spawning as {prefabName} at {mySpawnPoint.name}");
        PhotonNetwork.Instantiate(prefabName, mySpawnPoint.position, mySpawnPoint.rotation);
    }
}
