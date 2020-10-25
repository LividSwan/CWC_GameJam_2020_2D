using GameJam.Core;
using GameJam.Statics;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameJam.UI
{
    public class MainMenuController : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private string startSceneToLoad;
#pragma warning restore 0649

        private CoreManager _coreManager;
        private GameObject background;

        private float xBounds = 8;
        private float yBounds = 4;
        private Vector3 bgDirection;
        public float bgSpeed;


        public void Awake()
        {
            Debug.Log("MainMenu UI is Awake");
            _coreManager = CoreManager.Instance;

            background = GameObject.Find("Background");
            bgDirection = new Vector3(Random.Range(-1f, 1f), (Random.Range(-1f, 1f)), 0);
        }

        void Update()
        {

            MenuBackgroundMovement();

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

        public void MenuBackgroundMovement()
        {
            if (background.transform.position.x > xBounds || background.transform.position.x < -xBounds)
            {
                bgDirection = new Vector3(-bgDirection.x, Random.Range(-1f, 1f), 0);
            }

            if (background.transform.position.y > yBounds || background.transform.position.y < -yBounds)
            {
                bgDirection = new Vector3(Random.Range(-1f, 1f), -bgDirection.y, 0);
            }

            background.transform.Translate(bgDirection * Time.deltaTime * bgSpeed);
        }

    }
}