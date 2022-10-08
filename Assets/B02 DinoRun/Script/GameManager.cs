using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public const float ORIGIN_SPEED = 3;
    
    public static float globalRate;
    public static float score;
    public static bool isLive;
    public static GameObject uiOver;

    void Awake()
    {
        isLive = true;

     
        if (!PlayerPrefs.HasKey("Score"))
            PlayerPrefs.SetFloat("Score", 0);

    }
    // Update is called once per frame
    void Update()
    {
        if(isLive)
        {
            score += Time.deltaTime;
            globalRate = ORIGIN_SPEED + score * 0.1f;
        }


        if(!isLive && Input.GetButtonDown("Jump"))
        {
            Restart();
        }
    }

    public static void GameOver()
    {
        //uiOver.SetActive(true);
        isLive = false;
        float highScore = PlayerPrefs.GetFloat("Score");
        PlayerPrefs.SetFloat("Score",Mathf.Max(highScore , score));
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        score = 0f;
        isLive = true;
    }
}
