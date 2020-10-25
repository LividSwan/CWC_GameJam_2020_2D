using GameJam.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerLevelMovement : MonoBehaviour
{
    private InputController _inputController;
    public Vector2 moveAxis;
    public GameObject player;
    public float playerWalkSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        //Register for Events
        _inputController.OnHandleMove += HandleMove;
        _inputController.OnHandleMoveEnd += HandleMoveEnd;
        //player = GameObject.Find(playerObjectName);
    }

    private void Awake()
    {
        _inputController = InputController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        player.transform.Translate(moveAxis * Time.deltaTime * playerWalkSpeed);


    }

    private void OnEnable()
    {


    }

    public void HandleMove(InputAction.CallbackContext context)
    {
        moveAxis = context.ReadValue<Vector2>();
    }

    private void HandleMoveEnd(InputAction.CallbackContext context)
        {

                string keyPressed = context.control.name;
                switch (context.control.name)
                {
                    case "w":
                    case "upArrow":
                    case "s":
                    case "downArrow":
                    case "a":
                    case "leftArrow":
                    case "d":
                    case "rightArrow":
                        moveAxis = new Vector2(0,0);
                        break;
                }
            
}
}


