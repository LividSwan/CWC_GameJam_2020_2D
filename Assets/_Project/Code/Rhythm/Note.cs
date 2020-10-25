using GameJam.Core;
using GameJam.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static GameJam.Rhythm.MusicConductor;

namespace GameJam.Rhythm
{
    public class Note : MonoBehaviour
    {
        private InputController _inputController;
        private string _key1;
        private string _key2;

        private float _finalXPosition;

        public bool canBePressed;

        private float _beatTempo;
        private float _tempo;

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
            _beatTempo = _tempo / 60f;
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
            //transform.position += Vector3.Lerp(transform.position, new Vector3(-_beatTempo * Time.deltaTime, 0f), -_beatTempo * Time.deltaTime);

            if (transform.position.x < _finalXPosition)
            {
                NotePool.Instance.ReturnToPool(this);
            }
        }

        public void SpawnNote(ArrowDirection arrowdirection, Vector3 spawnPosition, float tempo, float finalXPosition)
        {
            switch (arrowdirection)
            {
                case ArrowDirection.UP:
                    _key1 = "w";
                    _key2 = "upArrow";
                    transform.Rotate(0f, 0f, 0f);
                    break;
                case ArrowDirection.DOWN:
                    _key1 = "s";
                    _key2 = "downArrow";
                    transform.Rotate(0f, 0f, 180f);
                    break;
                case ArrowDirection.LEFT:
                    _key1 = "a";
                    _key2 = "leftArrow";
                    transform.Rotate(0f, 0f, 90f);
                    break;
                case ArrowDirection.RIGHT:
                    _key1 = "d";
                    _key2 = "rightArrow";
                    transform.Rotate(0f, 0f, -90f);
                    break;
                default:
                    break;
            }

            transform.position = spawnPosition;
            _tempo = tempo;
            _finalXPosition = finalXPosition;
            this.gameObject.SetActive(true);
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
                    NotePool.Instance.ReturnToPool(this);
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