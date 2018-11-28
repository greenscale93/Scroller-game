using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    [SerializeField] List<WaveConfig> waveConfigs;

	void Start () {
	
        foreach(WaveConfig wave in waveConfigs)
        {
            StartCoroutine(SpawnWave(wave));
        }

	}

    IEnumerator SpawnWave(WaveConfig wave)
    {
        for (int i = 0; i < wave.GetEnemiesCount(); i++)
        {
            var enemy = Instantiate(wave.GetEnemyPrefab());
            enemy.GetComponent<Enemy>().SetWaveConfig(wave);

            yield return new WaitForSeconds(wave.GetSpawnDelay());
        }
    }

}
