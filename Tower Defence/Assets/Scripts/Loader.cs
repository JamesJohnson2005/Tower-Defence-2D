using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;


public class Loader : MonoBehaviour
{
    XDocument xmlDoc;
    bool finishedLoading = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartCoroutine("Assign Data");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (finishedLoading)
        {
            Application.LoadLevel("TestScene");

        }
    }
    void LoadXML()
    {
        xmlDoc = XDocument.Load("Assets/Resources/XML/HighScore");
        // Highscore = xmlDoc.Descendants("HighScore").Elements();
        foreach (var highscore in HighScore)
        {

        }
    }
}
