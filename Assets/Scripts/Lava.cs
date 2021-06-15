using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviourPunCallbacks
{
    public float speed;

    public GameObject youDied;

    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ThirdPersonController playerController = other.GetComponent<ThirdPersonController>();
            playerController.Die();
        }
    }
}
