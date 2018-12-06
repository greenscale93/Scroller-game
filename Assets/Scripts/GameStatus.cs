using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour {

    [SerializeField] float score = 0;

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
        if(FindObjectsOfType<GameStatus>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddScore(float score)
    {
        this.score += score;
    }

    public float GetScore()
    {
        return score;
    }

    private void Start()
    {
        score = 0;
    }

}
