using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class MultiplayerGameManager : BaseGameManager
{
	[Header("Multiplayer")]
	[SerializeField] private TextMeshProUGUI roomText;

	protected override void OnInitialized() {
		roomText.text = PhotonNetwork.CurrentRoom.Name;
	}

	public void ReturnFromMultiplayerButton() { // Button Function
		SceneManager.LoadScene(0);
	}

	public void DisconnectButton() { // Button Function
		SendDisconnectEvent();
		ManualDisconnect();
	}
}
