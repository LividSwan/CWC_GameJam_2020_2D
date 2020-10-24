using GameJam.Core;
using GameJam.Statics;
using UnityEngine;

namespace GameJam.UI
{
    public class MainMenuController : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private string startSceneToLoad;
#pragma warning restore 0649

        private CoreManager _coreManager;

        public void Awake()
        {
            Debug.Log("MainMenu UI is Awake");
            _coreManager = CoreManager.Instance;
        }

        public void ButtonStartClick()
        {
            _coreManager.SetGameState(CoreState.PLAY);

            LevelLoader.LoadLevelDebug = true;
            StartCoroutine(LevelLoader.LoadNamedSceneAsync(startSceneToLoad,
                () => Debug.Log("Load Complete : Do something cool here")
                ));
            //StartCoroutine(LevelLoader.LoadNamedSceneAsync(startSceneToLoad,
            //    (float progress) => loadingBar.value = progress,
            //    () => Debug.Log("Load Complete : Do something cool here")
            //    ));
        }

        public void ButtonExitClick()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}