using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimManager : MonoBehaviour
{
    public List<AnimBase> animList = new List<AnimBase>();
    void Start()
    {
        animList = transform.GetComponentsInChildren<AnimBase>().ToList();
        animList = animList.OrderBy(item => item.sort).ToList();
    }

    [ContextMenu("拆解动画")]
    public void StartAnim()
    {
        if (t != null)
        {
            StopCoroutine(t);
        }
        if (isAnim==false)
        {
            BackAnimToInitPos();
        }

        t = StartCoroutine(Anim(true));
    }
    [ContextMenu("装配动画")]
    public void EndAnim()
    {
        if (t != null)
        {
            StopCoroutine(t);
        }
        t = StartCoroutine(Anim(false));
    }
    [ContextMenu("直接归位动画")]
    public void BackAnimToInitPos()
    {
        if (animList.Count == 0)
        {
            animList = transform.GetComponentsInChildren<AnimBase>().ToList();
        }
        foreach (var item in animList)
        {
            item.EndItemToInitPos();
        }
    }
    [ContextMenu("直接就位动画")]
    public void BackAnimToTarPos()
    {
        if (animList.Count == 0)
        {
            animList = transform.GetComponentsInChildren<AnimBase>().ToList();
        }
        foreach (var item in animList)
        {
            item.EndItemToTarpos();
        }
    }
    public void AnimSpeedUp()
    {
        if (animList.Count == 0)
        {
            animList = transform.GetComponentsInChildren<AnimBase>().ToList();
        }
        foreach (var item in animList)
        {
            item.SetSpeed(0.5f);
        }
    }

    public void AnimSpeedDown()
    {
        if (animList.Count == 0)
        {
            animList = transform.GetComponentsInChildren<AnimBase>().ToList();
        }
        foreach (var item in animList)
        {
            item.SetSpeed(2f);
        }
    }


    Coroutine t;
    bool isAnim = false;
    IEnumerator Anim(bool isPositivesEquence)
    {
        int count = animList.Count;
        if (count == 0)
        {
            yield break;
        }
        int tempSort = 0;
        isAnim = true;
        if (isPositivesEquence)
        {
            tempSort = 0;
            while (tempSort<count)
            {
                AnimBase ab = animList[tempSort];
                ab.StartAnimIte();
                yield return new WaitForSeconds(ab.flyTime);
                tempSort++;
            }
            isAnim = false;
            Debug.Log("播放完成");
        }
        else if (!isPositivesEquence)
        {
            tempSort = count - 1;
            while (tempSort > -1)
            {
                AnimBase ab = animList[tempSort];
                ab.EndAnimItem();
                yield return new WaitForSeconds(ab.flyTime);
                tempSort--;
            }
            isAnim = false;
            Debug.Log("播放完成");
        }
    }
}
