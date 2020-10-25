using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameJam.DataAsset
{
    [CreateAssetMenu(fileName = "SongInfo", menuName = "GameJam/DataAsset/SongInfo", order = 2)]
    public class SongInfo : ScriptableObject
    {
#pragma warning disable 0649
        [SerializeField] private int _id;
        [SerializeField] private List<string> _loreTextList;
#pragma warning restore 0649

        public string SongName;
        public AudioClip SongAudioClip;
        public float _beatTempo;
    }
}
