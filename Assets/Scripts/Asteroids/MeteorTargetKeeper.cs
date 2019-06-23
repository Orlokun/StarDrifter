using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorTargetKeeper : MonoBehaviour
{
    public Transform[] transforms;
    public float id;
    private int lastRandomIndex;

    void Start()
    {
        GetTransforms();
    }

    void GetTransforms()
    {
        transforms = new Transform[transform.childCount];
        for (int i = 0; i < transforms.Length; i++)
        {
            transforms[i] = transform.GetChild(i);
        }
    }

    public Vector3 GiveMeATarget()
    {
        int randomNumber = Random.Range(0, transforms.Length);

        while (randomNumber == lastRandomIndex)
        {
            randomNumber = Random.Range(0, transforms.Length);
        }

        lastRandomIndex = randomNumber;
        return transforms[randomNumber].position;
    }
}
