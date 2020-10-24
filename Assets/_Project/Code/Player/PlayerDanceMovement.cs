using GameJam.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameJam.Player
{
    public class PlayerDanceMovement : MonoBehaviour
    {
        private InputController _inputController;

        public float moveSpeed = 20;
        public Transform movePoint;
        public Transform playerPoint;

        private Vector2 moveAxis;

        private void Awake()
        {
            _inputController = InputController.Instance;
        }

        private void OnEnable()
        {
            //Register for Events
            _inputController.OnHandleMove += HandleMove;
            _inputController.OnHandleMoveEnd += HandleMoveEnd;
        }

        

        // Start is called before the first frame update
        void Start()
        {
            movePoint.parent = null;
        }

        // Update is called once per frame
        void Update()
        {
            
        }


        public void HandleMove(InputAction.CallbackContext context)
        {
            //Debug.Log("Handling a Movement! " + context.ToString());
            moveAxis = context.ReadValue<Vector2>();
            //Debug.Log("X=" + moveAxis.x + "  Y=" + moveAxis.y);
            //Debug.Log("Magnitude=" + moveAxis.magnitude + "  Normalized=" + moveAxis.normalized);

            movePoint.position += (Vector3)moveAxis.normalized;
            playerPoint.position += (Vector3)moveAxis.normalized;

        }

        private void HandleMoveEnd(InputAction.CallbackContext context)
        {
            //Debug.Log("Stopping Moving away from the Invaders! " + context.ToString());
            moveAxis = new Vector2(0f, 0f);
            movePoint.position = new Vector3(0f, 0f);
        }


        private void OnDisable()
        {
            //DeRegister Events
            _inputController.OnHandleMove -= HandleMove;
            _inputController.OnHandleMoveEnd -= HandleMoveEnd;
        }
    }
}