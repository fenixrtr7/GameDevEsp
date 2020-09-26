using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmberKO.Manager;
using System.IO;

public class ReadJson : MonoBehaviour
{
    string sPath = "Assets/GameDEVJAM/JsonSong/csvjson.json";
    // Start is called before the first frame update
    void Start()
    {
        DataArrowList listArrow = new DataArrowList();
        DataArrow one = new DataArrow("1", "r", "0");
        DataArrow two = new DataArrow("1", "r", "1");

        listArrow.listData.Add(one);
        listArrow.listData.Add(two);

        string dataAsJson = JsonUtility.ToJson(listArrow);
        //Debug.Log(dataAsJson);
        SerializeManager.Save("Assets/GameDEVJAM/JsonSong/Newcsvjson.json", dataAsJson);

        Read(sPath);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Read(string path)
    {
        if (!File.Exists(path))
            return;

        string json = SerializeManager.Load(path);
        DataArrowList data = JsonUtility.FromJson<DataArrowList>(json);

        if (data == null)
            return;

        foreach (DataArrow d in data.listData)
        {
            Debug.Log("Time Stamp " + d.timestamp);
        }
    }
}

[System.Serializable]
public class DataArrow
{
    public string timestamp;
    public string direction;
    public string special;
    public DataArrow(string _timeStamp, string _direction, string _special)
    {
        this.timestamp = _timeStamp;
        this.direction = _direction;
        this.special = _special;
    }
}

[System.Serializable]
public class DataArrowList
{
    public List<DataArrow> listData;

    public DataArrowList()
    {
        listData = new List<DataArrow>();
    }
}
