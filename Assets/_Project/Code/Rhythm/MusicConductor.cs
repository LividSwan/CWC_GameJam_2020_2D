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
		public enum ArrowDirection { UP, DOWN, LEFT, RIGHT };

		public delegate void BeatOnHitAction(int trackNumber, Rank rank);
		public static event BeatOnHitAction beatOnHitEvent;

		//song completion
		public delegate void SongCompletedAction();
		public static event SongCompletedAction songCompletedEvent;

		private float songLength;

		private bool _songStarted = false;

		private SongInfo _songInfo;

		[Header("Spawn Points")]
		public float[] trackSpawnPosY;

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
		public NotePool notePool;

		//current song position
		public static float songposition;

		//how many seconds for each beat
		public static float secondsPerBeat;

		private float dsptimesong;

		public static float BeatsShownOnScreen = 4f;

		public int spawnedNotesCount;

		private AudioSource _audioSource;

		private float beatTimer = 0f;
		private float beatCounter = 0f;

		private System.Random random = new System.Random();
		private Array _arrowDirections;

		private void Awake()
        {
			_coreManager = CoreManager.Instance;
			_audioSource = GetComponent<AudioSource>();

			_arrowDirections = Enum.GetValues(typeof(ArrowDirection));
		}

        private void Start()
        {
			//display countdown canvas
			countDownPanel.SetActive(true);
			noteTracks.SetActive(false);

			spawnedNotesCount = 0;

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
			//float beatToShow = songposition / secondsPerBeat + BeatsShownOnScreen;

			beatTimer += Time.deltaTime;

			int randomIndex = UnityEngine.Random.Range(0, trackSpawnPosY.Length+1);
			
			if (beatTimer > secondsPerBeat && beatCounter < 6f)
			{
				var index = random.Next(_arrowDirections.Length);
				ArrowDirection randomArrow = (ArrowDirection)_arrowDirections.GetValue(index);

				Note newNote = notePool.Get();
                newNote.SpawnNote(randomArrow,
                    new Vector3(startLineX, trackSpawnPosY[randomIndex]),
                    _songInfo._beatTempo,
                    removeLineX);
				
				beatCounter++;
				spawnedNotesCount++;
			}

			if (beatTimer >= secondsPerBeat)
			{
				beatTimer = 0f;
			}

            if (beatCounter > 5f)
			{
				StartCoroutine(SkipBeat());
            }

			//check to see if the song reaches its end
			if (songposition > songLength)
			{
				_songStarted = false;
                songCompletedEvent?.Invoke();
            }
		}

		IEnumerator SkipBeat()
		{
			yield return new WaitForSeconds(2f);
			beatCounter = 0f;
		}
	}
}