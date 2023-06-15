using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class JumpEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Jump();
    }
    Tween t;
    public void Jump()
    {
        MyPingPang(0.44f,0.8f);
    }

    public void Stand()
    {
        t.Kill();
    }

    private void MyPingPang(float from,float to)
    {
       t= transform.DOLocalMoveY(to, 2).OnComplete(() => { 
            MyPingPang(to, from); 
        }).SetEase(Ease.OutSine);
    }
}
