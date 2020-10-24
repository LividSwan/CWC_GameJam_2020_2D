using GameJam.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameJam.Core
{

    public class CoreInitialize : MonoBehaviour
    {
        private InputController _inputController;

        private void Awake()
        {
            _inputController = InputController.Instance;
        }
    }
}