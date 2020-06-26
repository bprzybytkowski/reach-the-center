using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour {
    [SerializeField] float retryDelay = 0.5f;
    [SerializeField] float sessionResetDelay = 1f;

    private void Awake() {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ProcessRetry() {
        Circle[] circles = FindObjectsOfType<Circle>();
        foreach (Circle circle in circles) {
            circle.StopSpinning();
        }
        StartCoroutine(Retry());
        //StartCoroutine(ResetGameSession());
    }

    IEnumerator Retry() {
        yield return new WaitForSeconds(retryDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator ResetGameSession() {
        yield return new WaitForSeconds(sessionResetDelay);
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

}
