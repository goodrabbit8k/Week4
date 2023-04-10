using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    PlayerManager player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && player.punching)
        {
            Destroy(gameObject);
        }
    }
}
