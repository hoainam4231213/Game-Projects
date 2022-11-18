using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MissionControl : BYSingleton<MissionControl>
{
    public ConfigMissionRecord configMission;
    public ConfigWaveRecord cf_currentWave;
    public List<WaveInits> waveInits;
    private int numberEnemy;
    private int waveIndex;
    private string waveName;
    public GameUI gameUI;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        gameUI = GameObject.FindObjectOfType<GameUI>();
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
        if (waveIndex >= waveInits.Count)
        {
            OnEndWave();
        }
        else
        {
            waveName = ConfigManager.instance.configWave.GetRecordByKeySearch(waveInits[waveIndex].wave_id).Wave_name;
            DialogManager.instance.ShowDialog(DialogIndex.WaveDialog);
            yield return new WaitForSeconds(waveInits[waveIndex].delayTime);
            DialogManager.instance.HideDialog(DialogIndex.WaveDialog);
            cf_currentWave = ConfigManager.instance.configWave.GetRecordByKeySearch(waveInits[waveIndex].wave_id);
            List<string> enemies_ids = cf_currentWave.Enemies_id;
            numberEnemy = cf_currentWave.Number;
            gameUI.OnAmountEnemy(numberEnemy);
            for (int i = 0; i < cf_currentWave.Number; i++)
            {
                CreateEnemy(enemies_ids.OrderBy(x => Guid.NewGuid()).FirstOrDefault());
            }
            
        }
    }

    private void CreateEnemy(string id)
    {
        ConfigEnemyRecord configEnemy = ConfigManager.instance.configEnemy.GetRecordByKeySearch(id);
        GameObject go_enemy = Instantiate(Resources.Load("Enemy/" + configEnemy.Enemy_id, typeof(GameObject))) as GameObject;
        Quaternion q = Quaternion.Euler(0, UnityEngine.Random.RandomRange(0, 360f), 0);
        go_enemy.transform.rotation = q;
        Transform point_Trans = SpawnPos.instance.GetPos();
        go_enemy.transform.position = point_Trans.position;
        EnemyControl enemyControl = go_enemy.GetComponent<EnemyControl>();
        enemyControl.Setup(new EnemyDataInit { cf = configEnemy});


    }
    public string GetWaveIndex
    {
        get
        {
            return waveName;
        }
    }

    
    public void OnEnemyDead(EnemyControl enemyControl)
    {
        numberEnemy--;
        if(numberEnemy <= 0)
        {
            StopCoroutine("GetNextWave");
            StartCoroutine("GetNextWave");
        }
        gameUI.OnAmountEnemy(numberEnemy);
    }

    public void OnPlayerDead()
    {
       DialogManager.instance.ShowDialog(DialogIndex.DefeatDialog);
    }

    public virtual void OnEndWave()
    {
        DialogManager.instance.ShowDialog(DialogIndex.VictoryDialog);
    }
}
