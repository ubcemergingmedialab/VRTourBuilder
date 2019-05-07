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

    [SerializeField]
    private float initY = 100.0f;
    [SerializeField]
    private float deltaY = -80.0f;

    // Use this for initialization
    void Start()
    {
        listOfNodes = new List<Widget>();
        origin = originObj.transform;
    }

    public void CreateObject(GameObject controller)
    {
        GameObject newMarker = Instantiate(marker, controller.transform.position, controller.transform.rotation);
        newMarker.transform.SetParent(origin);

        GameObject newNode = Instantiate(node, nodePaneObj.transform);
        Vector3 position = newNode.transform.localPosition;
        position.y = initY;
        initY = initY + deltaY;
        newNode.transform.localPosition = position;

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
