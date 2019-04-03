using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableManager : MonoBehaviour {
    public static VariableManager instance = null;
    private DBScene toBuild = null;
    private IList<DBWidget> widgets = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this);
    }


    // Use this for initialization
    void Start () {
        toBuild = new DBScene();
        widgets = new List<DBWidget>();
        toBuild.Widgets = widgets;

        // Test code here
        //Test();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetBaseVals(string h, string p, string d)
    {
        toBuild.Host = h;
        toBuild.Port = p;
        toBuild.Db = d;
    }

    public void AddWidget(DBWidget wid)
    {
        if(widgets == null)
        {
            widgets = new List<DBWidget>();
        }
        widgets.Add(wid);
        toBuild.Widgets = widgets;
    }

    /*
    public void SetHostVals(string h)
    {
        toBuild.Host = h;
    }
    public void SetPortVals(string p)
    {
        toBuild.Port = p;
    }
    public void SetDBVals(string d)
    {
        toBuild.Db = d;
    }
    */

    public void Test()
    {
        BuildJSONToFile();
    }

    // Scene configuration settings
    private class DBScene
    {
        public string Host;
        public string Port;
        public string Db;
        // List of widgets in the room
        public IList<DBWidget> Widgets;
    }
    // Individual widget configuration settings - needs more robust testing, once we have an instance of multiple queries
    public class DBWidget
    {
        public Vector3 Position;
        public string Measure;

        // Possibly move these to DBScene?
        public string Building;
        public string Room;
    }

    public string BuildJSONToString()
    {
        string json = JsonConvert.SerializeObject(toBuild, Formatting.Indented);
        return json;
    }

    public void BuildJSONToFile()
    {
        // TODO: Improve date time formatting
        string curDate = DateTime.Now.ToOADate().ToString();
        string path = Application.streamingAssetsPath + "/JSON Output/jsonoutput-" + curDate + ".txt";
        System.IO.File.WriteAllText(path, BuildJSONToString());
    }
    /*
    public DBScene FinalizeScene()
    {
        DBScene toReturn = new DBScene(toBuild); 
        return toBuild;
    }
    */
}


