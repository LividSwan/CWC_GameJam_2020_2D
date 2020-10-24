using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameJam.Rhythm
{
    public class MusicConductor : MonoBehaviour
    {
        public enum Rank { PERFECT, GOOD, BAD, MISS };

		public delegate void BeatOnHitAction(int trackNumber, Rank rank);
		public static event BeatOnHitAction beatOnHitEvent;

		//song completion
		public delegate void SongCompletedAction();
		public static event SongCompletedAction songCompletedEvent;

		private float songLength;

		//if the whole game is paused
		public static bool paused = true;
		private bool songStarted = false;

		public static float pauseTimeStamp = -1f; //negative means not managed
		private float pausedTime = 0f;

		//private SongInfo songInfo;

		[Header("Spawn Points")]
		public float[] trackSpawnPosY;

		public float startLineX;
		public float finishLineX;

		public float removeLineX;

		public float badOffsetX;
		public float goodOffsetX;
		public float perfectOffsetX;

		//different colors for each track
		public Color[] trackColors;

		//current song position
		public static float songposition;

		//how many seconds for each beat
		public static float crotchet;

		//index for each tracks
		private int[] trackNextIndices;

		//queue, saving the MusicNodes which currently on screen
		private Queue<Note>[] queueForTracks;

		//multi-times notes might be paused on the finish line, but already dequed
		private Note[] previousMusicNodes;

		//keep a reference of the sound tracks
		//private SongInfo.Track[] tracks;

		private float dsptimesong;

		public static float BeatsShownOnScreen = 4f;

		//count down canvas
		private const int StartCountDown = 3;
		public GameObject countDownCanvas;
		public TextMeshPro countDownText;

		//layer each music node, so that the first one would be at the front
		private const float LayerOffsetZ = 0.001f;
		private const float FirstLayerZ = -6f;
		private float[] nextLayerZ;

		//total tracks
		private int len;
		private AudioSource audioSource { get { return GetComponent<AudioSource>(); } }


	}
}