using System.Collections.Generic;
using UnityEngine;

public class WaveScript : MonoBehaviour
{
    public Entity entity;

    [SerializeField] GameObject win;
    public bool lastWave = false;

    public Enemy_Spawner e_S_Script;
    public WaveTimerScript waveTimerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int enemyNub;
    //???????Åh
    private int TargetKillCnt;
    //???Åh
    private int KillCnt=0;
    //Åhg???Åh
    public int WaveCnt=1;
    //??Åh???Åhg??ÅfÅı
    bool IsWave=true;
    //????Åhg??????
    public int StoryLimit;
    //Åhg??????
    private int WaveLimit=10;
    List<GameObject> waveList = new List<GameObject>();
    List<GameObject> wave1List = new List<GameObject>();
    List<GameObject> wave2List = new List<GameObject>();
    List<GameObject> wave3List = new List<GameObject>();
    List<GameObject> wave4List = new List<GameObject>();
    List<GameObject> wave5List = new List<GameObject>();
    List<GameObject> wave6List = new List<GameObject>();
    List<GameObject> Boss1List = new List<GameObject>();
    List<GameObject> Boss2List = new List<GameObject>();
    void EndWave()
    {
        entity.currentHealth = entity.maxHealth;

        IsWave = false;
        WaveCnt++;

        if (lastWave == true)
        {
            win.SetActive(true);
            Time.timeScale = 0f;
            Debug.Log("pause");
        }

        else if (WaveCnt < WaveLimit)
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
        wave3List.Add(e_S_Script.enemyPrefabs[1]);
        wave3List.Add(e_S_Script.enemyPrefabs[5]);
       // wave3List.Add(e_S_Script.enemyPrefabs[3]);
        //4
        wave4List.Add(e_S_Script.enemyPrefabs[0]);
        wave4List.Add(e_S_Script.enemyPrefabs[4]);
        wave4List.Add(e_S_Script.enemyPrefabs[1]);
        wave4List.Add(e_S_Script.enemyPrefabs[5]);
        //5
        wave5List.Add(e_S_Script.enemyPrefabs[0]);
        wave5List.Add(e_S_Script.enemyPrefabs[4]);
       // wave5List.Add(e_S_Script.enemyPrefabs[3]);
        //6
        wave6List.Add(e_S_Script.enemyPrefabs[1]);
        wave6List.Add(e_S_Script.enemyPrefabs[5]);
        wave6List.Add(e_S_Script.enemyPrefabs[2]);
        //boss
        Boss1List.Add(e_S_Script.enemyPrefabs[6]);
        Boss2List.Add(e_S_Script.enemyPrefabs[7]);
    }
    public void WaveSpawn()
    {
        KillCnt = 0;
        switch(WaveCnt)
        {
            case 1:
                waveList.Clear();
                waveList.AddRange(wave1List);
                enemyNub = 30;
                TargetKillCnt = enemyNub;
                break;
            case 2:
                waveList.Clear();
                waveList.AddRange(wave2List);
                enemyNub = 50;
                TargetKillCnt = enemyNub;
                break;
            case 3:
                waveList.Clear();
                waveList.AddRange(wave3List);
                enemyNub = 50;
                TargetKillCnt = enemyNub;
                break;
            case 4:
                waveList.Clear();
                waveList.AddRange(wave4List);
                enemyNub = 60;
                TargetKillCnt = enemyNub;
                break;
            case 5:
                waveList.Clear();
                waveList.AddRange(wave5List);
                enemyNub = 29;
                TargetKillCnt = enemyNub+1+5;
                StartCoroutine(e_S_Script.SpawnEnemies(Boss1List.ToArray(), 1, 1f));
                break;
            case 6:
                lastWave = true;
                waveList.Clear();
                waveList.AddRange(wave6List);
                enemyNub = 29;
                TargetKillCnt = enemyNub + 1;
                StartCoroutine(e_S_Script.SpawnEnemies(Boss2List.ToArray(), 1 ,1f));
                break ;
            default:
                    
                break;
        }
       
        StartCoroutine(e_S_Script.SpawnEnemies(waveList.ToArray(), TargetKillCnt, 1f));
    }
    
    public void OnEnemyKilled()
    {
        KillCnt++;
        Debug.Log("???Åh: ++");
        Debug.Log("KillCnt");
        if (KillCnt >= TargetKillCnt)
        {
            EndWave();
        }
    }
}
