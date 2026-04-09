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
        GameObject myPlayer = PhotonNetwork.Instantiate(prefabName, mySpawnPoint.position, mySpawnPoint.rotation);
        //Camera Targetting Local Player
        Camera.main.GetComponent<CameraFollow>().target = myPlayer.transform;
        GameObject minimapCam = GameObject.Find("minimapCamera");
        if (characterToSpawn == 0)
            Camera.main.GetComponent<CameraFollow>().setCameraFullSize();
        if (minimapCam != null)
            minimapCam.GetComponent<CameraFollow>().target = myPlayer.transform;
    }
}
