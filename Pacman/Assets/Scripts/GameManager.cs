using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;
    public bool gameStarted = false;
    public bool gamePaused = false;
    public float invincibleTime = 0.0f;

    public AudioClip pauseAudio, startAudio;


    private void Awake()
    {
        sharedInstance = this;
        StartCoroutine("StartGame");
    }

    
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(4.4f);
        gameStarted = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            gamePaused = !gamePaused;
            if (gamePaused)
            { 
                PlayPauseMusic();
                Time.timeScale = 0;
            }
            else
            {
                StopPauseMusic();
                Time.timeScale = 1;
            }
        }

        if (invincibleTime > 0)
        {
            invincibleTime -= Time.deltaTime;
        }
 
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    private void PlayPauseMusic()
    {
        AudioSource source = GetComponent<AudioSource>();
        source.volume = 1f;
        source.clip = pauseAudio;
        source.loop = true;
        source.Play(); 
    }

    private void StopPauseMusic()
    {
        GetComponent<AudioSource>().Stop();

    }

    public void MakeInvincible(float numberOfSeconds)
    {
        this.invincibleTime += numberOfSeconds;
    }



}
