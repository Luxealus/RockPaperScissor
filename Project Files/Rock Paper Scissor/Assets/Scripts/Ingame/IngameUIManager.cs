using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngameUIManager : MonoBehaviour
{
	[SerializeField] private Slider playerHPSlider;
	[SerializeField] private Slider enemyHPSlider;
	[SerializeField] private GameObject gameEndWindow;
	[SerializeField] private TextMeshProUGUI gameEndText;
	[SerializeField] private TextMeshProUGUI postgameMMRText;
	[SerializeField] private Animator vignetteAnimator;
	private Slider currentlyAnimatingSlider;
	private float targetValue;
	private bool isHPBarChanging;

	private void Update() {
		if (!isHPBarChanging) return;

		float sliderValue = currentlyAnimatingSlider.value;
		sliderValue -= 0.25f * Time.deltaTime;
		if (sliderValue < targetValue) {
			sliderValue = targetValue;
			isHPBarChanging = false;
		}
		currentlyAnimatingSlider.value = sliderValue;
	}

	public void DamagePlayer(float percentage) {
		vignetteAnimator.Play("Damaged");
		currentlyAnimatingSlider = playerHPSlider;
		BeginHPBarDamageAnimation(percentage);
	}

	public void DamageEnemy(float percentage) {
		currentlyAnimatingSlider = enemyHPSlider;
		BeginHPBarDamageAnimation(percentage);
	}

	public void SetPostgameTexts(bool isWinning, bool isMultiplayer) {
		int deviceMMR = PlayerPrefs.GetInt("MMR");
		if (isMultiplayer) {
			int mmrModifier = Random.Range(20, 29);
			if (isWinning) {
				gameEndText.text = "YOU WIN!";
				deviceMMR += mmrModifier;
				postgameMMRText.text = $"MMR : {deviceMMR} (+{mmrModifier})";
			} else {
				gameEndText.text = "YOU LOSE!";
				deviceMMR -= mmrModifier;
				if (deviceMMR <= 0) deviceMMR = 1;
				postgameMMRText.text = $"MMR : {deviceMMR} (-{mmrModifier})";
			}
			PlayerPrefs.SetInt("MMR", deviceMMR);
		} else {
			if (isWinning) {
				gameEndText.text = "YOU WIN!";
			} else {
				gameEndText.text = "YOU LOSE!";
			}
			postgameMMRText.text = $"MMR : {deviceMMR} (Unchanged)";
		}
		gameEndWindow.SetActive(true);

	}

	private void BeginHPBarDamageAnimation(float percentage) {
		targetValue = percentage;
		isHPBarChanging = true;
	}
}
