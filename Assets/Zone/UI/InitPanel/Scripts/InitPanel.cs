using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LGZN.Unity;
using UnityEngine.UI;

public class InitPanel : UIBase
{
    public Button 开始训练, 设置, 退出, 训练确认,设置确认, PL1, PL2;
    public Transform 训练界面, 设置界面;
    public Color color;
    string currentSubject="";
    void Start()
    {
        InitObject();
        开始训练.onClick.AddListener(() => {
            开始训练.GetComponent<Image>().color = color;
            设置.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            退出.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            训练界面.gameObject.SetActive(true);
            设置界面.gameObject.SetActive(false);
            Debug.Log("开始");

        });
   
        设置.onClick.AddListener(() => {
            开始训练.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            设置.GetComponent<Image>().color = color;
            退出.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            训练界面.gameObject.SetActive(false);
            设置界面.gameObject.SetActive(true);


        });
        退出.onClick.AddListener(() => {
            开始训练.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            设置.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            退出.GetComponent<Image>().color = color;
            训练界面.gameObject.SetActive(false);
            设置界面.gameObject.SetActive(false);

            Application.Quit();

        });

        PL1.onClick.AddListener(() => {
            PL1.GetComponent<Image>().color = color;
            PL2.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            currentSubject = "PL1";
        });
        PL2.onClick.AddListener(() => {
            PL1.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            PL2.GetComponent<Image>().color = color;
            currentSubject = "PL2";
        });

        训练确认.onClick.AddListener(() => {
            if (!string.IsNullOrEmpty( currentSubject))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
            }
            else
            {
                
            }
        });
        设置确认.onClick.AddListener(() => {
            设置.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            设置界面.gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
