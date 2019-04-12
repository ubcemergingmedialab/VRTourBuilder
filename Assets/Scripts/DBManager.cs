using ARDesign.Serialize;
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
    private Text port;
    [SerializeField]
    private Text host;
    [SerializeField]
    private Text db;
    [SerializeField]
    private Text building;
    [SerializeField]
    private Text room;

    public void OnConfirm () {
        VariableManager.instance.SetBaseVals(host.text, port.text, db.text, building.text, room.text);
	}
}
