using GameJam.Core;
using GameJam.Statics;
using GameJam.UI;
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
        public Animator animator;
        private FadeEffect fadeEffectScript;


        public void Awake()
        {
            _coreManager = CoreManager.Instance;
            fadeEffectScript = GameObject.Find("FadeEffect").GetComponent<FadeEffect>();

            background = GameObject.Find("Background");
            bgDirection = new Vector3(Random.Range(-1f, 1f), (Random.Range(-1f, 1f)), 0);
        }

        void Update()
        {

            MenuBackgroundMovement();

        }


        public void ButtonStartClick()
        {
            fadeEffectScript.FadeOutEffect();
            _coreManager.SetGameState(CoreState.PLAY);
            StartCoroutine(StartGame());
        }

        IEnumerator StartGame()
        {
            //Print the time of when the function is first called.
            //Debug.Log("Started Coroutine at timestamp : " + Time.time);

            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSeconds(3f);

            //After we have waited 3 seconds print the time again.
            //Debug.Log("Finished Coroutine at timestamp : " + Time.time);

            LevelLoader.LoadLevelDebug = true;
            StartCoroutine(LevelLoader.LoadNamedSceneAsync(startSceneToLoad,
                () => Debug.Log("Load Complete : Do something cool here")
                ));
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