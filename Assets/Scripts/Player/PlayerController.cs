using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputActions playerCMap;
    private Vector2 MovementInput;
    private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private Camera playerCam;
    private void Awake() 
    {
        rb = GetComponent<Rigidbody>();
        playerCMap = new InputActions();

        playerCMap.Gameplay.Movement.performed += ctx => MovementInput = ctx.ReadValue<Vector2>();
        playerCMap.Gameplay.Movement.canceled += ctx => MovementInput = Vector2.zero;
    }

    private void Update() 
    {
        Movement();
    }

    void Movement()
    {
        Vector3 CamF = playerCam.transform.forward;
        Vector3 CamR = playerCam.transform.right;

        CamF.y = 0;
        CamR.y = 0;

        CamF = CamF.normalized;
        CamR = CamR.normalized;

        Vector3 positionToMoveTo = CamF * MovementInput.y + CamR * MovementInput.x;

        Vector3 m = positionToMoveTo * speed;

        rb.velocity = m;
    }

    void OnEnable() 
    {
        playerCMap.Gameplay.Enable();
    }

    void OnDisable() 
    {
        playerCMap.Gameplay.Movement.performed -= ctx => MovementInput = ctx.ReadValue<Vector2>();
        playerCMap.Gameplay.Movement.canceled -= ctx => MovementInput = Vector2.zero;
        playerCMap.Gameplay.Disable();
    }
}
