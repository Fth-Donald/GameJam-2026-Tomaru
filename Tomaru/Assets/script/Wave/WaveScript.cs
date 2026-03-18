using System.Collections.Generic;
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
    public int WaveCnt=1;
    //是否在波次中
    bool IsWave=true;
    //故事波次上限
    public int StoryLimit;
    //波次上限
    private int WaveLimit=10;

    List<GameObject> waveList = new List<GameObject>();
    void EndWave()
    {
        IsWave = false;
        WaveCnt++;
        if (WaveCnt < WaveLimit)
            waveTimerScript.TimerStart();
        else
        {
            Debug.Log("win!");
        }
        

    }
    public void WaveSpawn()
    {
        KillCnt = 0;
        switch(WaveCnt)
        {
            case 1:
                waveList.Add(e_S_Script.enemyPrefabs[0]);
                waveList.Add(e_S_Script.enemyPrefabs[1]);
                break;
            case 2:
                waveList.Remove(e_S_Script.enemyPrefabs[0]);
                waveList.Remove(e_S_Script.enemyPrefabs[1]);
                waveList.Add(e_S_Script.enemyPrefabs[2]);
                break;
            case 3:
                
                break;
            case 4:
               
                break;
            default:
                
                break;
        }
        //WaveEnemyPrefabs[0];
        StartCoroutine(e_S_Script.SpawnEnemies(waveList.ToArray(), TargetKillCnt*WaveCnt, 1f));
    }
    //void Start()
    //{
    //    WaveSpawn();
    //}
    public void OnEnemyKilled()
    {
        KillCnt++;
        Debug.Log("击杀数: ++");

        if (KillCnt >= TargetKillCnt * WaveCnt)
        {
            EndWave();
        }
    }
}
