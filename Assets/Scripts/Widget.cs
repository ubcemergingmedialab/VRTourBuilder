using ARDesign.Serialize;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Widget : MonoBehaviour {
    [SerializeField]
    private InputField xText;
    [SerializeField]
    private InputField yText;
    [SerializeField]
    private InputField zText;

    private GameObject widObj;


    private string vals;
    private string measure;
    private Vector3 position;
    private string x;
    private string y;
    private string z;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if(widObj != null && position != widObj.transform.localPosition)
        {
            position = widObj.transform.localPosition;
            xText.text = position.x.ToString();
            yText.text = position.y.ToString();
            zText.text = position.z.ToString();
        }
	}

    public void SetWidgetObj(GameObject wid)
    {
        widObj = wid;
        position = wid.transform.localPosition;
        xText.text = position.x.ToString();
        yText.text = position.y.ToString();
        zText.text = position.z.ToString();
    }

    //UnityEvent code
    public void UpdateMeasure(string m)
    {
        measure = m;
    }
    public void UpdateValues(string v)
    {
        vals = v;
    }
    public void UpdateX(string x0)
    {
        x = x0;
        float.TryParse(x, out position.x);
        widObj.transform.localPosition = position;
    }
    public void UpdateY(string y0)
    {
        y = y0;
        float.TryParse(y, out position.y);
        widObj.transform.localPosition = position;
    }
    public void UpdateZ(string z0)
    {
        z = z0;
        float.TryParse(z, out position.z);
        widObj.transform.localPosition = position;
    }



    public DBWidget Finalize()
    {
        return new DBWidget
        {
            Measure = measure,
            Position = position
            //TODO add code seperating vals on comma
        };
    }
}
