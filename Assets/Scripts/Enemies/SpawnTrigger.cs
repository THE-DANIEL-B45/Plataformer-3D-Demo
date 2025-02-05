using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public GameObject[] enemies;


    public bool once = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player") && !once)
        {
            once = true;

            foreach(GameObject enemy in enemies)
            {
                enemy.SetActive(true);
            }
        }
    }
}
