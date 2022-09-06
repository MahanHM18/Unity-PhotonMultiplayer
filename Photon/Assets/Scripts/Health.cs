using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class Health : MonoBehaviourPunCallbacks, IPunObservable
{
    public float health = 100;

    int team;

    private void Start()
    {

        team = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];

    }
    private void Update()
    {
        if (health <= 0)
        {
            if (photonView.IsMine)
            {
                StartCoroutine(Respawn());
                photonView.RPC(nameof(RPC_AddScorce), RpcTarget.All,team);
            }

        }
    }

    // Update is called once per frame
    public void TakeDamage(float value)
    {
        health -= value;

        if (photonView.IsMine)
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

        yield return new WaitForSeconds(1);

        GetComponent<CharacterController>().enabled = true;



    }

    [PunRPC]
    private void RPC_AddScorce(int t)
    {

        if (t == 0)
        {
            GameManager.RedTeamScore++;
            UIManager.Instance.SetRedScoreText(GameManager.RedTeamScore);

        }
        else if (t == 1)
        {
            GameManager.BlueTeamScore++;
            UIManager.Instance.SetBlueScoreText(GameManager.BlueTeamScore);
        }
    }
}