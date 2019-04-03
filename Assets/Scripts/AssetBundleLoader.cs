
using SFB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssetBundleLoader : MonoBehaviour {

    [SerializeField]
    private InputField pathField;

    [SerializeField]
    private GameObject success;
    [SerializeField]
    private GameObject badPath;
    [SerializeField]
    private GameObject fail;
    [SerializeField]
    private GameObject load;
    [SerializeField]
    private Text loadText;
    [SerializeField]
    private GameObject loadA;

    [HideInInspector]
    public string path;

	// Update is called once per frame
	public void ChoosePath () {
        var path_ = StandaloneFileBrowser.OpenFilePanel("Open File", "", "", false);
        if(path_.Length!= 0)
        {
            path = path_[0];
            pathField.text = path;
        }
    }

    public void LoadBundle()
    {
        success.SetActive(false);
        fail.SetActive(false);

        if(path.Length == 0)
        {
            badPath.SetActive(true);
            return;
        }
        else
        {
            badPath.SetActive(false);
        }

        StartCoroutine(LoadBundleAsync());
        
    }

    IEnumerator LoadBundleAsync()
    {
        load.SetActive(true);
        var bundleLoadRequest = AssetBundle.LoadFromFileAsync(path);

        yield return null;
        //TODO: Get this to actually show loading 
        while (!bundleLoadRequest.isDone)
        {
            loadText.text = "Loading progress: " + (bundleLoadRequest.progress * 100) + "%";
            yield return null;
        }
        

        var myLoadedAssetBundle = bundleLoadRequest.assetBundle;
        if (myLoadedAssetBundle == null)
        {
            load.SetActive(false);
            fail.SetActive(true);
            yield break;
        }
        load.SetActive(false);
        loadA.SetActive(true);
        var assetLoadRequest = myLoadedAssetBundle.LoadAllAssetsAsync();
        yield return assetLoadRequest;
        loadA.SetActive(false);

        if(assetLoadRequest.allAssets.Length == 0)
        {
            fail.SetActive(true);
            yield break;
        }
        GameObject prefab = assetLoadRequest.asset as GameObject;
        EnvironManager.instance.environ = prefab;
        success.SetActive(true);

        myLoadedAssetBundle.Unload(false);
    }
}
