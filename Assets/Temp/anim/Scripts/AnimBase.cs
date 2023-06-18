using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Valve.VR.InteractionSystem;
//using UnityEngine.UI;

public class AnimBase : MonoBehaviour
{
    public int sort=0;

    private Transform introPanel;

    public Vector3 initPos,initRot;
    public Vector3 tarPos,tarRot;
    public float flyTime = 1;
    private float initflyTime = 1;

    private Transform pare;// 父物体
    BoxCollider iner;

    void Start()
    {
        initPos = transform.position;
        initRot = transform.localEulerAngles;
        initflyTime = flyTime;
        introPanel = transform.Find("introPanel");
        //introPanel.GetComponentInChildren<Text>().text = transform.parent.name;
        introPanel.gameObject.SetActive(false);
        pare = transform.parent;
        iner = transform.GetComponent<BoxCollider>();
        if (iner)
        {
            iner.enabled = false;
        }
    }
   
    [ContextMenu("就为动画单个")]
    public void StartAnimIte()
    {
        transform.DOMove(tarPos,flyTime).SetEase(Ease.InOutSine);
        transform.DOLocalRotate(tarRot, flyTime).SetEase(Ease.InOutSine).OnComplete(()=> {
            introPanel.gameObject.SetActive(true);
            //可拿取
            if(iner)
            iner.enabled = true;
        });
    }
    [ContextMenu("归位动画单个")]
    public void EndAnimItem()
    {
        StartCoroutine(Delay());
    }
    public void Handle()
    {
        if (iner)
        {
            iner.enabled = false;
            transform.parent = pare;
        }
    }
    [ContextMenu("直接归位单个")]
    public void EndItemToInitPos()
    {
        if (iner)
        {
            iner.enabled = false;
            transform.SetParent(pare,false);
        }
        StartCoroutine(Delay2());
    }

    IEnumerator Delay()
    {
        yield return 1;
        introPanel.gameObject.SetActive(false);
        transform.DOMove(initPos, flyTime).SetEase(Ease.InOutSine);
        transform.DOLocalRotate(initRot, flyTime).SetEase(Ease.InOutSine);
    }
    IEnumerator Delay2()
    {
        yield return 1;
        transform.position = initPos;
        transform.localEulerAngles = initRot;
        if (sing)
        {
            transform.localEulerAngles = new Vector3(0, -92.928f, 0);
            transform.position = initPos;
        }
        if (introPanel != null)
            introPanel.gameObject.SetActive(false);
    }
    public bool sing ;
    [ContextMenu("直接就位单个")]
    public void EndItemToTarpos()
    {
        if (iner)
        {
            iner.enabled = true;
        }
        transform.position = tarPos;
        transform.localEulerAngles = tarRot;
        introPanel.gameObject.SetActive(true);
        if (iner)
            iner.enabled = true;
    }
    public void SetSpeed(float para)
    {
        flyTime = para * initflyTime;
    }


    [ContextMenu("[编辑器]：设置初始点")]
    void SaveInitInEditor()
    {
        initPos = transform.position;
        initRot = transform.localEulerAngles;
    }
    [ContextMenu("[编辑器]：设置目标点")]
    void SaveTarInEditor()
    {
        tarPos = transform.position;
        tarRot = transform.localEulerAngles;
    }
 

    [ContextMenu("[编辑器]：定位到目标点")]
    void FlyToTarInEditor()
    {
        transform.position = tarPos;
        transform.localEulerAngles = tarRot;
    }
    [ContextMenu("[编辑器]：归位到初始点")]
    void FlyToInitInEditor()
    {
        transform.position = initPos;
        transform.localEulerAngles = initRot;
    }
}
