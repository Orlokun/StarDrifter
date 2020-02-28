using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCRotateM : MonoBehaviour
{
    public float Speed;
    public bool IsInverse;
    public bool roll;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!roll)
        {
            if (IsInverse)
            {
                transform.Rotate(0, Speed * Time.deltaTime, 0);
            }
            else
            {
                transform.Rotate(0, -Speed * Time.deltaTime, 0);
            }
        }
        else
        {
            transform.Rotate(0, 0, Speed * Time.deltaTime);
        }
    }
}
