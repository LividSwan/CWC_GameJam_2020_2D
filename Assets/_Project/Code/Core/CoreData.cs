using GameJam.DataAsset;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GameJam.Core
{
    /// <summary>
    /// Load and Store the Core Data required for the Game
    /// </summary>
    public class CoreData : CoreSingleton<CoreData>
    {
        public delegate void DataLoadComplete();
        public static event DataLoadComplete OnDataLoadComplete;

        public bool coreDataLoaded;

        private DataAssetsToLoad _dataAssetsToLoad;

        protected CoreData() { }

        private async void Awake()
        {
            Debug.Log("CoreData is Awake");
            //coreManager = CoreManager.Instance;
            coreDataLoaded = false;

            await LoadCoreDataDefaults();
        }

        private async Task LoadCoreDataDefaults()
        {
            await GetDataAssetsToLoadAsync();

            //Task task1 = LoadSystemsSettings();
            //Task task2 = LoadSpriteSheetData();
            //Task task3 = LoadBackstoryData();
            //Task task4 = LoadMercenaryDefaults();

            //Load new Game data or Saved Game Data HERE
            //Task gameDataTask = LoadNewGameDataAsync();

            //await Task.WhenAll(task1, task2, task3, task4, gameDataTask);

            //await Task.WhenAll(task1);

            coreDataLoaded = true;
            Debug.Log("CoreData has LOADED from AWAKE");
            OnDataLoadComplete?.Invoke();
        }

        private async Task GetDataAssetsToLoadAsync()
        {
            AsyncOperationHandle<DataAssetsToLoad> handle = Addressables.LoadAssetAsync<DataAssetsToLoad>("DataAssetsToLoad");
            await handle.Task;
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _dataAssetsToLoad = handle.Result;
            }
            Debug.Log("DataAssetsToLoad == LOADED");
        }

    }
}
