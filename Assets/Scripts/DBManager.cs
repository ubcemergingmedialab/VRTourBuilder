using VRTour.Serialize;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DBManager : MonoBehaviour {
    public static DBManager instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    [SerializeField]
    private Text nameLabel;

    public void OnConfirm () {
        VariableManager.instance.SetBaseVals(nameLabel.text);
	}
}
