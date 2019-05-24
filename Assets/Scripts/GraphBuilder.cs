using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTour.Serialize;

public class GraphBuilder : MonoBehaviour {

    [SerializeField]
    private Text numNodes;
    [SerializeField]
    private RectTransform contentPanel;
    [SerializeField]
    private GameObject destPrefab;

    private RectTransform back;
    private IList<DestinationPanel> nodes;
    private float offset;

    // Use this for initialization
    void Start () {
        back = contentPanel.GetComponent<RectTransform>();
        nodes = new List<DestinationPanel>();
        numNodes.text = VariableManager.instance.GetNumNodes().ToString();
        offset = 0;

        foreach (Node n in VariableManager.instance.GetNodes())
        {
            AddNode(n);
        }
	}
	
	public void Submit() {
        List<Node> toBuild = new List<Node>();
        foreach(DestinationPanel dp in nodes)
        {
            toBuild.Add(dp.Finalize());
        }
        VariableManager.instance.SetNodes(toBuild.ToArray());
	}

    private void AddNode(Node n)
    {
        if (offset >= back.sizeDelta.y)
        {
            back.sizeDelta = new Vector2(back.sizeDelta.x, back.sizeDelta.y * 2);
        }
        GameObject dest = Instantiate(destPrefab, contentPanel);
        DestinationPanel nodePanel = dest.GetComponent<DestinationPanel>();
        nodePanel.Setup(n);
        nodes.Add(nodePanel);
        RectTransform destTransform = dest.GetComponent<RectTransform>();
        destTransform.anchoredPosition = new Vector2(0, -offset);
        offset += destTransform.sizeDelta.y;

    }

    
}
