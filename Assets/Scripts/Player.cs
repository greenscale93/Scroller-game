using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] float moveSpeed = 15f;

    //cashe
    private float cameraSize;

	// Use this for initialization
	void Start () {
        cameraSize = Camera.main.orthographicSize;
        Debug.Log(cameraSize);
	}
	
	// Update is called once per frame
	void Update () {
        Move();	
	}

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal")*Time.deltaTime*moveSpeed;
        float newPosX = Mathf.Clamp(transform.position.x + deltaX,-4,4);

        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        float newPosY = transform.position.y + deltaY;


        transform.position = new Vector2(newPosX, newPosY);
    }
}
