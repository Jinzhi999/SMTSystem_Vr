using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class MenuFunctionPanel : MonoBehaviour
{
    public VRUIButon 退出;
    public VRUIButon 组成讲解, 装备拆卸, 装备装配,菜单;
    public VRUIButon 归位1,归位2, 归位3, 零件库, 模型树,透视,网格化;
    public VRUIButon 拆卸步骤, 拆卸动画, 拆卸动画减速,拆卸动画加速;
    public VRUIButon 装配步骤, 装配动画, 装配动画减速, 装配动画加速;

    public AnimManager animManager;
    public FadeManagerTest effectManager;


    public StepTablePanel stepTablePanel;           //步骤
    public SparePartsTreePanel sparePartsTreePanel; //零件


    // devie

    public DeviceTest deviceManager;
    void Start()
    {
        // 自动赋值
        InitObjects();
        退出.OnClickDn.AddListener(() => {
            LoadScene.LoadSceneByName(退出.name, "MainMenuScene");
        });
        菜单.OnClickDn.AddListener(() => {
            animManager.BackAnimToTarPos();
            deviceManager.InitToInitPro();
        });
        return;
        //组成讲解.OnClickDn.AddListener(()=> {
        //    animManager.BackAnimToInitPos();
        //});
        //装备拆卸.OnClickDn.AddListener(() => {
        //    animManager.BackAnimToInitPos();

        //    deviceManager.InitToTarPro();
        //});
        //装备装配.OnClickDn.AddListener(() => {
        //    animManager.BackAnimToTarPos();
        //    deviceManager.InitToInitPro();
        //});

        // 二级
        归位1.OnClickDn.AddListener(() => {
            animManager.BackAnimToInitPos();
            deviceManager.HideAllHightLight();
        });
        归位2.OnClickDn.AddListener(() => {
            animManager.BackAnimToInitPos();
            deviceManager.HideAllHightLight();
        });
        归位3.OnClickDn.AddListener(() => {
            animManager.BackAnimToInitPos();
            deviceManager.HideAllHightLight();
        });

        零件库.OnClickDn.AddListener(() => {
            if (stepTablePanel.gameObject.activeInHierarchy)
            {
                stepTablePanel.gameObject.SetActive(false);
            }    
            sparePartsTreePanel.ShowPanel();
        });
        模型树.OnClickDn.AddListener(() => {

        });
        透视.OnClickDn.AddListener(() => {
            Text t = 透视.GetComponentInChildren<Text>();
            if (t.text == "透视")
            {
                effectManager.SetFadeMaterial();
                t.text = "实体";
                if (网格化.GetComponentInChildren<Text>().text == "实体")
                {
                    网格化.GetComponentInChildren<Text>().text = "网格化";
                }
            }
            else
            {
                t.text = "透视";
                effectManager.SetInitMaterial();
            }
        });
        网格化.OnClickDn.AddListener(() => {
            Text t = 网格化.GetComponentInChildren<Text>();
            if (t.text == "网格化")
            {
                effectManager.SetGridMaterial();
                t.text = "实体";

                if (透视.GetComponentInChildren<Text>().text == "实体")
                {
                    透视.GetComponentInChildren<Text>().text = "透视";
                }
            }
            else {
                t.text = "网格化";
                effectManager.SetInitMaterial();
            }
        });

        // 拆解
        拆卸步骤.OnClickDn.AddListener(() => {
            if (sparePartsTreePanel.gameObject.activeInHierarchy)
            {
                sparePartsTreePanel.gameObject.SetActive(false);
            }
            stepTablePanel.SetInitPos();
        });
        拆卸动画.OnClickDn.AddListener(() => {
            animManager.BackAnimToInitPos();
            animManager.StartAnim();
        });
        拆卸动画减速.OnClickDn.AddListener(() => {
            animManager.AnimSpeedDown();
        });
        拆卸动画加速.OnClickDn.AddListener(() => {
            animManager.AnimSpeedUp();
        });

        // 装配
        装配步骤.OnClickDn.AddListener(() => {
            if (sparePartsTreePanel.gameObject.activeInHierarchy)
            {
                sparePartsTreePanel.gameObject.SetActive(false);
            }
            stepTablePanel.SetInitPos();
        });
        装配动画.OnClickDn.AddListener(() => {
            animManager.BackAnimToTarPos();
            animManager.EndAnim();
        });
        装配动画减速.OnClickDn.AddListener(() => {
            animManager.AnimSpeedDown();
        });
        装配动画加速.OnClickDn.AddListener(() => {
            animManager.AnimSpeedUp();
        });
    }

    void InitObjects()
    {
        // 获取所有 vrui类型
        Type type = typeof(MenuFunctionPanel);
        var filedls = type.GetFields();
        List<FieldInfo> fieldList = new List<FieldInfo>();
        foreach (var item in filedls)
        {
            if (item.FieldType.Equals(typeof(VRUIButon)))
            {
                fieldList.Add(item);
            }
        }
        // 遍历子物体  获取组件
        List<VRUIButon> btnList = transform.GetComponentsInChildren<VRUIButon>(true).ToList();
        foreach (var field in fieldList)
        {
            foreach (var btn in btnList)
            {
                if (btn.name.Equals(field.Name))
                {
                    field.SetValue(this, btn);
                    Debug.Log(btn.name);
                    continue;
                }
            }
        }
    }
}
