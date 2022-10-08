using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour
{
    public bool isHighScore;

    float highScore;
    Text uiText;

    // Start is called before the first frame update
    void Start()
    {
        uiText = GetComponent<Text>();
        Debug.Log("������?");
        if(isHighScore)
        {
           
            highScore = PlayerPrefs.GetFloat("Score");
            uiText.text = highScore.ToString("F");
            Debug.Log(highScore);
        }
    }

    private void LateUpdate()
    { 
        if (isHighScore && GameManager.score < highScore)
            return;

        uiText.text = GameManager.score.ToString("F");
    }
}
