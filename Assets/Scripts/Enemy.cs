using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] float health = 500;
    [SerializeField] float score = 200;
    [SerializeField] float healthBarOffset = 20f;
    [SerializeField] float shotDelay = 5f;
    [SerializeField] float shotRandomFactor = 3f;
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] GameObject deathParticlePrefab;
    
    [SerializeField] GameObject healthBarPrefab;

    [Header("Crates")]
    [SerializeField] float crateChance = 20f;
    [SerializeField] GameObject lifeCrate;
    [SerializeField] GameObject weaponCrate;
    [SerializeField] GameObject fireRateCrate; 

    Coroutine shootingCoroutine;
    private float maxHealth;
    private float waveAmplification;
    
    GameObject healthBar;
    GameStatus gameStatus;

    WaveConfig waveConfig;
    float moveVelocity = 10f;

    List<Transform> waypoints = new List<Transform>();
    int waypointIndex = 0;
    Vector2 targetPosition;

    public float GetHealthBarOffset()
    {
        return healthBarOffset;
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void HandleHit(float damage)
    {
        health -= damage;
        healthBar.GetComponent<HealthBar>().ChangeHealthBarView();
        CheckDeath();
    }

    private void CheckDeath()
    {
        if(health<=0)
        {
            DestroyEnemy(true);
        }
    }

    private void DestroyEnemy(bool isKilled)
    {
        if (isKilled)
        {
            var explosion = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 1f);

            //drop crate
            float containCrate = Random.Range(0, 100);
            if(containCrate <= crateChance)
            {
                int crateType = Random.Range(1, 4);
                if (crateType == 1)
                {
                    //health
                    Instantiate(lifeCrate, transform.position, transform.rotation);
                }
                else if (crateType == 2)
                {
                    //weapon
                    Instantiate(weaponCrate, transform.position, transform.rotation);
                }
                else if (crateType == 3)
                {
                    //fireRate
                    Instantiate(fireRateCrate, transform.position, transform.rotation);
                }
            }            

            gameStatus.AddScore(score);
        }
        StopCoroutine(shootingCoroutine);
        Destroy(healthBar);
        Destroy(gameObject);
    }

	void Start ()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        waveAmplification = FindObjectOfType<WaveSpawner>().GetAmplificationRate();

        InitializeEnemyParameters();
        InitializeMovement();
        InitializeHealthBar();

        shootingCoroutine = StartCoroutine(Shoot());
    }

    private void InitializeEnemyParameters()
    {
        health = health * waveAmplification;
        maxHealth = health;
        shotDelay -= shotDelay * (waveAmplification - 1);
        shotRandomFactor -= shotRandomFactor * (waveAmplification - 1);
    }

    private void InitializeHealthBar()
    {
        healthBar = Instantiate(healthBarPrefab);
        healthBar.GetComponent<HealthBar>().SetBaseObject(gameObject);
    }

    private void InitializeMovement()
    {
        foreach (Transform waypoint in waveConfig.GetPathPrefab().transform)
        {
            waypoints.Add(waypoint);
        }
        transform.position = waypoints[waypointIndex].position;

        targetPosition = waypoints[waypointIndex + 1].position;
        moveVelocity = waveConfig.GetEnemySpeed() * waveAmplification;
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
	
	void Update () {
        Move();	
	}

    private void Move()
    {
        if(waypointIndex < waypoints.Count)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveVelocity * Time.deltaTime);
            
            if((Vector2)transform.position == targetPosition)
            {
                waypointIndex++;
                if (waypointIndex < waypoints.Count)
                {
                    targetPosition = waypoints[waypointIndex].position;
                }                                
            }
        }
        else
        {
            DestroyEnemy(false);
        }
        
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            ShootProjectile();
            yield return new WaitForSeconds(shotDelay + Random.Range(-shotRandomFactor, shotRandomFactor));
        }
    }

    private void ShootProjectile()
    {
        var projectile = Instantiate(weaponPrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectile.GetComponent<EnemyWeapon>().GetProjectileSpeed());
    }
}
