using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ControllType {Keys, WASD}

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public ControllType controllType;

    private Rigidbody rb;
    private InputAction hor;
    private InputAction ver;

    private float horMove;
    private float verMove;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        string horInputPos = "<Keyboard>/d";
        string horInputNeg = "<Keyboard>/a";
        string verInputPos = "<Keyboard>/w";
        string verInputNeg = "<Keyboard>/s";
        if(controllType == ControllType.Keys)
        {
            horInputPos = "<Keyboard>/rightArrow";
            horInputNeg = "<Keyboard>/leftArrow";
            verInputPos = "<Keyboard>/upArrow";
            verInputNeg = "<Keyboard>/downArrow";
        }
        hor = new InputAction("Hor");
        hor.AddCompositeBinding("Axis")
            .With("Positive", horInputPos)
            .With("Negative", horInputNeg);
        hor.started += ctx => horMove = ctx.action.ReadValue<float>();
        hor.canceled += ctx => horMove = 0;
        hor.Enable();
        ver = new InputAction("Ver");
        ver.AddCompositeBinding("Axis")
            .With("Positive", verInputPos)
            .With("Negative", verInputNeg);
        ver.started += ctx => verMove = ctx.action.ReadValue<float>();
        ver.canceled += ctx => verMove = 0;
        ver.Enable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector3(horMove, 0, verMove) * speed;
    }
}
