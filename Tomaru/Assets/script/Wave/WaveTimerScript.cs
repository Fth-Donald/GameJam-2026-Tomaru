using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class WaveTimerScript : MonoBehaviour
{
    public TMP_Text timerText;   // UI文字
    public WaveScript waveScript;
    public WaveTxtScript　waveTxtScript;
    public float timeLeft = 20f; // 倒计时秒数

    private bool isRunning = false;

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
    public void TimerStart()
    {
        isRunning = true;
    }
    void TimerEnd()
    {
        waveTxtScript.WaveStartTxtStart();
    }
}