using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPartsIdHolder : MonoBehaviour
{
    private int partId;

    public void SetId(int id)
    {
        partId = id;
    }

    public int GetId()
    {
        return partId;
    }
}
