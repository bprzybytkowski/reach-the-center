using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{

    [SerializeField] int lives = 3;
    [SerializeField] float liveLostDelay = 0.5f;
    [SerializeField] float sessionResetDelay = 1f;

    private void Awake() {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ProcessLostLive() {
        Circle[] circles = FindObjectsOfType<Circle>();
        foreach (Circle circle in circles) {
            circle.StopSpinning();
        }
        if (lives > 1) {
            StartCoroutine(TakeLife());
        } else {
            StartCoroutine(ResetGameSession());
        }
    }

    IEnumerator TakeLife() {
        yield return new WaitForSeconds(liveLostDelay);
        lives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator ResetGameSession() {
        yield return new WaitForSeconds(sessionResetDelay);
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

}
