using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Gun : MonoBehaviourPunCallbacks
{
    public Transform GunTrasform;
    public ParticleSystem ps;

    public LayerMask EnemyLayer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetMouseButtonDown(0))
            {
                photonView.RPC("RPC_Shoot",RpcTarget.All);
            }
        }
    }

    [PunRPC]
    private void RPC_Shoot()
    {
        ps.Play();

        Ray gunRay = new Ray(GunTrasform.position, GunTrasform.forward);

        if (Physics.Raycast(gunRay, out RaycastHit hit, 100, EnemyLayer))
        {
            var enemyHealth = hit.transform.gameObject.GetComponent<Health>();
            if (enemyHealth)
            {
                enemyHealth.TakeDamage(20);
            }
        }
    }
}
