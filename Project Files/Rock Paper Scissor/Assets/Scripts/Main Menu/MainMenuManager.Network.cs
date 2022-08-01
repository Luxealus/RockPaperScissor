using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public partial class MainMenuManager : MonoBehaviourPunCallbacks
{
	private void ConnectPUN() {
		if (PhotonNetwork.IsConnected) {
			OnConnectedToMaster();
		} else {
			PhotonNetwork.ConnectUsingSettings();
		}
	}

	public override void OnConnectedToMaster() {
		PhotonNetwork.AutomaticallySyncScene = true;
		findMatchButton.interactable = true;
		connectivityText.text = "Connected to server";
	}

	private void CreateMultiplayerRoom() {
		RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 2 };
		PhotonNetwork.CreateRoom($"Room {Random.Range(0, 10000)}", roomOptions);
	}

	public override void OnJoinRandomFailed(short returnCode, string message) {
		CreateMultiplayerRoom();
	}

	public override void OnCreateRoomFailed(short returnCode, string message) {
		CreateMultiplayerRoom();
	}

	public override void OnEnable() {
		PhotonNetwork.AddCallbackTarget(this);
	}

	public override void OnDisable() {
		PhotonNetwork.RemoveCallbackTarget(this);
	}

	public override void OnJoinedRoom() {
		cancelFindMatchButton.interactable = PhotonNetwork.IsMasterClient;
	}

	public override void OnPlayerEnteredRoom(Player newPlayer) {
		cancelFindMatchButton.interactable = false;
		if (PhotonNetwork.IsMasterClient) {
			PhotonNetwork.LoadLevel(2);
		}
	}
}
