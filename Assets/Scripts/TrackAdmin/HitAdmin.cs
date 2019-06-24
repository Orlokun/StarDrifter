using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAdmin : MonoBehaviour
{
    CarController player;
    HealthManager hManager;

    private void Start()
    {
        player = FindObjectOfType<CarController>();
        hManager = FindObjectOfType<HealthManager>();
    }

    public void EvaluateHit(CarController player, int damage)
    {
        if (player.GetHitState() == true)
        {
            return;
        }
        else
        {
            TakeHit(player.gameObject, damage);
            StartCoroutine(TurnPlayeravailable(player));
        }
    }

    private IEnumerator TurnPlayeravailable(CarController player)
    {
        player.SetHit(true);
        yield return new WaitForSeconds(3);

        player.SetHit(false);
    }

    private void TakeHit(GameObject player, int damage)
    {
        hManager.ChangeHP(player, damage);
    }
}
