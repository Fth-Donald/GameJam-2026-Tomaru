using UnityEngine;

public class WaveScript : MonoBehaviour
{
   
    public Enemy_Spawner e_S_Script;
    public WaveTimerScript waveTimerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //需求击杀数
    public int TargetKillCnt;
    //击杀数
    private int KillCnt=0;
    //波次数
    public int WaveCnt;
    //是否在波次中
    bool IsWave=true;

    void EndWave()
    {
        IsWave = false;
        WaveCnt++;
        waveTimerScript.TimerStart();
    }
    void WaveSpawn()
    {
        StartCoroutine(e_S_Script.SpawnEnemies(e_S_Script.enemyPrefabs, WaveCnt*30, 1f));
    }
    void Start()
    {
        KillCnt = 0;
        WaveSpawn();
    }
    public void OnEnemyKilled()
    {
        KillCnt++;
        Debug.Log("击杀数: ++");

        if (KillCnt >= TargetKillCnt)
        {
            EndWave();
        }
    }
}
