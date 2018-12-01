using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    [SerializeField] Sprite[] healthBarSprites;

    GameObject baseObject;
    Enemy enemy;
    SpriteRenderer spriteComponent;

    private void Start()
    {
        spriteComponent = GetComponent<SpriteRenderer>();
    }

    private float healthBarOffset;

	public void SetBaseObject(GameObject baseObject)
    {
        this.baseObject = baseObject;
        healthBarOffset = baseObject.GetComponent<Enemy>().GetHealthBarOffset();
        enemy = baseObject.GetComponent<Enemy>();
    }

    public void DestroyHealthBar()
    {
        Destroy(gameObject);
    }

    public void ChangeHealthBarView()
    {
        float healthRate = enemy.GetHealth() / enemy.GetMaxHealth();
        SetSprite(healthRate);
    }

    private void SetSprite(float healthRate)
    {
        if (healthRate <= 0.25f)
        {
            spriteComponent.sprite = healthBarSprites[3];
        }
        else if (healthRate <= 0.5f)
        {
            spriteComponent.sprite = healthBarSprites[2];
        }
        else if (healthRate <= 0.75f)
        {
            spriteComponent.sprite = healthBarSprites[1];
        }
        else
        {
            spriteComponent.sprite = healthBarSprites[0];
        }
    }

    // Update is called once per frame
    void Update () {
        transform.position = new Vector2(baseObject.transform.position.x, 
            baseObject.transform.position.y + healthBarOffset);
	}
}
