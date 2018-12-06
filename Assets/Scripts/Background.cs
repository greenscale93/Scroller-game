using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

    [SerializeField] float scrollingSpeed = 0.5f;
    Material material;
    private void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update () {
        material.mainTextureOffset += new Vector2(0f, scrollingSpeed)*Time.deltaTime;
	}
}
