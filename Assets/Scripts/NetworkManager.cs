using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public String roomName = "room1";
    private void Start()
    {
        Debug.Log("Trying to Connect Master Server");

        if (PhotonNetwork.ConnectUsingSettings())
            Debug.Log("Connected to the master server");
        else
            Debug.Log("Failed to connect to the master server");

    }

    public void HostServer()
    {
        RoomOptions options = new RoomOptions { MaxPlayers = 5 };
        PhotonNetwork.CreateRoom(roomName, options);
    }
    public void joinServer()
    {
        PhotonNetwork.JoinRoom(roomName);
    }

}