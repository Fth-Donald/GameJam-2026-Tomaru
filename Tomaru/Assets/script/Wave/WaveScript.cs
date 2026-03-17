using UnityEngine;

public class WaveScript : MonoBehaviour
{
    public Enemy_Spawner e_S_Script;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //需求击杀数
    public int TargetKillCnt;
    //击杀数
    private int KillCnt;
    //波次数
    private int WaveCnt;
    //是否在波次中
    bool IsWave;
    void EndWave()
    {
        
            IsWave = false;
    }

   //怪物生成
   void WaveSwapn()
    {

    }
    void Update()
    {
        if (IsWave)
        {
            WaveSwapn();
        }
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
