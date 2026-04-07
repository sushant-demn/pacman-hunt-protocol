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


        int spawnIndex = characterToSpawn;
        Debug.Log("spawning at " + spawnIndex);
        Transform mySpawnPoint = spawnPoints[spawnIndex];

        string prefabName = playerPrefabs[characterToSpawn].name;
        Debug.Log($"Spawning as {prefabName} at {mySpawnPoint.name}");
        PhotonNetwork.Instantiate(prefabName, mySpawnPoint.position, mySpawnPoint.rotation);
    }
}
