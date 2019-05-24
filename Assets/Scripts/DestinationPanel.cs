using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTour.Serialize;

public class DestinationPanel : MonoBehaviour {

    [SerializeField]
    private Text id;
    [SerializeField]
    private Text question;
    [SerializeField]
    private RectTransform answerPanel;
    [SerializeField]
    private GameObject answerPrefab;

    public List<Answer> answers;

    #region PRIVATE_MEMBER_VARIABLES
    private float offset;
    private Node activeNode;
    #endregion //PRIVATE_MEMBER_VARIABLES

    private void Start()
    {
        offset = 0;
        answers = new List<Answer>();
    }

    public void Setup(Node n)
    {
        activeNode = n;
        id.text = n.nodeId.ToString();
        question.text = n.label;
    }

    public void AddAnswer()
    {
        if (offset >= answerPanel.sizeDelta.y)
        {
            answerPanel.sizeDelta = new Vector2(answerPanel.sizeDelta.x, answerPanel.sizeDelta.y * 2);
        }
        GameObject answer = Instantiate(answerPrefab, answerPanel);
        answers.Add(answer.GetComponent<Answer>());
        RectTransform answerTransform = answer.GetComponent<RectTransform>();
        answerTransform.anchoredPosition = new Vector2(0, -offset);
        offset += answerTransform.sizeDelta.y;

    }

    public Node Finalize()
    {
        activeNode.answers = new Destination[answers.Count];
        for(int i = 0; i <answers.Count; i++)
        {
            activeNode.answers[i] = answers[i].Finalize();
        }
        return activeNode;
    }


}
