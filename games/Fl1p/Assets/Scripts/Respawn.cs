using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Respawn : MonoBehaviour
{
    public GameObject Spawn;
    public GameObject Player;
    private void OnTriggerEnter(Collider other) {
        other.transform.position = Spawn.transform.position;
    }
}