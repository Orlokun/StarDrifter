using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    private Rigidbody targetRBody;
    public float backFollow;
    public float height;
    public float horizontalFollow;
    private float rotationDamping = 3f;
    private float heightDamping = 2f;
    public float zoomRatio;
    public float defaultFOW = 60;

    private Vector3 myRotation;
    private Vector3 actualPointInSpace;

    // Start is called before the first frame update
    void Start()
    {
        CheckEditorValues();
        SetInitialValues();
        myRotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        SetCameraPositionAndRotation();
    }

    private void SetCameraPositionAndRotation()
    {
        float wantedAngle = myRotation.y;           // Datos para mover camara con Lerp
        float wantedHeight = target.position.y + height;
        float myAngle = transform.eulerAngles.y;
        float myHeight = transform.position.y;

        myAngle = Mathf.LerpAngle(myAngle, wantedAngle, rotationDamping * Time.deltaTime);      //Movimiento suave de la cámara
        myHeight = Mathf.Lerp(myHeight, wantedHeight, heightDamping * Time.deltaTime);

        Quaternion currentRotation = Quaternion.Euler(0f, myAngle, 0);                          //Seteo Ángulo y posición para seguir desde atrás
        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * backFollow;
        transform.position = new Vector3(transform.position.x, myHeight, transform.position.z);
        transform.LookAt(target);
    }

    private void FixedUpdate()
    {
        Vector3 localVelocity = target.parent.InverseTransformDirection(targetRBody.velocity.normalized);
        if (localVelocity.z < -0.5f)
        {
            myRotation.y = target.eulerAngles.y + 180;
        }
        else
        {
            myRotation.y = target.eulerAngles.y;
        }
        float acceleration = targetRBody.velocity.magnitude;
        Camera cam = Camera.main;
        cam.fieldOfView = defaultFOW + acceleration * zoomRatio * Time.deltaTime;
    }

    void CheckEditorValues()
    {
        if(!target)
        {
            Debug.LogError("La cámara necesita un Target para seguir desde el editor");
        }
    }

    void SetInitialValues()
    {
        targetRBody = target.parent.gameObject.GetComponent<Rigidbody>();
    }
}
