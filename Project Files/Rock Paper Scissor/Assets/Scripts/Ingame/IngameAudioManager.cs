using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameAudioManager : MonoBehaviour
{
	[Header("BGM")]
	[SerializeField] private AudioSource normalMusic;
	[SerializeField] private AudioSource engageMusic;
	[Header("SFX")]
	[SerializeField] private AudioSource cardFlipSound;
	[SerializeField] private AudioSource hoverCardSound;
	[SerializeField] private AudioSource selectCardSound;

	public void ChangeMusic() {
		normalMusic.volume = 0;
		engageMusic.volume = 0.75f;
	}

	public void RevertMusic() {
		engageMusic.volume = 0;
		normalMusic.volume = 0.65f;
	}

	public void PlayCardFlipSound() {
		cardFlipSound.Play();
	}

	public void PlayHoverCardSound() {
		hoverCardSound.Play();
	}

	public void PlaySelectCardSound() {
		selectCardSound.Play();
	}
}
