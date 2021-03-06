﻿using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour {
    [SerializeField] int lives = 3;
    [SerializeField] float liveLostDelay = 0.5f;
    [SerializeField] float sessionResetDelay = 1f;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] float levelLoadDelay = 1.0f;

    [Tooltip("Fast-forward specified number of seconds after starting the scene then pause")]
    [SerializeField] float fastFowardSeconds;
    [SerializeField] bool enableFramerateCounter;
    [SerializeField] bool startPaused;
    GameObject framerateCounter;
    private const int levelsPerChapter = 5;

    private void Awake() {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() {
        livesText.text = lives.ToString();
        if (startPaused) {
            Debug.Break();
        }
        framerateCounter = GameObject.Find("FramerateCounter");
        StartCoroutine(FastForward(fastFowardSeconds));
        if (!enableFramerateCounter) {
            framerateCounter.SetActive(false);
        }
    }

    public void ProcessLostLife() {
        StopCameraColorShift();
        StopCirclesSpinning();
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
        livesText.text = lives.ToString();
    }
    public IEnumerator LoadNextLevel() {
        StopCameraColorShift();
        StopCirclesSpinning();
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex < levelsPerChapter) {
            SceneManager.LoadScene(++currentSceneIndex);
        } else {
            SceneManager.LoadScene(0);
            Destroy(gameObject);
        }
    }

    IEnumerator ResetGameSession() {
        yield return new WaitForSeconds(sessionResetDelay);
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void StopCameraColorShift() {
        FindObjectOfType<CameraColorShift>().StopAllCoroutines();
    }

    private void StopCirclesSpinning() {
        Circle[] circles = FindObjectsOfType<Circle>();
        foreach (Circle circle in circles) {
            circle.StopSpinning();
        }
    }

    IEnumerator FastForward(float seconds) {
        if (seconds > 0) {
            Time.timeScale = seconds * 10;
            yield return new WaitForSecondsRealtime(0.1f);
            Time.timeScale = 1;
            Debug.Break();
        }
    }

    public int GetLives() {
        return lives;
    }

}
