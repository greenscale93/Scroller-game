using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour {

    GameStatus gameStatus;
    TextMeshProUGUI TMPro;

    // Use this for initialization
    void Start () {
        TMPro = GetComponent<TextMeshProUGUI>();
        gameStatus = FindObjectOfType<GameStatus>();
        TMPro.text = "SCORE: " + gameStatus.GetScore().ToString();
    }

}
