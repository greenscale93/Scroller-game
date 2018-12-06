using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthBoard : MonoBehaviour {

    TextMeshProUGUI TMPro;
    Player player;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<Player>();
        TMPro = GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update () {
        TMPro.text = player.GetHealth().ToString();
	}
}
