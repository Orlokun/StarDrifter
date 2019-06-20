using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarParticles : MonoBehaviour
{
    Transform myTransform;
    ParticleSystem.Particle[] starDust; 

    private void Start()
    {
        myTransform = transform;
    }

    private void Update()
    {
        
    }
    void CreateStars()
    {
        if (starDust == null)
        {
            CreateStars();
        }
    }

}
