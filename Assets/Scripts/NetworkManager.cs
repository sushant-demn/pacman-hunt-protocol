using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        Debug.Log("Trying to Connect Master Server");

        if (PhotonNetwork.ConnectUsingSettings())
            Debug.Log("Connected to the master server");
        else
            Debug.Log("Failed to connect to the master server");

    }

}