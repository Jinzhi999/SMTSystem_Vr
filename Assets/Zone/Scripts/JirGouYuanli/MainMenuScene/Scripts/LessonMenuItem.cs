using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LessonMenuItem : MonoBehaviour
{
    private Text t;
    public void Init(string name)
    {
        t = transform.Find("t/Text").GetComponent<Text>();
        transform.GetComponent<VRUIButon>().OnClickDn.AddListener(() => {
            LoadScene.LoadSceneByName( transform.name,"Lesson_"+t.text);
        });
    }
}
