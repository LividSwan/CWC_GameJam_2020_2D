using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace GameJam.Statics
{
    /// <summary>
    /// Level Loader allows static calls to an Async Operation to LoadSceneAsync.
    /// Provides Warning Logs on Level Progress (@todo -- add class level bool to flag debug on or off)
    /// </summary>
    public static class LevelLoader
    {
        public static bool LoadLevelDebug = false;

        #region LoadNamedScene
        /// <summary>
        /// Load the scene with a given name with the option for Actions to get the Progress
        /// as well as a method for when it has Completed.
        /// </summary>

        public static IEnumerator LoadNamedSceneAsync(string sceneName, Action<float> OnProgress, Action OnComplete)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = false;

            while ((operation.progress / 0.9f) < 1f)
            {
                LogProgress(sceneName, operation.progress / 0.9f);
                OnProgress(operation.progress / 0.9f);
                yield return null;
            }

            LogComplete(sceneName);
            operation.allowSceneActivation = true;
            OnComplete();
        }

        public static IEnumerator LoadNamedSceneAsync(string sceneName, Action OnComplete)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = false;

            while ((operation.progress / 0.9f) < 1f)
            {
                LogProgress(sceneName, operation.progress / 0.9f);
                yield return null;
            }

            LogComplete(sceneName);
            operation.allowSceneActivation = true;
            OnComplete();
        }

        public static IEnumerator LoadNamedSceneAsync(string sceneName, Action<float> OnProgress)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = true;

            while ((operation.progress / 0.9f) < 1f)
            {
                LogProgress(sceneName, operation.progress / 0.9f);
                OnProgress(operation.progress / 0.9f);
                yield return null;
            }
        }

        public static IEnumerator LoadNamedSceneAsync(string sceneName)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = true;

            while ((operation.progress / 0.9f) < 1f)
            {
                LogProgress(sceneName, operation.progress / 0.9f);
                yield return null;
            }
        }

        public static IEnumerator LoadNamedSceneAsyncAdditive(string sceneName, Action OnComplete)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            operation.allowSceneActivation = false;

            while ((operation.progress / 0.9f) < 1f)
            {
                LogProgress(sceneName, operation.progress / 0.9f);
                yield return null;
            }

            LogComplete(sceneName);
            operation.allowSceneActivation = true;
            OnComplete();
        }

        public static IEnumerator LoadNamedSceneAsyncAdditive(string sceneName)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            operation.allowSceneActivation = true;

            while ((operation.progress / 0.9f) < 1f)
            {
                LogProgress(sceneName, operation.progress / 0.9f);
                yield return null;
            }
        }

        #endregion

        #region UnLoadScene

        public static IEnumerator UnLoadNamedSceneAsync(Scene scene)
        {
            string sceneName = scene.name;
            AsyncOperation operation = SceneManager.UnloadSceneAsync(scene);
            while ((operation.progress / 0.9f) < 1f)
            {
                LogProgress(sceneName, operation.progress / 0.9f);
                yield return null;
            }
        }

        public static IEnumerator UnLoadNamedSceneAsync(string sceneName)
        {
            AsyncOperation operation = SceneManager.UnloadSceneAsync(sceneName);
            while ((operation.progress / 0.9f) < 1f)
            {
                LogProgress(sceneName, operation.progress / 0.9f);
                yield return null;
            }
        }

        public static IEnumerator UnLoadNamedSceneAsync(string sceneName, Action OnComplete)
        {
            AsyncOperation operation = SceneManager.UnloadSceneAsync(sceneName);
            while ((operation.progress / 0.9f) < 1f)
            {
                LogProgress(sceneName, operation.progress / 0.9f);
                yield return null;
            }

            OnComplete();
        }

        #endregion

        private static void LogProgress(string sceneName, float progress)
        {
            if (LoadLevelDebug)
            {
                Debug.LogWarning($"Load Level: \"{sceneName}\"\nProgress: {progress}");
            }
        }

        private static void LogComplete(string sceneName)
        {
            if (LoadLevelDebug)
            {
                Debug.LogWarning($"Scene [\"{sceneName}\"] Loaded");
            }
        }
    }
}