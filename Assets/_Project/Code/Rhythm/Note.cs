using GameJam.Core;
using GameJam.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameJam.Rhythm
{
    public class Note : MonoBehaviour
    {
        private InputController _inputController;
        private string _key1;
        private string _key2;

        private float _removeAtX = -20;

        public bool canBePressed;
        public List<KeyCode> keyCodeList;

        private float _beatTempo;
        public float tempo;

        private void Awake()
        {
            _inputController = InputController.Instance;

            _key1 = Char.ToLowerInvariant(keyCodeList[0].ToString()[0]) + keyCodeList[0].ToString().Substring(1);
            _key2 = Char.ToLowerInvariant(keyCodeList[1].ToString()[0]) + keyCodeList[1].ToString().Substring(1);
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
            _beatTempo = tempo / 60f;
        }

        // Update is called once per frame
        void Update()
        {
            //transform.position = Vector2.Lerp(
            //    SpawnPos,
            //    RemovePos,
            //    (BeatsShownInAdvance - (beatOfThisNote - songPosInBeats)) / BeatsShownInAdvance
            //    );

            transform.position += new Vector3(-_beatTempo * Time.deltaTime, 0f);

            if (transform.position.x < _removeAtX)
            {
                NotePool.Instance.ReturnToPool(this);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Activator")
            {
                canBePressed = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == "Activator")
            {
                canBePressed = false;
            }
        }

        public void HandleMove(InputAction.CallbackContext context)
        {
            string keyPressed = context.control.name;

            if (canBePressed)
            {
                if (keyPressed == _key1 || keyPressed == _key2)
                {
                    //Debug.Log($"KeyCodes = {_key1} {_key2} keyPressed = {keyPressed}");
                    gameObject.SetActive(false);
                }
            }
        }

        private void HandleMoveEnd(InputAction.CallbackContext context)
        {

        }


        private void OnDisable()
        {
            //DeRegister Events
            _inputController.OnHandleMove -= HandleMove;
            _inputController.OnHandleMoveEnd -= HandleMoveEnd;
        }
    }
}