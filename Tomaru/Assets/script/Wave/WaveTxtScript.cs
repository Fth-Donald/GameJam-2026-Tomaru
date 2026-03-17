using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class WaveTxtScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float WaveStartTxtTimer=1f;
    private bool IsStart = true;
    public TMP_Text timerText;
    private float scale=1f;
    private void Update()
    {
        if (!IsStart) return;
        if(WaveStartTxtTimer < 0)
        {
            IsStart = false;
            timerText.gameObject.SetActive(false);
        }
        UpdateUI();
    }
    void UpdateUI()
    {
        scale += Time.deltaTime;

        timerText.transform.localScale = Vector3.one * scale;
    }
    public void WaveStartTxtStart()
    {
        timerText.gameObject.SetActive(true);
        IsStart = true;
    }
}
