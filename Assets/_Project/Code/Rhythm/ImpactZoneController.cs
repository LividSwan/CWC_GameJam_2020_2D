using GameJam.Core;
using GameJam.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameJam.Rhythm
{
    public class ImpactZoneController : MonoBehaviour
    {
        private CoreManager _coreManager;
        private InputController _inputController;
        
        private SpriteRenderer _impactSpriteRenderer;

        public Sprite defaultImage;
        public Sprite pressedImage;

        private void Awake()
        {
            _coreManager = CoreManager.Instance;
            _inputController = _coreManager.InputController;
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
            _impactSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {


        }

        public void HandleMove(InputAction.CallbackContext context)
        {
            _impactSpriteRenderer.sprite = pressedImage;

            switch (context.control.name)
            {
                case "w":
                case "upArrow":
                    Debug.Log("UP (w) Arrow pressed");
                    break;
                case "s":
                case "downArrow":
                    //Debug.Log("DOWN (s) Arrow pressed");
                    break;
                case "a":
                case "leftArrow":
                    //Debug.Log("LEFT (a) Arrow pressed");
                    break;
                case "d":
                case "rightArrow":
                    //Debug.Log("RIGHT (d) Arrow pressed");
                    break;
                default:
                    break;
            }
        }

        private void HandleMoveEnd(InputAction.CallbackContext context)
        {
            _impactSpriteRenderer.sprite = defaultImage;

            switch (context.control.name)
            {
                case "w":
                case "upArrow":
                    //Debug.Log("UP (w) Arrow released");
                    break;
                case "s":
                case "downArrow":
                    //Debug.Log("DOWN (s) Arrow released");
                    break;
                case "a":
                case "leftArrow":
                    //Debug.Log("LEFT (a) Arrow released");
                    break;
                case "d":
                case "rightArrow":
                    //Debug.Log("RIGHT (d) Arrow released");
                    break;
                default:
                    break;
            }
        }

        private void OnDisable()
        {
            //DeRegister Events
            _inputController.OnHandleMove -= HandleMove;
            _inputController.OnHandleMoveEnd -= HandleMoveEnd;
        }
    }
}