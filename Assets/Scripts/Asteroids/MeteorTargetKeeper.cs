using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorTargetKeeper : MonoBehaviour
{
    public Transform[] transforms;
    public float id;
    private int lastRandomIndex;
    private List<int> activeTargets;

    void Start()
    {
        GetTransforms();
        activeTargets = new List<int>();
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
        int randomNumber;
        do
        {
            randomNumber = Random.Range(0, transforms.Length);
        } while (randomNumber == lastRandomIndex || activeTargets.Contains(randomNumber));

        activeTargets.Add(randomNumber);
        lastRandomIndex = randomNumber;
        return transforms[randomNumber].position;
    }

    public void EraseTargetFromList(Vector3 target)
    {
        for (int i = 0; i < transforms.Length; i++)
        {
            Transform _transform = transforms[i];
            if(_transform.position == target)
            {
                if(activeTargets.Contains(i))
                {
                    activeTargets.Remove(i);
                }
            }
        }
    }
}
