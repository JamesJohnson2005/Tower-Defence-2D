using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    float timer;
    private bool run;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    { 
        if (run == true)
        { 
            timer += Time.deltaTime;
        }

    }
    public void Win()
    {
        run = false;

    }
}
