using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public int key = 01;

    private bool checkpointActivated = false;
    //private string checkpointKey = "CheckpointKey";

    private void OnTriggerEnter(Collider other)
    {
        if(!checkpointActivated && other.transform.tag == "Player")
        {
            CheckCheckpoint();
        }
    }

    private void CheckCheckpoint()
    {
        SaveCheckPoint();
        TurnItOn();
    }

    [Button]
    private void TurnItOn()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.white);
    }

    private void TurnItOff()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.grey);
    }

    private void SaveCheckPoint()
    {
        /*if (PlayerPrefs.GetInt(checkpointKey, 0) > key) return;
        PlayerPrefs.SetInt(checkpointKey, key);*/

        CheckpointManager.Instance.SaveCheckPoint(key);

        SaveManager.Instance.SaveItems();

        checkpointActivated = true;
    }
}
