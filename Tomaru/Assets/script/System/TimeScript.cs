using TMPro;
using UnityEngine;

public class TimeCountDownScript : MonoBehaviour
{
    public TMP_Text timerText;   // UI文字
    public float timeLeft = 12f; // 倒计时秒数

    private bool isRunning = true;

    void Update()
    {
        if (!isRunning) return;

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            timeLeft = 0;
            isRunning = false;
            TimerEnd();
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (timeLeft > 5f)
        {
            timerText.text = Mathf.CeilToInt(timeLeft).ToString();
        }
        else
        {
            timerText.text = timeLeft.ToString("F2");
            timerText.color = Color.red;
        }
    }

    void TimerEnd()
    {
        Debug.Log("Time Over!");
    }
}
