using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using GameJam.Input;

namespace GameJam.Core
{


    public class CoreSoundManager : MonoBehaviour
    {
#pragma warning disable 0649


        [SerializeField] private AudioClip menuHover0;
        [SerializeField] private AudioClip menuClick0;
        [SerializeField] private AudioClip menuTheme0;
        [SerializeField] private AudioClip gameTheme0;
        [SerializeField] private AudioClip mistake;
        [SerializeField] private AudioClip battleTheme0;
        [SerializeField] private AudioClip downButton0;
        [SerializeField] private AudioClip leftButton0;
        [SerializeField] private AudioClip rightButton0;
        [SerializeField] private AudioClip upButton0;
        
        [SerializeField] private AudioClip battleTheme1;
        [SerializeField] private AudioClip downButton1;
        [SerializeField] private AudioClip leftButton1;
        [SerializeField] private AudioClip rightButton1;
        [SerializeField] private AudioClip upButton1;

#pragma warning restore 0649


        private InputController _inputController;

        AudioSource sceneAudioSource;

        // Start is called before the first frame update
        void Start()
        {
            //playerAudioSource = GameObject.Find("Player").GetComponent<AudioSource>();
            sceneAudioSource = GetComponent<AudioSource>();


        }

        // Update is called once per frame
        void Update()
        {

        }

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

        public void HandleMove(InputAction.CallbackContext context)
        {
            if (SceneManager.GetActiveScene().name == "DanceScene")
            {
                string keyPressed = context.control.name;
                switch (context.control.name)
                {
                    case "w":
                    case "upArrow":
                        //Debug.Log("UP (w) Arrow released");
                        sceneAudioSource.PlayOneShot(upButton0);

                        break;
                    case "s":
                    case "downArrow":
                        //Debug.Log("DOWN (s) Arrow released");
                        sceneAudioSource.PlayOneShot(downButton0);

                        break;
                    case "a":
                    case "leftArrow":
                        //Debug.Log("LEFT (a) Arrow released");
                        sceneAudioSource.PlayOneShot(leftButton0);

                        break;
                    case "d":
                    case "rightArrow":
                        //Debug.Log("RIGHT (d) Arrow released");
                        sceneAudioSource.PlayOneShot(rightButton0);

                        break;
                    default:
                        break;
                }
            }
        }

        private void HandleMoveEnd(InputAction.CallbackContext context)
        {
            if (SceneManager.GetActiveScene().name == "DanceScene")
            {
                string keyPressed = context.control.name;
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
        }


        private void OnDisable()
        {
            //DeRegister Events
            _inputController.OnHandleMove -= HandleMove;
            _inputController.OnHandleMoveEnd -= HandleMoveEnd;
        }



        public void menuHoverSound()
        {
            if (SceneManager.GetActiveScene().name == "MenuScene")
            {
                sceneAudioSource.PlayOneShot(menuHover0);
            }

        }

        public void menuClickSound()
        {
            if (SceneManager.GetActiveScene().name == "MenuScene")
            {
                sceneAudioSource.PlayOneShot(menuClick0);
            }

        }

    }



}