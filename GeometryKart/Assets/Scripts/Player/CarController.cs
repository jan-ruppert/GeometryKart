using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : NetworkBehaviour
{
    
    //input
    [SerializeField] private InputActionAsset inputActionAsset;

    private InputActionMap gameplayActionMap;

    private InputAction accelerateAction;

    private InputAction steerAction;

    //param

    public GameObject sphere;

    public float acceleration;

    public float steeringSensitivity;

    private float currentAcceleration;

    private float currentSpeed, currentRotate;


    private void Awake()
    {
        gameplayActionMap = inputActionAsset.FindActionMap("Gameplay");
        
        accelerateAction = gameplayActionMap.FindAction("accelerate");

        steerAction = gameplayActionMap.FindAction("steer");

        inputActionAsset.Enable();
        
       
    }

    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        HandleMovementServerAuth();
        //HandleMovement();
    }

    private void FixedUpdate()
    {
        if (!IsOwner)
        {
            return;
        }
        FixedHandleMovementServerRpc();
    }

    private void HandleMovementServerAuth()
    {
        HandleMovementServerRpc(accelerateAction.ReadValue<float>(), steerAction.ReadValue<float>());
    }

    [ServerRpc(RequireOwnership = false)]
    private void HandleMovementServerRpc(float accelerateValue, float steerValue)
    {
        transform.position = sphere.transform.position - new Vector3(0, sphere.GetComponent<SphereCollider>().radius, 0);
        
        currentSpeed = Mathf.SmoothStep(currentSpeed, acceleration * accelerateValue, Time.deltaTime * 12f);
        
        currentRotate = Mathf.Lerp(currentRotate, steeringSensitivity *  steerValue, Time.deltaTime * 4f);
    }

    [ServerRpc(RequireOwnership = false)]
    private void FixedHandleMovementServerRpc()
    {
        //acceleration
        sphere.GetComponent<Rigidbody>().AddForce(-transform.forward * currentSpeed, ForceMode.Acceleration);
        
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, transform.eulerAngles.y + currentRotate, 0), Time.fixedDeltaTime * 5f);
    }


    private void HandleMovement()
    {
        transform.position = sphere.transform.position - new Vector3(0, sphere.GetComponent<SphereCollider>().radius, 0);
        
        currentSpeed = Mathf.SmoothStep(currentSpeed, acceleration * accelerateAction.ReadValue<float>(), Time.deltaTime * 12f);
        
        currentRotate = Mathf.Lerp(currentRotate, steeringSensitivity *  steerAction.ReadValue<float>(), Time.deltaTime * 4f);
    }

    private void FixedHandleMovement()
    {
        //acceleration
        sphere.GetComponent<Rigidbody>().AddForce(-transform.forward * currentSpeed, ForceMode.Acceleration);
        
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, transform.eulerAngles.y + currentRotate, 0), Time.fixedDeltaTime * 5f);
    }
    
}
