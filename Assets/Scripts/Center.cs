using UnityEngine;

public class Center : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        FindObjectOfType<Counter>().StopCounting();
        StartCoroutine(FindObjectOfType<GameSession>().LoadNextLevel());
    }
}
