using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameJam.Rhythm
{
    public class BeatController : MonoBehaviour
    {
        private Keyboard _keyboard;
        private float _beatTempo;

        public float tempo;
        public bool hasStarted;

        // Start is called before the first frame update
        void Start()
        {
            _beatTempo = tempo / 60f;
            _keyboard = Keyboard.current;
        }

        // Update is called once per frame
        void Update()
        {
            if (!hasStarted)
            {
                if (_keyboard.anyKey.wasPressedThisFrame)
                {
                    hasStarted = true;
                }
            }
            else
            {
                transform.position += new Vector3(-_beatTempo * Time.deltaTime, 0f);
            }
        }
    }
}