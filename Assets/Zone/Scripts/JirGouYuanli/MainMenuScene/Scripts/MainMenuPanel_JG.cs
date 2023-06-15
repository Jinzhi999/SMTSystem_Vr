using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainMenuPanel_JG : MonoBehaviour
{
    public Transform content;
    public VR_ScrollSlider scroll;
     List<LessonMenuItem> lessonList = new List<LessonMenuItem>();

    public VRUIButon back;
    void Start()
    {
        // 清除 课件

        // 生成课件

        lessonList = content.GetComponentsInChildren<LessonMenuItem>().ToList();
        foreach (var item in lessonList)
        {
            item.Init("");
        }



        //int row = lessonList.Count / 3;
        //if (lessonList.Count % 3 > 0)
        //{
        //    row++;
        //}
        int row = content.childCount / 3;
        if (content.childCount % 3 > 0)
        {
            row++;
        }
        scroll.Init(row);

        back.OnClickDn.AddListener(() =>{
            LoadScene.LoadSceneByName("s","MainMenuScene");
        });
    }
}
