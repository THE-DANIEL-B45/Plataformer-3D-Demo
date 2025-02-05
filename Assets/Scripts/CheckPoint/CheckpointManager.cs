using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public int lastCheckpoint = 0;

    public List<CheckpointBase> checkpoints;

    public GameObject UICheckpoint;

    public bool HasCheckpoint()
    {
        return lastCheckpoint > 0;
    }

    public void SaveCheckPoint(int i)
    {
        if (i > lastCheckpoint)
        {
            lastCheckpoint = i;

            StartCoroutine(ActivateCheckpointUI());
        }
    }

    public IEnumerator ActivateCheckpointUI()
    {
        UICheckpoint.SetActive(true);
        yield return new WaitForSeconds(2f);
        UICheckpoint.SetActive(false);
    }

    public Vector3 GetPositionFromLastCheckpoint()
    {
        var checkpoint = checkpoints.Find(i => i.key == lastCheckpoint);
        return checkpoint.transform.position;
    }
}
