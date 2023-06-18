using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LGZN.Unity;
using UnityEngine.UI;

public class MainMenuPanel : UIBase
{
    public Button 结构原理, 操作使用, 检测维修, 维护保养;

    string currentSubject="";
    void Start()
    {
        InitObject();

        结构原理.onClick.AddListener(() => {
            UnityEngine.SceneManagement.SceneManager.LoadScene("01结构原理");
        });
        操作使用.onClick.AddListener(() => {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Lesson_课程00");
        });
        检测维修.onClick.AddListener(() => {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Lesson_课程00");
        });
        维护保养.onClick.AddListener(() => {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Lesson_课程00");
        });
    }
}
