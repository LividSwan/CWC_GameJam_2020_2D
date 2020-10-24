using GameJam.Input;
using GameJam.Statics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameJam.Core
{
    public class CoreInitialize : MonoBehaviour
    {
        private CoreManager _coreManager;

#pragma warning disable 0649
        [SerializeField] private string _levelToLoad;
        [SerializeField] private bool _levelLoaderDebug;
        [SerializeField] private CoreState _gameStateInitial;
        [SerializeField] private CoreState _gameStateToChangeTo;
#pragma warning restore 0649


        private void Awake()
        {
            _coreManager = CoreManager.Instance;
            _coreManager.SetGameState(_gameStateInitial);
        }

        private void OnEnable()
        {
            //Register for Events
            _coreManager.OnStateChange += HandleOnStateChange;
            CoreData.OnDataLoadComplete += CoreData_OnDataLoadComplete;
        }

        private void CoreData_OnDataLoadComplete()
        {
            //Change to Next State
            _coreManager.SetGameState(_gameStateToChangeTo);
        }

        private void HandleOnStateChange()
        {
            Debug.LogError($"Current State is {_coreManager.CoreState}");

            if (_coreManager.CoreState == _gameStateToChangeTo)
            {
                Debug.Log("Handling state change to " + _coreManager.CoreState);
                ////DO STATE SPECIFIC TASKS HERE when it changes
                LevelLoader.LoadLevelDebug = true;
                StartCoroutine(LevelLoader.LoadNamedSceneAsync(_levelToLoad,
                    () => LevelLoader.UnLoadNamedSceneAsync("Initialize"))
                    );
            }
        }

        private void OnDisable()
        {
            //Clear any Delegates being referenced
            _coreManager.OnStateChange -= HandleOnStateChange;
            CoreData.OnDataLoadComplete -= CoreData_OnDataLoadComplete;
        }
    }
}