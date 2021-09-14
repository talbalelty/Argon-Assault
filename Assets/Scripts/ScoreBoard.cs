using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    int score;
    TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "0";
    }

    // enemies make a call to update score when they die
    public void UpdateScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }
}
