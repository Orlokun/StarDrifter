using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrWheel : MonoBehaviour
{
    private Rigidbody rBody;

    [Header("Suspension")]
    public float restLength;
    public float springTravel;
    public float springStiffnes;
    public float damperStiffnes;

    private float minLength;
    private float maxLength;
    private float lastLength;
    private float springLength;
    private float springForce;
    private float springVelocity;
    private float damperForce;

    private Vector3 suspensionForce;


    [Header("Wweel")]
    public float wheelRadius;

    // Start is called before the first frame update
    void Start()
    {
        rBody = transform.root.GetComponent<Rigidbody>();

        minLength = springLength - springTravel;
        maxLength = springTravel + springTravel;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, -transform.up, out hit, maxLength + wheelRadius))
        {
            lastLength = springLength; 
            springLength = hit.distance - wheelRadius;
            springLength = Mathf.Clamp(springLength, minLength, maxLength);
            springVelocity = (lastLength - springLength) / Time.fixedDeltaTime;

            springForce = springStiffnes * (restLength - springLength);
            suspensionForce = (springForce + damperForce) * transform.up;
            damperForce = damperStiffnes * springVelocity;

            rBody.AddForceAtPosition(suspensionForce, hit.point);
        }
    }
}
