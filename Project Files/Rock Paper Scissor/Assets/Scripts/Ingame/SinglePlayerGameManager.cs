using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class SinglePlayerGameManager : BaseGameManager
{
	protected override void OnPlayerInputSign(SignID signID) {
		float randomFloat = Random.Range(0, 100f);
		if (randomFloat < 33f) {
			EnemyMoves(SignID.Rock);
		} else if (randomFloat < 66f) {
			EnemyMoves(SignID.Paper);
		} else {
			EnemyMoves(SignID.Scissor);
		}
	}

	public void ReturnFromSinglePlayerButton() { // Button Function
		SceneManager.LoadScene(0);
	}
}
