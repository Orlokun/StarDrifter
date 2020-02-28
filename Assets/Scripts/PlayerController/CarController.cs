using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public ScrWheel[] wheel;

    [Header ("CarSpecs")]
    public float wheelBase;    //in mts
    public float rearTrack;    //in mts
    public float turnRadius;   //in mts

    [Header("Inputs")]
    public float steerInput;
    private float ackermannAngleLeft;
    private float ackermannAngleRight;




    private InputController iController;
    private Vector3 wasdController;
    private Rigidbody rBody;
    public float centerOfMassY;
    private bool hitState;

    public WheelCollider[] frontWheels = new WheelCollider[2];
    public WheelCollider[] rearWheels = new WheelCollider[2];

    public Transform[] frontWheelsT = new Transform[2];
    public Transform[] rearWheelsT = new Transform[2];

    private float turningAngle;
    private float maxTurnAngle = 30f;
    private float lowSpeedSteerAngle = 15;
    private float highSpeedSteerAngle = 5;
    private float currentSteerAngle;
    private float speedFactor;

    [SerializeField]
    private float motorForce;
    public float topSpeed;

    AudioSource aSource;

    private void Awake()
    {

    }

    private void FixedUpdate()
    {
        steerInput = Input.GetAxis("Horizontal");
        if (steerInput > 0) //Turning Right
        {
            ackermannAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / turnRadius + (rearTrack / 2));
            ackermannAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / turnRadius - (rearTrack / 2));
        }
        else if(steerInput < 0) //Turning Rigth
        {
            ackermannAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / turnRadius - (rearTrack / 2));
            ackermannAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / turnRadius + (rearTrack / 2));
        }
        else
        {
            ackermannAngleRight = 0;
            ackermannAngleLeft = 0;
        }
    }

    private void SetSteerAngleFromSpeedAndInput()
    {
        speedFactor = rBody.velocity.magnitude / topSpeed;
        currentSteerAngle = Mathf.Lerp(lowSpeedSteerAngle, highSpeedSteerAngle, speedFactor);
        currentSteerAngle *= Input.GetAxis("Horizontal");
    }
    void SetWheelSteerAngle(float _steerAngle)
    {
        foreach (WheelCollider wCollider in frontWheels)
        {
            wCollider.steerAngle = _steerAngle;
        }
    }

    public void Accelerate()
    {
        if (wasdController.y != 0f)
        {
            foreach (WheelCollider wCollider in frontWheels)
            {
                wCollider.motorTorque = wasdController.y * motorForce;
            }
        }
    }

    void UpdateWheelStates()
    {
        UpdatePairOfWheels(frontWheels, frontWheelsT);
        UpdatePairOfWheels(rearWheels, rearWheelsT);
    }

    private void UpdatePairOfWheels(WheelCollider[] wColliders, Transform[] wTransforms)
    {
        for (int i = 0; i < wColliders.Length; i++)
        {
            UpdateWheelPose(wColliders[i], wTransforms[i]);
        }
    }

    void UpdateWheelPose(WheelCollider wCollider, Transform _transform)
    {
        Vector3 position = _transform.position;
        Quaternion myQuaternion = _transform.rotation;

        wCollider.GetWorldPose(out position, out myQuaternion);
        _transform.position = position;
        _transform.rotation = myQuaternion;
    }

    private void TurnAudioOn()
    {
        aSource = GetComponent<AudioSource>();
        if (aSource)
        {
            aSource.volume = 1;
        }
    }

    public void SetHit(bool _setHit)
    {
        hitState = _setHit;
    }

    public bool GetHitState()
    {
        return hitState;
    }

}
