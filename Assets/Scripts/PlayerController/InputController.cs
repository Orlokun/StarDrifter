using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;
    CarController cController;

    private void Awake()
    {
        cController = FindObjectOfType<CarController>();
    }

    public Vector3 GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        return new Vector3(horizontalInput, verticalInput, 0);
    }


}
