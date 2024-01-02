using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    #region Singleton
    public static ScoreManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public Text scoreText;

    int _score = 0;

    private void Update()
    {
        scoreText.text = $"Score: {_score:00}";
    }

    public void AddScore(int increment)
    {
        _score += increment;
    }

    public void RemoveScore(int decrement)
    {
        _score -= decrement;
    }
}
