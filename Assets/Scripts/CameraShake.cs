using UnityEngine;

public class CameraShake : MonoBehaviour {
	public Transform camTransform;

	[SerializeField] float shakeAmount = 0.1f;

	private float duration = 0.0f;

	Vector3 originalPosition;

	void Awake() {
		if (camTransform == null) {
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}

	void OnEnable() {
		originalPosition = camTransform.localPosition;
	}

	void Update() {
		if (duration > 0) {
			Shake();
			duration -= Time.deltaTime;
		} else if (duration == 0) {
			duration = 0f;
			camTransform.localPosition = originalPosition;
		} else {
			Shake();
		}
	}

	void Shake() {
		camTransform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;
	}

	public void StartShake(float duration) {
		this.duration = duration;
	}
}