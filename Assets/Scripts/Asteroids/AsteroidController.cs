using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]

public class AsteroidController : MonoBehaviour
{
    private int maxHealth;
    private int currentHealth;
    private string healthRecquired;
    private int actualLife = 0;
    private int lifeSpan;
    private int damage;

    private HealthManager hManager;
    private HealthBar healthBar;
    private HitAdmin hAdmin;
    private GameObject pSystem;

    public bool isAvailable;
    private int asteroId;
    private bool canMove;
    private int speedMovement = 6;
    Vector3 target;
    Vector3 direction;
    Rigidbody rBody;

    private MeteorAdmin mAdmin;

    private enum EnemyType
    {
        small, medium, big
    }

    // Start is called before the first frame update
    void Start()
    {
        CheckComponentsAndVariables();
        healthBar = GetComponent<HealthBar>();
        mAdmin = FindObjectOfType<MeteorAdmin>();
    }

    private void Update()
    {
        AddTimetoLifeSpan();
        CheckLifeSpan();
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            direction = target - transform.position;
            rBody.AddForce(direction.normalized * speedMovement, ForceMode.Force);
        }
    }

    private void AddTimetoLifeSpan()
    {
        actualLife += 1;
    }
    private void CheckLifeSpan()
    {
        if (actualLife >= lifeSpan)
        {
            actualLife = 0;
            canMove = false;
            ToggleParticle(false);
            mAdmin.TurnAsteroidAvailableForNewLaunch(asteroId);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TrackPartsIdHolder>())
        {
            TrackPartsIdHolder pieceId = other.GetComponent<TrackPartsIdHolder>();
            if (pieceId.GetId() == mAdmin.GetActiveTrackSection())
            {
                CheckIfDamageIsDone();
                canMove = false;
                ToggleParticle(false);
                mAdmin.TurnAsteroidAvailableForNewLaunch(asteroId);
            }

        }
    }

    private void CheckComponentsAndVariables()
    {
        healthRecquired = gameObject.tag;
        hManager = FindObjectOfType<HealthManager>();
        maxHealth = hManager.AssignStartHealth(healthRecquired);
        rBody = GetComponent<Rigidbody>();
        hAdmin = FindObjectOfType<HitAdmin>();
    }

    public void ReceiveHit(int damage)  //Pensar dónde agregar la física del impacto
    {
        Debug.Log("Incoming damage is: " + damage);
        healthBar.TakeHit(damage);
    }

    public void StartLaunch(Vector3 _target)
    {
        rBody.velocity = new Vector3(0, 0, 0);
        target = _target;
        canMove = true;
        isAvailable = false;
        ToggleParticle(true);
    }

    public void SetParticleInPosition(Vector3 pTarget)
    {
        pSystem.transform.position = pTarget;
    }

    private void ToggleParticle(bool isActive)
    {
        pSystem.SetActive(isActive);
    }

    public void SetID(int id)
    {
        asteroId = id;
    }

    public int GetId()
    {
        return asteroId;
    }

    public void SetAvailableAgain()
    {
        isAvailable = true;
    }

    public void SetLifeSpan(int _lifeSpan)
    {
        lifeSpan = _lifeSpan;
    }

    public void AssignNewTarget(Vector3 newTarget)
    {
        target = newTarget;
        SetParticleToNewTarget(target);
    }

    private void SetParticleToNewTarget(Vector3 nTarget)
    {
        pSystem.transform.position = nTarget;
    }

    public void SetDamage(int _damage)
    {
        damage = _damage;
    }

    public void SetParticleSystemObject(GameObject _pSystem)
    {
        pSystem = _pSystem;
    }

    private void CheckIfDamageIsDone()
    {
        CarController cController = FindObjectOfType<CarController>();
        Transform pTransform = cController.GetComponent<Transform>();
        if (Vector3.Distance(pTransform.position, transform.position) < mAdmin.GetRatio())
        {
            hAdmin.EvaluateHit(cController, damage);
        }
    }

}
