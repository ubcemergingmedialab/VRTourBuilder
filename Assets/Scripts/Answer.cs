using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTour.Serialize;

public class Answer : MonoBehaviour {

    [SerializeField]
    private InputField label;
    [SerializeField]
    private InputField id;

    public Destination Finalize()
    {
        return new Destination
        {
            label = label.text,
            dest = int.Parse(id.text)
    };
    } 
}
