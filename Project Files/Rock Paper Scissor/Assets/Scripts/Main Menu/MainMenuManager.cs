using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class MainMenuManager
{
	[Header("General")]
	[SerializeField] private GameObject mainMenuWindow;
	[SerializeField] private GameObject findingMatchWindow;
	[Header("Button")]
	[SerializeField] private Button findMatchButton;
	[SerializeField] private Button cancelFindMatchButton;
	[Header("Text")]
	[SerializeField] private TextMeshProUGUI connectivityText;
	[SerializeField] private TextMeshProUGUI deviceMMRText;
	[Header("Audio")]
	[SerializeField] private AudioSource buttonSFX;
	[SerializeField] private AudioSource backSFX;

	private void Start() {
		if (PlayerPrefs.GetInt("MMR") == 0) {
			PlayerPrefs.SetInt("MMR", 1000);
		}
		deviceMMRText.text = $"Device MMR : {PlayerPrefs.GetInt("MMR")}";
		ConnectPUN();
	}
}
