using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] float moveSpeed = 15f;
    [SerializeField] float movementBorderWidth = 1f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float fireSpeed = 1f;
    [SerializeField] float health = 100;
    [SerializeField] float projectileSpread = 1f;

    [Header("Max improvements")]
    [SerializeField] float maxLife;
    [SerializeField] int maxProjectiles;
    [SerializeField] float minFireRate;

    private int lasersAmount = 1;
    GameObject laser,laser2;

    //cashe
    private float cameraSizeX, cameraSizeY;
    Coroutine firingCoroutine;

	// Use this for initialization
	void Start ()
    {
        SetCameraParameters();
    }

    private void SetCameraParameters()
    {
        float cameraHeight = Camera.main.orthographicSize * 2;
        cameraSizeX = (cameraHeight * Camera.main.aspect) - movementBorderWidth;
        cameraSizeY = cameraHeight - movementBorderWidth;
    }

    // Update is called once per frame
    void Update () {
        Move();
        Fire();
	}

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContiniously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContiniously()
    {
        while (true)
        {
            for (int i = 0; i < lasersAmount; i++)
            {
                float xVelocity = ((-((float)lasersAmount - 1) / 2) + i) * projectileSpread;

                laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity, laser.GetComponent<Laser>().GetProjectileSpeed());
            }
            yield return new WaitForSeconds(fireSpeed);
        }
    }

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal")*Time.deltaTime*moveSpeed;
        float newPosX = Mathf.Clamp(transform.position.x + deltaX, -cameraSizeX/2, cameraSizeX/2);

        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        float newPosY = Mathf.Clamp(transform.position.y + deltaY, -cameraSizeY/2, cameraSizeY/2);


        transform.position = new Vector2(newPosX, newPosY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            DecreaseLife(100);
        }
    }

    public void HandleHit(float damage)
    {
        DecreaseLife(damage);
    }

    private void DecreaseLife(float amount)
    {
        health -= amount;
        CheckDeath();
    }

    public void IncreaseLife(float amount)
    {
        health += amount;
        health = Mathf.Min(health, maxLife);
    }

    public void IncreaseFireRate(float amount)
    {
        fireSpeed -= amount;
        fireSpeed = Mathf.Max(fireSpeed, minFireRate);
    }

    public void AddProjectile(int amount)
    {
        lasersAmount += amount;
        lasersAmount = Mathf.Min(lasersAmount, maxProjectiles);
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            FindObjectOfType<SceneLoader>().LoadLoseScene();
        }
    }

    public float GetHealth()
    {
        return health;
    }

}
