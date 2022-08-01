using System.Collections;
using UnityEngine;

public partial class BaseGameManager : MonoBehaviour
{
	[Header("Base Game")]
	[SerializeField] private IngameUIManager ingameUIManager;
	[SerializeField] private IngameAudioManager ingameAudioManager;
	[SerializeField] private PlayerSignCard playerRockCard;
	[SerializeField] private PlayerSignCard playerPaperCard;
	[SerializeField] private PlayerSignCard playerScissorCard;
	[SerializeField] private EnemySignCard enemyCard;
	[SerializeField] private WeaponAnimation playerWeaponAnimationScript;
	[SerializeField] private WeaponAnimation enemyWeaponAnimationScript;
	private const float maxHP = 100f;
	private SignID playerSignID, enemySignID;
	private bool isAttackHit = false;
	private float playerHP, enemyHP;
	private int readyCount;

	private void Start() {
		playerRockCard.SignChosenEvent += OnSignChosen;
		playerRockCard.HoverEvent += OnCardHovered;
		playerPaperCard.SignChosenEvent += OnSignChosen;
		playerPaperCard.HoverEvent += OnCardHovered;
		playerScissorCard.SignChosenEvent += OnSignChosen;
		playerScissorCard.HoverEvent += OnCardHovered;
		playerWeaponAnimationScript.AttackHitEvent += OnAttackHit;
		enemyWeaponAnimationScript.AttackHitEvent += OnAttackHit;
		playerHP = maxHP;
		enemyHP = maxHP;
		StartCoroutine(WaitOpeningCinematic());
		OnInitialized();
	}

	private void OnSignChosen(SignID signID) {
		ingameAudioManager.PlaySelectCardSound();
		playerRockCard.SetClickable(false);
		playerPaperCard.SetClickable(false);
		playerScissorCard.SetClickable(false);
		playerSignID = signID;
		switch (signID) {
			case SignID.Rock:
				playerPaperCard.MoveOutOfScreen();
				playerScissorCard.MoveOutOfScreen();
				break;
			case SignID.Paper:
				playerRockCard.MoveOutOfScreen();
				playerScissorCard.MoveOutOfScreen();
				break;
			case SignID.Scissor:
				playerRockCard.MoveOutOfScreen();
				playerPaperCard.MoveOutOfScreen();
				break;
		}
		readyCount++;
		OnPlayerInputSign(signID);
	}

	private void OnCardHovered() {
		ingameAudioManager.PlayHoverCardSound();
	}

	private void OnAttackHit() {
		isAttackHit = true;
	}

	private IEnumerator WaitOpeningCinematic() {
		yield return new WaitForSeconds(4f);
		StartCoroutine(BeginRound());
	}

	private IEnumerator BeginRound() {
		readyCount = 0;
		playerSignID = SignID.Empty;
		enemySignID = SignID.Empty;
		playerRockCard.SetClickable(true);
		playerPaperCard.SetClickable(true);
		playerScissorCard.SetClickable(true);
		enemyCard.ResetState();
		yield return new WaitUntil(() => readyCount >= 2);
		ingameAudioManager.ChangeMusic();
		RoundResult roundResult = DetermineRoundResult();
		yield return new WaitForSeconds(0.35f);
		enemyCard.FlipCard();
		ingameAudioManager.PlayCardFlipSound();
		yield return new WaitForSeconds(1.1f);
		switch (roundResult) {
			case RoundResult.Win:
				playerWeaponAnimationScript.PlayAttackAnimation();
				yield return new WaitUntil(() => isAttackHit);
				isAttackHit = false;
				enemyHP -= 20f;
				ingameUIManager.DamageEnemy(enemyHP / maxHP);
				if (enemyHP <= 0) OnGameEnding();
				yield return new WaitForSeconds(1f);

				break;
			case RoundResult.Lose:
				enemyWeaponAnimationScript.PlayAttackAnimation();
				yield return new WaitUntil(() => isAttackHit);
				isAttackHit = false;
				playerHP -= 20f;
				ingameUIManager.DamagePlayer(playerHP / maxHP);
				if (playerHP <= 0) OnGameEnding();
				yield return new WaitForSeconds(1f);
				break;
			case RoundResult.Draw:
				break;
		}
		if (playerHP <= 0) {
			ingameUIManager.SetPostgameTexts(false, this is MultiplayerGameManager);
		} else if (enemyHP <= 0) {
			ingameUIManager.SetPostgameTexts(true, this is MultiplayerGameManager);
		} else {
			enemyCard.ReturnCard();
			playerRockCard.MoveBackIn();
			playerPaperCard.MoveBackIn();
			playerScissorCard.MoveBackIn();
			yield return new WaitForSeconds(1f);
			ingameAudioManager.RevertMusic();
			StartCoroutine(BeginRound());
		}
	}

	private RoundResult DetermineRoundResult() {
		if (playerSignID == enemySignID) return RoundResult.Draw;
		switch (playerSignID) {
			case SignID.Rock:
				if (enemySignID == SignID.Scissor) {
					return RoundResult.Win;
				} else {
					return RoundResult.Lose;
				}
			case SignID.Paper:
				if (enemySignID == SignID.Rock) {
					return RoundResult.Win;
				} else {
					return RoundResult.Lose;
				}
			case SignID.Scissor:
				if (enemySignID == SignID.Paper) {
					return RoundResult.Win;
				} else {
					return RoundResult.Lose;
				}
			default:
				return RoundResult.Draw;
		}
	}

	protected void EnemyMoves(SignID signID) {
		enemySignID = signID;
		enemyCard.SetSignID(signID);
		readyCount++;
	}

	protected void ManualDisconnect() {
		OnGameEnding();
		ingameUIManager.SetPostgameTexts(false, this is MultiplayerGameManager);
		playerRockCard.SetClickable(false);
		playerPaperCard.SetClickable(false);
		playerScissorCard.SetClickable(false);
	}

	protected void EnemyDisconnect() {
		OnGameEnding();
		ingameUIManager.SetPostgameTexts(true, this is MultiplayerGameManager);
		playerRockCard.SetClickable(false);
		playerPaperCard.SetClickable(false);
		playerScissorCard.SetClickable(false);
	}

	#region Virtual Functions
	protected virtual void OnInitialized() { }
	protected virtual void OnPlayerInputSign(SignID signID) { }
	protected virtual void OnGameEnding() { }
	#endregion
}
