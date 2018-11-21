using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] float moveSpeed = 15f;
    [SerializeField] float movementBorderWidth = 1f;

    //cashe
    private float cameraSizeX, cameraSizeY;

	// Use this for initialization
	void Start () {
        float cameraHeight = Camera.main.orthographicSize * 2;
        cameraSizeX = (cameraHeight * Camera.main.aspect) - movementBorderWidth;
        cameraSizeY = cameraHeight - movementBorderWidth;
	}
	
	// Update is called once per frame
	void Update () {
        Move();	
	}

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal")*Time.deltaTime*moveSpeed;
        float newPosX = Mathf.Clamp(transform.position.x + deltaX, -cameraSizeX/2, cameraSizeX/2);

        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        float newPosY = Mathf.Clamp(transform.position.y + deltaY, -cameraSizeY/2, cameraSizeY/2);


        transform.position = new Vector2(newPosX, newPosY);
    }
}
