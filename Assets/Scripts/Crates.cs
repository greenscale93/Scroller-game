using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crates : MonoBehaviour {

    [SerializeField] Vector2 dropVelocity = new Vector2(0,-5f);
    enum CrateType {health, fireRate, projectile};
    [SerializeField] CrateType crateType;
    [SerializeField] float healthAddition;
    [SerializeField] float fireRateIncrease;
    [SerializeField] int projectileAddition;

    Player player;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().velocity = dropVelocity;
        player = FindObjectOfType<Player>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            HandleBoxLooting();
            Destroy(gameObject);
        }
    }

    private void HandleBoxLooting()
    {
        if (crateType == CrateType.health)
        {
            player.IncreaseLife(healthAddition);
        }
        else if (crateType == CrateType.fireRate)
        {
            player.IncreaseFireRate(fireRateIncrease);
        }
        else if (crateType == CrateType.projectile)
        {
            player.AddProjectile(projectileAddition);
        }
    }
}
