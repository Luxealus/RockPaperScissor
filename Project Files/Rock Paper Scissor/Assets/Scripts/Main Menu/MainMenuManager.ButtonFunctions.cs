using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class MainMenuManager
{
	public void SinglePlayer() {
		buttonSFX.Play();
		SceneManager.LoadScene(1);
	}

	public void FindMatch() {
		buttonSFX.Play();
		findMatchButton.interactable = false;
		connectivityText.text = "Not Connected to Network";
		mainMenuWindow.SetActive(false);
		findingMatchWindow.SetActive(true);
		PhotonNetwork.JoinRandomRoom();
	}

	public void QuitGame() {
		buttonSFX.Play();
		Application.Quit();
	}

	public void CancelFindMatch() {
		backSFX.Play();
		mainMenuWindow.SetActive(true);
		findingMatchWindow.SetActive(false);
		cancelFindMatchButton.interactable = false;
		PhotonNetwork.LeaveRoom();
	}
}
