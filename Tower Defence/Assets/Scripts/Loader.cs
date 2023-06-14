using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;


public class Loader : MonoBehaviour
{
    [SerializeField] private HighScoreData _HighScoreData = new HighScoreData();

    public void SaveIntoJSON()
    {
        string id = JsonUtility.ToJson(_HighScoreData);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/HighScore.json", id);

    }

    // Start is called before the first frame update
    [System.Serializable]

    public class HighScoreData
    {
        public int levelID;
        public float time;
    }
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
     
    }
    void LoadXML()
    {

    }
}
