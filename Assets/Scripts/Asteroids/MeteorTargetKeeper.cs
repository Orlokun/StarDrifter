using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorTargetKeeper : MonoBehaviour
{
    public List<Transform> transforms = new List<Transform>();   
    public float id;

    void Start()
    {
        foreach (Transform _transform in transform)
        {
            transforms.Add(_transform);
        }
    }
}
