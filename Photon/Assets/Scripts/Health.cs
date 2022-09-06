using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class Health : MonoBehaviourPunCallbacks, IPunObservable
{
    public float health = 100;

    private void Update()
    {
        if (health <= 0)
        {
            StartCoroutine(Respawn());
        }
    }

    // Update is called once per frame
    public void TakeDamage(float value)
    {
        health -= value;
        UIManager.Instance.SetHealthBar(health);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(health);
        }
        else
        {
            health = (float)stream.ReceiveNext();
        }
    }

    IEnumerator Respawn()
    {
        health = 100;
        UIManager.Instance.SetHealthBar(health);
        transform.position = new Vector3(0, 10, 0);
        GetComponent<CharacterController>().enabled = false;

        photonView.RPC(nameof(RPC_AddScorce), RpcTarget.All);

        yield return new WaitForSeconds(1);
        
        GetComponent<CharacterController>().enabled = true;

        
        
    }

    [PunRPC]
    private void RPC_AddScorce()
    {
        int team = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];

        if (team == 1)
        {
            GameManager.BlueTeamScore++;
            UIManager.Instance.SetBlueScoreText(GameManager.BlueTeamScore);
        }
        else
        {
            GameManager.RedTeamScore++;
            UIManager.Instance.SetBlueScoreText(GameManager.RedTeamScore);
        }
    }
}