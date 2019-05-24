using VRTour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTour.Serialize;

public class MarkerCreator : MonoBehaviour
{
    public List<Widget> listOfNodes;

    [SerializeField]
    private GameObject marker;
    [SerializeField]
    private GameObject node;
    [SerializeField]
    private GameObject originObj;
    [SerializeField]
    private GameObject nodePaneObj;

    private Transform origin;
    private RectTransform back;

    private float offset = 0;

    // Use this for initialization
    void Start()
    {
        listOfNodes = new List<Widget>();
        back = nodePaneObj.GetComponent<RectTransform>();
        origin = originObj.transform;
    }

    public void CreateObject(GameObject controller)
    {
        if(offset >= back.sizeDelta.y)
        {
            back.sizeDelta = new Vector2(back.sizeDelta.x, back.sizeDelta.y * 2);
        }
        GameObject newMarker = Instantiate(marker, controller.transform.position, controller.transform.rotation);
        newMarker.transform.SetParent(origin);

        GameObject newNode = Instantiate(node, nodePaneObj.transform);
        RectTransform rt = newNode.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(0, -offset);
        offset += rt.sizeDelta.y;

        Widget n = newNode.GetComponent<Widget>();
        n.SetWidgetObj(newMarker);

        listOfNodes.Add(n);
    }

    public void FinalizeWidgets()
    {
        int id = 0;
        foreach(Widget wid in listOfNodes)
        {
            VariableManager.instance.AddWidget(wid.Finalize(id));
            id++;
        }


    }

}
