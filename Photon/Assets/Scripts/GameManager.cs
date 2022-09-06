using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject RedPlayerPrefab;
    public GameObject BluePlayerPrefab;


    public static int BlueTeamScore = 0;
    public static int RedTeamScore = 0;

    private void Awake()
    {

    }

    void Start()
    {

        int team = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];

        if (team == 0)
        {
            PhotonNetwork.Instantiate(RedPlayerPrefab.name, new Vector3(0, 5, 0), Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate(BluePlayerPrefab.name, new Vector3(0, 5, 0), Quaternion.identity);
        }


    }

    // Update is called once per frame
    public void Quit()
    {
        PhotonNetwork.LeaveRoom();
    }


}
