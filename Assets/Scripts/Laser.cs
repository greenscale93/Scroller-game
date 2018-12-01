using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float damage = 100;

    public float GetProjectileSpeed()
    {
        return projectileSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().HandleHit(damage);
        }
        Destroy(gameObject);
    }

}
