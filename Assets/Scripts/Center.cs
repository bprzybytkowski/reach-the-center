using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Center : MonoBehaviour {
    [SerializeField] float levelLoadDelay = 1.0f;

    private void OnTriggerEnter2D(Collider2D other) {
        FindObjectOfType<Counter>().StopCounting();
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel() {
        Circle[] circles = FindObjectsOfType<Circle>();
        foreach (Circle circle in circles) {
            circle.StopSpinning();
        }
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(++currentSceneIndex);
    }


}
