using Itens;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            ItemCollectableBase i = other.transform.GetComponent<ItemCollectableBase>();
            if(i != null)
            {
                i.gameObject.AddComponent<Magnetic>();
            }
        }
    }
}
