using System.Collections;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour {
    [SerializeField] int seconds;
    [SerializeField] float growScale;
    [SerializeField] float shrinkSpeed;

    private Vector2 grownText;
    private TextMeshProUGUI counter;

    void Start() {
        counter = GetComponent<TextMeshProUGUI>();
        grownText = new Vector2(growScale, growScale);
        counter.transform.localScale = grownText;
        StartCoroutine(ScaleOverTime(shrinkSpeed));
        InvokeRepeating("CountDown", 1f, 1f);
    }

    void CountDown() {
        seconds--;
        counter.text = seconds.ToString();
        if (seconds > 0) {
            StartCoroutine(ScaleOverTime(shrinkSpeed));
        } else {
            counter.transform.localScale = grownText;
            StopCounting();
            FindObjectOfType<GameSession>().ProcessLostLive();
        }
    }

    IEnumerator ScaleOverTime(float time) {
        Vector2 originalScale = grownText;
        Vector2 destinationScale = new Vector2(1, 1);

        float currentTime = 0.0f;

        do {
            counter.transform.localScale = Vector2.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);
    }

    public void StopCounting() {
        CancelInvoke();
    }

    public int getSeconds() {
        return seconds;
    }


}
