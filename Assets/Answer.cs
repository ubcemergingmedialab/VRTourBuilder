using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer : MonoBehaviour {

    [SerializeField]
    private InputField label;
    [SerializeField]
    private InputField id;

    public int GetID()
    {
        return int.Parse(id.text);
    }
    public string GetLabel()
    {
        return label.text;
    }
}
