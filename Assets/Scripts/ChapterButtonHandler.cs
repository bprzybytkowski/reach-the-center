using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterButtonHandler : MonoBehaviour {
    int chapterNumber;

    void Start() {
        chapterNumber = int.Parse(gameObject.name.Replace("ChapterButton", ""));
    }
    public void StartChapter() {
        SceneManager.LoadScene(chapterNumber == 1 ? 1 : chapterNumber * 5);
    }
}
