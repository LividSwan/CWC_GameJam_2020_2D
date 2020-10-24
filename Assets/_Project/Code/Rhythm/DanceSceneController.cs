using GameJam.Core;
using GameJam.Input;
using GameJam.Statics;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameJam.Rhythm
{
    public class DanceSceneController : MonoBehaviour
    {
        private CoreManager _coreManager;
        private Keyboard _keyboard;

        private bool _danceSceneLoaded;

        private void Awake()
        {
            _coreManager = CoreManager.Instance;
            _danceSceneLoaded = false;
        }

        // Start is called before the first frame update
        void Start()
        {
            _keyboard = Keyboard.current;
        }

        // Update is called once per frame
        void Update()
        {
            if (_keyboard.f1Key.wasReleasedThisFrame)
            {
                if (!_danceSceneLoaded)
                {
                    LoadDanceScene();
                }
            }
            if (_keyboard.f12Key.wasReleasedThisFrame)
            {
                if (_danceSceneLoaded)
                {
                    UnLoadDanceScene();
                }
            }
        }

        private void LoadDanceScene()
        {
            LevelLoader.LoadLevelDebug = true;
            StartCoroutine(LevelLoader.LoadNamedSceneAsyncAdditive("DanceScene",
                () => _danceSceneLoaded = true));
        }

        private void UnLoadDanceScene()
        {
            LevelLoader.LoadLevelDebug = true;
            StartCoroutine(LevelLoader.UnLoadNamedSceneAsync("DanceScene",
                () => _danceSceneLoaded = false));
        }

        
    }
}