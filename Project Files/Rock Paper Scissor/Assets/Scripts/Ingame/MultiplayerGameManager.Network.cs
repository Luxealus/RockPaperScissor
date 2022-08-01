using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

public partial class MultiplayerGameManager
{
	private const byte PlayerSelectSign = 0;
	private const byte PlayerDisconnect = 1;

	private void OnEnable() { // Unity Function
		PhotonNetwork.NetworkingClient.EventReceived += OnNetworkEventReceived;
	}

	private void OnDisable() { // Unity Function
		PhotonNetwork.NetworkingClient.EventReceived -= OnNetworkEventReceived;
	}

	private void OnNetworkEventReceived(EventData eventData) {
		switch (eventData.Code) {
			case PlayerSelectSign:
				SignID playerSignID = (SignID)eventData.CustomData;
				EnemyMoves(playerSignID);
				break;
			case PlayerDisconnect:
				EnemyDisconnect();
				break;
		}
	}

	private void SendDisconnectEvent() {
		PhotonNetwork.RaiseEvent(PlayerDisconnect, 0, RaiseEventOptions.Default, SendOptions.SendReliable);
	}

	protected override void OnPlayerInputSign(SignID signID) {
		PhotonNetwork.RaiseEvent(PlayerSelectSign, (int)signID, RaiseEventOptions.Default, SendOptions.SendReliable);
	}

	protected override void OnGameEnding() {
		PhotonNetwork.LeaveRoom();
	}
}
