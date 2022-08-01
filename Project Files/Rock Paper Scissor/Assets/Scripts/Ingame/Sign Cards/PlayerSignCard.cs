using System;
using UnityEngine;

public class PlayerSignCard : BaseSignCard
{
	public Action HoverEvent;
	public Action<SignID> SignChosenEvent;

	[SerializeField] private SignID signID;
	[SerializeField] private GameObject onHoverEffect;
	private bool isClickable = false;
	private bool isHovered = false;

#if UNITY_STANDALONE
	private void OnMouseEnter() {
		isHovered = true;
		if (isClickable) OnHover();
}

private void OnMouseExit() {
		isHovered = false;
		if (isClickable) onHoverEffect.SetActive(false);
	}
#endif

	private void OnMouseDown() {
		if (!isClickable) return;
		SignChosenEvent?.Invoke(signID);
		onHoverEffect.SetActive(false);
	}

	public void SetClickable(bool isClickable) {
		this.isClickable = isClickable;
		if (isClickable && isHovered) OnHover();
	}

	public void MoveOutOfScreen() {
		StartMoving(new Vector3(transform.position.x, -7.5f, transform.position.z), 12f);
	}

	public void MoveBackIn() {
		StartMoving(new Vector3(transform.position.x, -3f, transform.position.z), 12f);
	}

	private void OnHover() {
		onHoverEffect.SetActive(true);
		HoverEvent?.Invoke();
	}
}
