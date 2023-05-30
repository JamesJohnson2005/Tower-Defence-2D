using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameObject gameManager;

    private void Awake()
    {
        // Keep the GameManager between scenes
        DontDestroyOnLoad(gameObject);
        
        // Set a gamemanager if it doesnt already exist
        if (gameManager == null)
        {
            gameManager = gameObject;
        } else
        {
            Destroy(gameObject);
        }

    }
}
