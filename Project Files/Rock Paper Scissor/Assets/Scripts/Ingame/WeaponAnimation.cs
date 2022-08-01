using System;
using UnityEngine;

public class WeaponAnimation : MonoBehaviour
{
	public Action AttackHitEvent;

	[SerializeField] private Animator animator;

	public void PlayAttackAnimation() {
		animator.Play("Attack");
	}

	public void OnAttackHit() { // Animation Event
		AttackHitEvent?.Invoke();
	}
}
