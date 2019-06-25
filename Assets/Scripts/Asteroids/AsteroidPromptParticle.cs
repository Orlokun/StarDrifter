using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidPromptParticle : MonoBehaviour
{
    private int particleId;

    public void SetId(int id)
    {
        particleId = id;
    }

    public int GetId()
    {
        return particleId;
    }
}
