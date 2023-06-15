using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;

public class VR_ScrollSlider : VRUIButon
{
    public SteamVR_LaserPointer leaser;

    Vector3 hitPint;
    public bool inSlider = false;
    public bool startChange = false;

    public RectTransform scrollContent;
    private float initHight=-0.207f;
    private float maxHight= 0f;
    int row = 0;
    // 0.207  0.7
    /// <summary>
    /// 行数
    /// </summary>
    /// <param name="count"></param>
    public void Init(int count)
    {
        row = count;
        if (count < 3)
        {
            gameObject.SetActive(false);
        }
        else if (count == 3)
        {
            maxHight = 0.207f;
        }
        else
        {
            maxHight = 0.207f+(row-3)*0.7f;
        }
    }

    public override void Start()
    {
        base.Start();
        hitPint = leaser.hit.point;
    }

    public override void OnPointEnter()
    {
        inSlider = true;
    }

    public override void OnPointExit()
    {
        inSlider = false;
        startChange = false;
    }

    public override void OnPointClickUp()
    {
        Debug.Log("up");
        startChange = false;
    }

    public override void OnPointClickDn()
    {
        Debug.Log("down ");
        hitPint = leaser.hit.point;
        startChange = true;
    }

    
    float change = 0;
    float oldFloat = 0;
    float newFloat = 0;
    float high= 0;
    float para = 0;
    private void Update()
    {
        if (inSlider && startChange)
        {
            oldFloat = hitPint.y;
            hitPint = leaser.hit.point;
            newFloat = hitPint.y;

            change = newFloat - oldFloat;
            oldFloat = newFloat;
            high = transform.localPosition.y+ change;
            if (high >= 0.4f)
            {
                high = 0.4f;
            }
            else if (high <= -0.4f)
            {
                high = -0.4f;
            }
            //high = Mathf.Clamp(high, -1.2f, -0.4f);
            transform.localPosition = new Vector3(0,high,0);
            para = (0.4f+high)*1.25f;
            scrollContent.anchoredPosition = new Vector2(0.1f, maxHight + (-maxHight+initHight)* para);
            // 获取 行数 
            //  第三行 到0.207
            // 后续每次加0.7f
        }
    }
}
