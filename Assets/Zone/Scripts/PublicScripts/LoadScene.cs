using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
   
    }


    public string sn;
    [ContextMenu("µ÷")]
    private void OnColl()
    {
        LoadSceneByName("s", sn);
    }

    public static void LoadSceneByName(string n, string sceneName)
    {
        Debug.Log(n);
     UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
