using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using GameJam.Core;

namespace GameJam.Input
{
    public delegate void InputMoveHandler(InputAction.CallbackContext context);
    public delegate void InputMoveEndHandler(InputAction.CallbackContext context);

    public class InputController : CoreSingleton<InputController>
    {
        protected InputController() { }

        public event InputMoveHandler OnHandleMove;
        public event InputMoveEndHandler OnHandleMoveEnd;

        private DanceMovementInput _danceControls;

        private void Awake()
        {
            Debug.Log("InputController Is Awake");
            _danceControls = new DanceMovementInput();
        }

        private void OnEnable()
        {
            _danceControls.Enable();
            _danceControls.Player.Move.performed += HandleMove;
            _danceControls.Player.Move.canceled += HandleMoveEnd;
        }



        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }


        public void HandleMove(InputAction.CallbackContext context)
        {
            OnHandleMove?.Invoke(context);
        }

        private void HandleMoveEnd(InputAction.CallbackContext context)
        {
            OnHandleMoveEnd?.Invoke(context);
        }


        private void OnDisable()
        {
            _danceControls.Player.Move.performed -= HandleMove;
            _danceControls.Player.Move.canceled -= HandleMoveEnd;
            _danceControls.Disable();
        }
    }
}