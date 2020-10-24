using GameJam.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Core
{
    public enum CoreState { INIT, MAIN_MENU, PLAY, NEW_GAME }

    public delegate void OnChangeGameStateHandler();

    public class CoreManager : CoreSingleton<CoreManager>
    {
        protected CoreManager() { }

        public event OnChangeGameStateHandler OnStateChange;


        //Systems to Init
        public CoreData CoreData;
        public InputController InputController;

        public CoreState CoreState { get; private set; }

        private void Awake()
        {
            Debug.Log("CoreManager Is Awake");
            CoreData = CoreData.Instance;
            InputController = InputController.Instance;
        }

        private void OnEnable()
        {
            CoreData.OnDataLoadComplete += CoreData_OnDataLoadComplete;
        }

        private void CoreData_OnDataLoadComplete()
        {
            Debug.Log("Manager Says: \"CoreData has been Loaded\"");
        }

        void Start()
        {
            Debug.Log("CoreManager Start");
        }

        void Update()
        {

        }

        public void SetGameState(CoreState state)
        {
            Debug.LogError("Changing State to " + state);
            CoreState = state;
            OnStateChange?.Invoke();
        }

        private void OnDisable()
        {
            CoreData.OnDataLoadComplete -= CoreData_OnDataLoadComplete;
        }
    }
}
