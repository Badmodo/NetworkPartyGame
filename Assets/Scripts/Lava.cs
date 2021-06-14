using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviourPunCallbacks
{
    public float speed;

    PlayerManager playerManager;
    public GameObject youDied;

    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerManager.Die();
        }
    }
}
