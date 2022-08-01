using UnityEngine;

public class EnemySignCard : BaseSignCard
{
	[SerializeField] private GameObject rockOverlay;
	[SerializeField] private GameObject paperOverlay;
	[SerializeField] private GameObject scissorOverlay;
	private bool isRotating = false;
	private float angleProgress = 0;

	protected override void OnUpdate() {
		if (isRotating) {
			transform.Rotate(Vector3.up * 180f * Time.deltaTime);
			angleProgress += 180f * Time.deltaTime;
			if (angleProgress >= 180f) {
				transform.rotation = Quaternion.Euler(0, 180f, 0);
				isRotating = false;
			}
		}
	}

	public void SetSignID(SignID signID) {
		StartMoving(new Vector3(0, 3f, 0), 13f);
		switch (signID) {
			case SignID.Rock:
				rockOverlay.SetActive(true);
				break;
			case SignID.Paper:
				paperOverlay.SetActive(true);
				break;
			case SignID.Scissor:
				scissorOverlay.SetActive(true);
				break;
		}
	}

	public void FlipCard() {
		isRotating = true;
		angleProgress = 0;
	}

	public void ReturnCard() {
		StartMoving(new Vector3(0, 7.5f, 0), 15f);
	}

	public void ResetState() {
		rockOverlay.SetActive(false);
		paperOverlay.SetActive(false);
		scissorOverlay.SetActive(false);
		transform.rotation = Quaternion.Euler(0, 0, 0);
	}
}
