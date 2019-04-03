using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OriginManager : MonoBehaviour {

    //public static OriginManager instance;

    [SerializeField]
    private GameObject originControllers;
    [SerializeField]
    private GameObject widgetControllers;
    [SerializeField]
    private GameObject originObj;

    [SerializeField]
    private InputField xText;
    [SerializeField]
    private InputField yText;
    [SerializeField]
    private InputField zText;

    private Vector3 position;
    private string x;
    private string y;
    private string z;

    private bool isSet = false;

    private List<Transform> children = new List<Transform>();

    // Use this for initialization
    void Awake () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (!isSet)
        {
            position = originObj.transform.position;
            xText.text = position.x.ToString();
            x = xText.text;
            yText.text = position.y.ToString();
            y = yText.text;
            zText.text = position.z.ToString();
            z = zText.text;
        }
	}

    public void MarkOrigin(GameObject controller)
    {
        originObj.transform.position = controller.transform.position;
    }

    public void OnConfirm()
    {
        isSet = true;
        originControllers.SetActive(false);
        widgetControllers.SetActive(true);
        if(children.Count != 0)
        {
            foreach(Transform child in children)
            {
                child.SetParent(originObj.transform);
            }
            children.Clear();
        }
    }

    public void OnReposition()
    {
        isSet = false;
        foreach(Transform child in originObj.transform)
        {
            children.Add(child);
        }
        originObj.transform.DetachChildren();
        originControllers.SetActive(true);
        widgetControllers.SetActive(false);
    }

    public void UpdateX(string x0)
    {
        x = x0;
        float.TryParse(x, out position.x);
        originObj.transform.localPosition = position;
    }
    public void UpdateY(string y0)
    {
        y = y0;
        float.TryParse(y, out position.y);
        originObj.transform.localPosition = position;
    }
    public void UpdateZ(string z0)
    {
        z = z0;
        float.TryParse(z, out position.z);
        originObj.transform.localPosition = position;
    }

}
