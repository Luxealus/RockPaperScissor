using UnityEngine;

public class BaseSignCard : MonoBehaviour
{
	private Vector3 moveTargetPoint;
	private float speed;
	private bool isMoving = false;

	private void Update() {
		OnUpdate();
		if (isMoving) {
			transform.position = Vector3.MoveTowards(transform.position, moveTargetPoint, speed * Time.deltaTime);
			if (transform.position == moveTargetPoint) {
				isMoving = false;
			}
		}
	}

	protected void StartMoving(Vector3 targetPosition, float speed) {
		moveTargetPoint = targetPosition;
		this.speed = speed;
		isMoving = true;
	}

	protected virtual void OnUpdate() { }
}
