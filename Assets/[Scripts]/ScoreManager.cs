using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreLable;
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        scoreLable = GameObject.Find("ScoreLabel").GetComponent<TMP_Text>();
        SetScore(score);
    }

    public int GetScore()
    {
        return score;
    }

    public void AddPoints(int points)
    {
        score += points;
        UpdateScore();
    }

    public void SetScore(int newScore)
    {
        score = newScore;
        UpdateScore();
    }

    public void UpdateScore()
    {
        scoreLable.text = $"Score: {score}";
    }


    void Update()
    {
        
    }
}
