using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class WaveTxtScript_new : MonoBehaviour
{
    public WaveScript waveScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float WaveStartTxtTimer=0f;
    private float WaveTxtTimer=0f;
    private bool waveEnded = true;
    private bool startEnded = true;
    public TMP_Text timerText;
    private float scale=1f;

    private void Start()
    {
        WaveCntTxtStart();
    }
    void Update()
    {
        if (WaveTxtTimer > 0)
        {
            UpdateWaveUI();
        }
        else if (!waveEnded)
        {
            waveEnded = true;
            WaveCntTxtEnd();
        }

        if (WaveStartTxtTimer > 0)
        {
            UpdateStartUI();
        }
        else if (!startEnded)
        {
            startEnded = true;
            timerText.gameObject.SetActive(false);
            WaveStartTxtEnd();
        }
    }
    void UpdateStartUI()
    {
        if (WaveStartTxtTimer > 0)
        {
            WaveStartTxtTimer -= Time.deltaTime;
            //scale += 4*Time.deltaTime;
            timerText.transform.localScale = Vector3.one * scale;
        }
    }
    public void WaveStartTxtStart()
    {
        startEnded = false;
        scale = 1f;
        timerText.text = "Start";
        WaveStartTxtTimer = 2f;
    }
    public void WaveCntTxtStart()
    {
        waveEnded = false;
        scale = 1f;
        timerText.gameObject.SetActive(true);
        WaveTxtTimer = 2f;
        timerText.text = "Wave " + waveScript.WaveCnt;
    }
    void UpdateWaveUI()
    {
        if (WaveTxtTimer > 0){
            WaveTxtTimer -= Time.deltaTime;
            //scale += 4 * Time.deltaTime;
            timerText.transform.localScale = Vector3.one * scale;
        }
    }
    
    private void WaveCntTxtEnd()
    {
        WaveStartTxtStart();
    }
    public void WaveStartTxtEnd()
    {
        waveScript.WaveSpawn();
    }
}
