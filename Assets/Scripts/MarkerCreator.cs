using ARDesign.Serialize;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerCreator : MonoBehaviour
{
    public List<Widget> listOfWidgets;

    [SerializeField]
    private GameObject marker;
    [SerializeField]
    private GameObject widget;
    [SerializeField]
    private GameObject originObj;
    [SerializeField]
    private GameObject widgetPaneObj;

    private Transform origin;

    [SerializeField]
    private float initY = 100.0f;
    [SerializeField]
    private float deltaY = -80.0f;

    // Use this for initialization
    void Start()
    {
        listOfWidgets = new List<Widget>();
        origin = originObj.transform;
    }

    public void CreateObject(GameObject controller)
    {
        GameObject newMarker = Instantiate(marker, controller.transform.position, new Quaternion(0f,0f,0f,0f));
        newMarker.transform.SetParent(origin);

        GameObject newWidget = Instantiate(widget, widgetPaneObj.transform);
        Vector3 position = newWidget.transform.localPosition;
        position.y = initY;
        initY = initY + deltaY;
        newWidget.transform.localPosition = position;

        Widget wid = newWidget.GetComponent<Widget>();
        wid.SetWidgetObj(newMarker);

        listOfWidgets.Add(wid);
    }

    public void FinalizeWidgets()
    {
        foreach(Widget wid in listOfWidgets)
        {
            VariableManager.instance.AddWidget(wid.Finalize());
        }


    }

}
