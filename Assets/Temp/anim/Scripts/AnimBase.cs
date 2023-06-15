using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AnimBase : MonoBehaviour
{
    public int sort=0;


    public Vector3 initPos,initRot;
    public Vector3 tarPos,tarRot;
    public float flyTime = 1;
    private float initflyTime = 1;
    void Start()
    {
        initPos = transform.position;
        initRot = transform.localEulerAngles;
        initflyTime = flyTime;
    }
    private void Update()
    {
       
    }
    [ContextMenu("就为动画单个")]
    public void StartAnimIte()
    {
        transform.DOMove(tarPos,flyTime).SetEase(Ease.InOutSine);
        transform.DOLocalRotate(tarRot, flyTime).SetEase(Ease.InOutSine);
    }
    [ContextMenu("归位动画单个")]
    public void EndAnimItem()
    {
        transform.DOMove(initPos, flyTime).SetEase(Ease.InOutSine);
        transform.DOLocalRotate(initRot, flyTime).SetEase(Ease.InOutSine);
    }
    [ContextMenu("直接归位单个")]
    public void EndItemToInitPos()
    {
        transform.position = initPos;
        transform.localEulerAngles = initRot;
        if (sing)
        {
            transform.localEulerAngles = new Vector3(0 ,- 92.928f,0);
            transform.position = initPos;
        }
    }
    public bool sing ;
    [ContextMenu("直接就位单个")]
    public void EndItemToTarpos()
    {
        transform.position = tarPos;
        transform.localEulerAngles = tarRot;
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
