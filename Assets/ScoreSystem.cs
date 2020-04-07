using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public float score = 0f;
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText.text = score.ToString();
    }

    public void AddPoints (float pointAmount)
    {
        score += pointAmount;//somewhere to add point by killing enemy
        scoreText.text = score.ToString();
    }
}
