using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField] int seconds;

    private TextMeshProUGUI counter;

    // Start is called before the first frame update
    void Start()
    {
        counter = GetComponent<TextMeshProUGUI>();
        InvokeRepeating("CountDown", 1f, 1f);
    }

    void CountDown() {
        seconds--;
        if (seconds == 0) {
            StopCounting();
            FindObjectOfType<GameSession>().ProcessLostLive();
        }
        counter.text = seconds.ToString();
    }

    public void StopCounting() {
        CancelInvoke();
    }

    public int getSeconds() {
        return seconds;
    }


}
