﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuObject;
    public bool paused;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenuObject.SetActive(true);
            paused = true;
            Time.timeScale = 0;
        }
       
    }
    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenuObject.SetActive(false);
        paused = false;
    }
    public void Restart()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Exit()
    {
        Resume();
        SceneManager.LoadScene("MainMenu");
    }
}
