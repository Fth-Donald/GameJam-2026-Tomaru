using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class WaveTxtScript : MonoBehaviour
{
    public WaveScript waveScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float WaveStartTxtTimer=1f;
    private bool IsStart = true;
    public TMP_Text timerText;
    private float scale=1f;
    void Update()
    {
        if (!IsStart) return;
        if(WaveStartTxtTimer < 0)
        {
            IsStart = false;
            timerText.gameObject.SetActive(false);
            WaveStartTxtEnd();
        }
        UpdateUI();
    }
    void UpdateUI()
    {
        scale += 4*Time.deltaTime;
        WaveStartTxtTimer -=Time.deltaTime;
        timerText.transform.localScale = Vector3.one * scale;
    }
    public void WaveStartTxtStart()
    {
        timerText.gameObject.SetActive(true);
        IsStart = true;
        WaveStartTxtTimer = 1f;
    }
    public void WaveStartTxtEnd()
    {
        waveScript.WaveSpawn();
    }
}
