using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorAdmin : MonoBehaviour
{
    RaceTrackHandler rHandler;
    AsteroidController[] asteroids;
    Vector3[] initialAsteroidPositions;
    AsteroidPromptParticle[] alertParticles;

    int difficulty;
    [SerializeField] int asteroidsFreq;
    int delayTime;
    int asteroidIndex;
    public int asteroidLifeSpan;
    public int damage = 15;
    public float travelTime;

    private Vector3 actualTarget;
    int activeSection;
    bool isActive = false;
    private float damageRatio;

    private void Start()
    {
        rHandler = FindObjectOfType<RaceTrackHandler>();
        asteroids = FindObjectsOfType<AsteroidController>();
        alertParticles = FindObjectsOfType<AsteroidPromptParticle>();
        SetAsteroidParameters();
        GetAsteroidsPlaceHoldersPosition();
        SetDamageRatio();
        ToggleAllAsteroids(false);
        StartCoroutine(TurnAsteroidThrowerOn());
    }

    private void Update()
    {
        if (isActive)
        {
            SecureFrequency();
            delayTime++;
            if (delayTime >= asteroidsFreq)
            {
                SendNewAsteroid();
                delayTime = 0;
            }
        }
    }

    private void SecureFrequency()
    {
        if (asteroidsFreq < 30)
        {
            asteroidsFreq = 30;
        }
    }

    private void GetAsteroidsPlaceHoldersPosition()
    {
        AsteroidsPlaceHolder[] placeHolders = FindObjectsOfType<AsteroidsPlaceHolder>();
        initialAsteroidPositions = new Vector3[placeHolders.Length];
        foreach (AsteroidsPlaceHolder aHolder in placeHolders)
        {
            int indexPosition = aHolder.id;
            initialAsteroidPositions[indexPosition] = aHolder.transform.position;
        }
    }

    public int GetActiveTrackSection()
    {
        return rHandler.GetActiveSection();
    }

    private IEnumerator TurnAsteroidThrowerOn()
    {
        yield return new WaitForSeconds(5);
        isActive = true;
    }

    private void SendNewAsteroid()
    {
        AsteroidController aController = asteroids[asteroidIndex];
        if (!aController.gameObject.activeInHierarchy && aController.isAvailable)
        {
            SetLaunchValues();
            LaunchAsteroid(actualTarget, asteroids[asteroidIndex]);
            asteroidIndex++;
            if (asteroidIndex >= asteroids.Length)
            {
                asteroidIndex = 0;
            }
        }
    }


    void SetLaunchValues()
    {
        activeSection = GetActiveTrackSection();
        actualTarget = GetTargetFromSection(activeSection);
    }

    public Vector3 GetTargetFromSection(int sectionId)
    {
        MeteorTargetKeeper targetKeeper = GetMyTargetKeeper(sectionId);
        Vector3 myTarget = targetKeeper.GiveMeATarget();
        Debug.Log("Asteroids target is: " + myTarget);
        return myTarget;
    }

    MeteorTargetKeeper GetMyTargetKeeper(int sectionId)
    {
        TrackSection[] tracks = FindObjectsOfType<TrackSection>();
        for (int i = 0; i < tracks.Length; i++)
        {
            if (tracks[i].GetSectionId() == activeSection)
            {
                if (tracks[i].targetKeeper != null)
                {
                    return tracks[i].targetKeeper;
                }
            }
        }
        return null;
    }

    private void LaunchAsteroid(Vector3 targetPosition, AsteroidController aController)
    {
        aController.gameObject.SetActive(true);
        aController.SetTargetGroupId(activeSection);
        aController.SetParticleInPosition(targetPosition);
        aController.StartLaunch(targetPosition);
    }


    void ToggleAllAsteroids(bool isActive)
    {
        for (int i = 0; i < asteroids.Length; i++)
        {
            GameObject mAsteroid = asteroids[i].gameObject;
            if (mAsteroid)
            {
                mAsteroid.SetActive(isActive);
            }
        }
    }

    void SetAsteroidParameters()
    {
        for (int i = 0; i < asteroids.Length; i++)
        {
            AsteroidController aController = asteroids[i];
            if (aController)
            {
                SetParticleIdAndAsteroid(aController, i);
                aController.SetID(i);
                aController.SetLifeSpanAndTravelTime(asteroidLifeSpan, travelTime);
                aController.SetDamage(damage);
            }
        }
    }

    void SetParticleIdAndAsteroid(AsteroidController aController, int id)
    {
        AsteroidPromptParticle particle = GetParticleController(id);
        if (particle)
        {
            particle.SetId(id);
            aController.SetParticleSystemObject(particle.gameObject);
            particle.gameObject.SetActive(false);
        }
    }

    AsteroidPromptParticle GetParticleController(int id)
    {
        if (alertParticles[id] != null)
        {
            return alertParticles[id];
        }
        return null;
    }

    void ToggleAsteroid(int id, bool _active)
    {
        for (int i = 0; i < asteroids.Length; i++)
        {
            AsteroidController asteroid = asteroids[i];
            if (asteroid)
            {
                if (asteroid.GetId() == id)
                {
                    asteroid.gameObject.SetActive(_active);
                }
            }

        }
    }

    public AsteroidController GetAsteroidById(int id)
    {
        for (int i = 0; i < asteroids.Length; i++)
        {
            AsteroidController aController = asteroids[i];
            if (aController.GetId() == id)
            {
                return aController;
            }
        }
        return null;
    }

    public void TurnAsteroidAvailableForNewLaunch(int id)
    {
        ToggleAsteroid(id, false);
        //ToggleParticle(id, false);
        ReturnAsteroidToSectionPosition(id);
        StartCoroutine(MakeAsteroidAvailableAgain(id));
    }

    public void EraseTargetFromList(Vector3 target, int sectionId)
    {
        MeteorTargetKeeper tKeeper = GetMyTargetKeeper(sectionId);
        tKeeper.EraseTargetFromList(target);
    }

    /*private void ToggleParticle(int id, bool isActive)
    {
        for (int i = 0; i < alertParticles.Length; i++)
        {
            AsteroidPromptParticle aParticle = alertParticles[i];
            if (aParticle)
            {
                if (aParticle.GetId() == id)
                {
                    aParticle.gameObject.SetActive(isActive);
                }
            }

        }
    }*/

    private void SetDamageRatio()
    {
        switch (LevelManager.GetDifficulty())
        {
            case 0:
                damageRatio = .3f;
                break;
            case 1:
                damageRatio = 1f;
                break;
            case 2:
                damageRatio = 5f;
                break;
            default:
                break;
        }
    }

    private void ReturnAsteroidToSectionPosition(int astroId)
    {
        Vector3 initialPosition = initialAsteroidPositions[rHandler.GetActiveSection()];
        Debug.Log("Returning Asteroid " + astroId + " to position: " + initialPosition);
        for (int i = 0; i < asteroids.Length; i++)
        {
            AsteroidController aController = asteroids[i];
            if (aController)
            {
                if (aController.GetId() == astroId)
                {
                    aController.transform.position = initialPosition;
                }
            }
        }
    }

    void GiveAsteroidNewTarget(int id)
    {
        for (int i = 0; i < asteroids.Length; i++)
        {
            AsteroidController aController = asteroids[i];
            if (aController)
            {
                if (aController.GetId() == id)
                {
                    SetLaunchValues();
                    aController.AssignNewTarget(actualTarget);
                }
            }
        }
    }

    private IEnumerator MakeAsteroidAvailableAgain(int id)
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < asteroids.Length; i++)
        {
            AsteroidController aController = asteroids[i];
            if (aController)
            {
                if (aController.GetId() == id)
                {
                    aController.SetAvailableAgain();
                }
            }
        }
    }

    public void SetNewFrequency(int numberOfLaps)
    {
        if(numberOfLaps <= 3)
        {
            asteroidsFreq -= 25;
        }
        else if (numberOfLaps >= 4 && numberOfLaps<=10)
        {
            asteroidsFreq -= 35;
        }
    }

    public float GetRatio()
    {
        return damageRatio;
    }

}
