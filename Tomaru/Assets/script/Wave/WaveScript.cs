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
    List<GameObject> wave1List = new List<GameObject>();
    List<GameObject> wave2List = new List<GameObject>();
    List<GameObject> wave3List = new List<GameObject>();
    List<GameObject> wave4List = new List<GameObject>();
    List<GameObject> wave5List = new List<GameObject>();
    List<GameObject> wave6List = new List<GameObject>();
    List<GameObject> wave7List = new List<GameObject>();
    List<GameObject> wave8List = new List<GameObject>();
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
    private void Start()
    {
        //1
        wave1List.Add(e_S_Script.enemyPrefabs[0]);
        wave1List.Add(e_S_Script.enemyPrefabs[1]);
        //2
        wave2List.Add(e_S_Script.enemyPrefabs[0]);
        wave2List.Add(e_S_Script.enemyPrefabs[2]);
        wave2List.Add(e_S_Script.enemyPrefabs[4]);
        //3
        wave3List.Add(e_S_Script.enemyPrefabs[0]);
        wave3List.Add(e_S_Script.enemyPrefabs[5]);
        wave3List.Add(e_S_Script.enemyPrefabs[3]);
        //4
        wave4List.Add(e_S_Script.enemyPrefabs[0]);
        wave4List.Add(e_S_Script.enemyPrefabs[4]);
        wave4List.Add(e_S_Script.enemyPrefabs[1]);
        wave4List.Add(e_S_Script.enemyPrefabs[5]);
        //5
        wave5List.Add(e_S_Script.enemyPrefabs[0]);
        wave5List.Add(e_S_Script.enemyPrefabs[4]);
        wave5List.Add(e_S_Script.enemyPrefabs[3]);
        wave5List.Add(e_S_Script.enemyPrefabs[6]);
        //6
        wave6List.Add(e_S_Script.enemyPrefabs[1]);
        wave6List.Add(e_S_Script.enemyPrefabs[5]);
        wave6List.Add(e_S_Script.enemyPrefabs[2]);
        wave6List.Add(e_S_Script.enemyPrefabs[7]);
    }
    public void WaveSpawn()
    {
        KillCnt = 0;
        switch(WaveCnt)
        {
            case 1:
                waveList.Clear();
                waveList.AddRange(wave1List);
                break;
            case 2:
                waveList.Clear();
                waveList.AddRange(wave2List);
                break;
            case 3:
                waveList.Clear();
                waveList.AddRange(wave3List);
                break;
            case 4:
                waveList.Clear();
                waveList.AddRange(wave4List);
                break;
                case 5:
                waveList.Clear();
                waveList.AddRange(wave5List);
                break;
                case 6:
                waveList.Clear();
                waveList.AddRange(wave6List);
                break ;
            default:
                
                break;
        }
       
        StartCoroutine(e_S_Script.SpawnEnemies(waveList.ToArray(), TargetKillCnt*WaveCnt, 1f));
    }
    
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
