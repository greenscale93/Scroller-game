using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave config")]
public class WaveConfig : ScriptableObject {

    [SerializeField] int enemiesCount;
    [SerializeField] float spawnDelay;
    [SerializeField] float enemySpeed;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;

    public int GetEnemiesCount(){return enemiesCount;}
    public float GetSpawnDelay() { return spawnDelay; }
    public float GetEnemySpeed() { return enemySpeed; }
    public GameObject GetEnemyPrefab() { return enemyPrefab; }
    public GameObject GetPathPrefab() { return pathPrefab; }

}
