using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] bool isRepeating = true;
    float amplificationRate = 1f;

	IEnumerator Start () {
        do
        {
            foreach (WaveConfig wave in waveConfigs)
            {
                yield return StartCoroutine(SpawnWave(wave));
                amplificationRate *= 1.03f;
            }
        }
        while (isRepeating);
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

    public float GetAmplificationRate()
    {
        return amplificationRate;
    }

}
