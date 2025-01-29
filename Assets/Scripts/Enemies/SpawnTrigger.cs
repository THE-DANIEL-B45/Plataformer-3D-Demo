using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public GameObject[] enemies;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            foreach(GameObject enemy in enemies)
            {
                enemy.SetActive(true);
            }
        }
    }
}
