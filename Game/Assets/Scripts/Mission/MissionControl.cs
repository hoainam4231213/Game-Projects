using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MissionControl : BYSingleton<MissionControl>
{
    public ConfigMissionRecord configMission;
    public ConfigWaveRecord cf_currentWave;
    public List<WaveInits> waveInits;
    private int countEnemy;
    private int numberEnemy;
    private int waveIndex;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        configMission = GameManager.instance.configMission;
        waveInits = configMission.Waves;
        waveIndex = -1;
        yield return new WaitForSeconds(1);
        StartCoroutine("GetNextWave");
    }


    IEnumerator GetNextWave()
    {
        waveIndex++;
        if (waveIndex > waveInits.Count)
        {
            OnEndWave();
        }
        else
        {
            yield return new WaitForSeconds(waveInits[waveIndex].delayTime);
            cf_currentWave = ConfigManager.instance.configWave.GetRecordByKeySearch(waveInits[waveIndex].wave_id);
            List<string> enemies_ids = cf_currentWave.Enemies_id;
            countEnemy = 0;
            numberEnemy = cf_currentWave.Number;
            for(int i = 0; i < cf_currentWave.Number; i++)
            {
                CreateEnemy(enemies_ids.OrderBy(x => Guid.NewGuid()).FirstOrDefault());
            }
            
        }
    }

    private void CreateEnemy(string id)
    {
        ConfigEnemyRecord configEnemy = ConfigManager.instance.configEnemy.GetRecordByKeySearch(id);
        GameObject go_enemy = Instantiate(Resources.Load("Enemy/" + configEnemy.Enemy_id, typeof(GameObject))) as GameObject;
        go_enemy.transform.position = SpawnPos.instance.GetPos().position;
        EnemyControl enemyControl = go_enemy.GetComponent<EnemyControl>();
        enemyControl.Setup(new EnemyDataInit { cf = configEnemy});

    }

    
    public void OnEnemyDead(EnemyControl enemyControl)
    {
        countEnemy++;
        if(countEnemy >= numberEnemy)
        {
            StopCoroutine("GetNextWave");
            StartCoroutine("GetNextWave");
        }
    }

    public virtual void OnEndWave()
    {

    }
}
