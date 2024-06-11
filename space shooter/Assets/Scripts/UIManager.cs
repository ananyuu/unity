using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Image lifeImage;
    [SerializeField] private Sprite[] lifeScore;
    void Start()
    {
        scoreText.text = "Score: " + 0;
    }

    public void UpdateScore(int playerscore)
    {
        scoreText.text = "Score: " + playerscore.ToString();
    }

    public void UpdateLifeScore(int currentLife)
    {
        lifeImage.sprite = lifeScore[currentLife];
    }
}
