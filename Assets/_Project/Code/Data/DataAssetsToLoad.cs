using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameJam.DataAsset
{
    [CreateAssetMenu(fileName = "DataAssetsToLoad", menuName = "GameJam/DataAsset/DataAssetsToLoad", order = 1)]
    public class DataAssetsToLoad : ScriptableObject
    {
        [Header("AssetLabels")]
        public string GenericAssetsToLoadLabelName;

        //[Header("Text Assets to Load")]
        //public AssetReference TextAddress;

        //[Header("SpriteImages to Load")]
        //public List<AssetReference> logoSpriteSheets;
        //public Sprite spriteAssetToLoad;

        //[Header("Player New Game Assets to Load")]
        //public AssetReference PlayerNewGameData;

    }
}

