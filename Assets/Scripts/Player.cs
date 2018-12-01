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
            firingCoroutine = StartCoroutine(FireContiniously(laserPrefab));
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContiniously(GameObject laserPrefab)
    {      
            while (true)
            {
                var laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laser.GetComponent<Laser>().GetProjectileSpeed());

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

    private void CheckDeath()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            FindObjectOfType<SceneLoader>().ReloadScene();
        }
    }

}
