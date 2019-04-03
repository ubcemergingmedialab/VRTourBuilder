using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    static SceneController instance = null;
    [SerializeField]
    private GameObject loadScreen;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        loadScreen.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void IncrementScene()
    {
        int curIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = curIndex+1;
        SceneManager.LoadSceneAsync(nextIndex);
        loadScreen.SetActive(true);
        //Debug.Log(VariableManager.instance.BuildJSONToString());
    }

    public void DecrementScene()
    {
        int curIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = curIndex - 1;
        SceneManager.LoadSceneAsync(nextIndex);
        loadScreen.SetActive(true);
    }

}
