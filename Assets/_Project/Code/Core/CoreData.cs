using GameJam.DataAsset;
using System;
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

        public Dictionary<string, SongInfo> SongDictionary = new Dictionary<string, SongInfo>();

        protected CoreData() { }

        private async void Awake()
        {
            coreDataLoaded = false;
            await LoadCoreDataDefaults();
        }

        private async Task LoadCoreDataDefaults()
        {
            await GetDataAssetsToLoadAsync();

            Task task1 = LoadSongsData();
            //Task task2 = LoadSpriteSheetData();
            //Task task3 = LoadBackstoryData();
            //Task task4 = LoadMercenaryDefaults();

            //Load new Game data or Saved Game Data HERE
            //Task gameDataTask = LoadNewGameDataAsync();

            //await Task.WhenAll(task1, task2, task3, task4, gameDataTask);

            await Task.WhenAll(task1);

            coreDataLoaded = true;
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

        private async Task LoadSongsData()
        {
            AsyncOperationHandle<IList<SongInfo>> songDataHandle = Addressables.LoadAssetsAsync<SongInfo>(_dataAssetsToLoad.SongsDataLabel, LoadSongsCompleted);
            await songDataHandle.Task;

            foreach (var item in SongDictionary)
            {
                Debug.Log(item.Key);
            }
        }

        private void LoadSongsCompleted(SongInfo songDataObject)
        {
            if (!SongDictionary.ContainsKey(songDataObject.SongName))
            {
                SongDictionary.Add(songDataObject.SongName, songDataObject);
            }
        }
    }
}
