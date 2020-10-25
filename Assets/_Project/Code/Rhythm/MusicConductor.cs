using GameJam.Core;
using GameJam.DataAsset;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameJam.Rhythm
{
    public class MusicConductor : MonoBehaviour
    {
		private CoreManager _coreManager;



		public enum Rank { PERFECT, GOOD, BAD, MISS };

		public delegate void BeatOnHitAction(int trackNumber, Rank rank);
		public static event BeatOnHitAction beatOnHitEvent;

		//song completion
		public delegate void SongCompletedAction();
		public static event SongCompletedAction songCompletedEvent;

		private float songLength;

		//if the whole game is paused
		public static bool paused = true;
		private bool _songStarted = false;

		public static float pauseTimeStamp = -1f; //negative means not managed
		private float pausedTime = 0f;

		private SongInfo _songInfo;

		[Header("Spawn Points")]
		public float trackSpawnPosY;

		public float startLineX;
		public float finishLineX;

		public float npcActivateLineX;

		public float removeLineX;

		public float badOffsetX;
		public float goodOffsetX;
		public float perfectOffsetX;

		[Header("Count Down Panel")]
		public GameObject countDownPanel;
		public TMP_Text countDownText;

		private const int _countDownStart = 3;

		[Header("Note Tracks")]
		public GameObject noteTracks;

		//current song position
		public static float songposition;

		//how many seconds for each beat
		public static float secondsPerBeat;

		private float dsptimesong;

		public static float BeatsShownOnScreen = 4f;

		

		private AudioSource _audioSource;

        private void Awake()
        {
			_coreManager = CoreManager.Instance;
			_audioSource = GetComponent<AudioSource>();
		}

        private void Start()
        {
			//display countdown canvas
			countDownPanel.SetActive(true);
			noteTracks.SetActive(false);

			//get the song info from Manager
			_songInfo = _coreManager.GetCurrentSong();

			//initialize fields
			secondsPerBeat = 60f / _songInfo._beatTempo;
			songLength = _songInfo.SongAudioClip.length;

			//initialize audioSource
			_audioSource.clip = _songInfo.SongAudioClip;

			//start countdown
			StartCoroutine(CountDown());
		}

		IEnumerator CountDown()
		{
			//yield return new WaitForSeconds(1f);

			for (int i = _countDownStart; i >= 0; i--)
			{
				countDownText.text = i.ToString();

                if (i == 0)
                {
					countDownText.text = "GO!";
				}
				yield return new WaitForSeconds(1f);
			}
			countDownPanel.SetActive(false);
			noteTracks.SetActive(true);
			StartSong();
		}

        private void StartSong()
        {
			//get dsptime
			dsptimesong = (float)AudioSettings.dspTime;

			//play song
			_audioSource.Play();

			_songStarted = true;
		}

        private void Update()
        {
			//for count down
			if (!_songStarted) return;

			//INSERT A PAUSE CHECK HERE IF NEEDED

			//calculate songposition
			//songposition = (float)(AudioSettings.dspTime - dsptimesong - pausedTime) * audioSource.pitch - songInfo.songOffset;
			songposition = (float)(AudioSettings.dspTime - dsptimesong) * _audioSource.pitch;
			//print (songposition);

			//check if need to instantiate new Notes
			float beatToShow = songposition / secondsPerBeat + BeatsShownOnScreen;

			Debug.Log(beatToShow);

			//check to see if the song reaches its end
			if (songposition > songLength)
			{
				_songStarted = false;
                songCompletedEvent?.Invoke();
            }
		}
    }
}