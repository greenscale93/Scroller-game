using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour {

    [SerializeField] float projectileSpeed;
    [SerializeField] float damage;

	public float GetProjectileSpeed()
    {
        return projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().HandleHit(damage);
            Destroy(gameObject);
        }
    }

}
