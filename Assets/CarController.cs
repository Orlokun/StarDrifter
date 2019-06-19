using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private InputController iController;
    private Vector3 wasdController;
    private Rigidbody rBody;
    public float centerOfMassY;

    public WheelCollider[] frontWheels = new WheelCollider[2];
    public WheelCollider[] rearWheels = new WheelCollider[2];

    public Transform[] frontWheelsT = new Transform[2];
    public Transform[] rearWheelsT= new Transform[2];

    float turningAngle;
    float maxTurnAngle = 30f;
    [SerializeField]
    float motorForce = 50f;


    public void Accelerate()
    {
        if(wasdController.y != 0f)
        {
            foreach(WheelCollider wCollider in frontWheels)
            {
                wCollider.motorTorque = wasdController.y * motorForce;
            }
        }
    }

    public void Turn()
    {
        turningAngle = maxTurnAngle * wasdController.x;
        if (turningAngle != 0)
        {
            foreach (WheelCollider wCollider in frontWheels)
            {
                wCollider.steerAngle = turningAngle;
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

    private void Awake()
    {
        iController = FindObjectOfType<InputController>();
        rBody = GetComponent<Rigidbody>();
        rBody.centerOfMass = new Vector3(rBody.centerOfMass.x, centerOfMassY, rBody.centerOfMass.z);
    }

    private void FixedUpdate()
    {
        wasdController = iController.GetInput();
        Turn();
        Accelerate();
        UpdateWheelStates();
    }
}
