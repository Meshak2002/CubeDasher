using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class photonConnect : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        thememanager.instance.GetComponent<AudioSource>().enabled = false;
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("Connected to master");
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Connected");
        this.GetComponent<joinLobby>().enabled = true;
    }
}
