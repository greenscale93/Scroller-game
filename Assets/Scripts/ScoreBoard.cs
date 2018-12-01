using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour {

    GameStatus gameStatus;
    TextMeshProUGUI TMPro;

	// Use this for initialization
	void Start () {
        TMPro = GetComponent<TextMeshProUGUI>();
        gameStatus = FindObjectOfType<GameStatus>();
	}
	
	// Update is called once per frame
	void Update () {
        TMPro.text = gameStatus.GetScore().ToString();
	}
}
