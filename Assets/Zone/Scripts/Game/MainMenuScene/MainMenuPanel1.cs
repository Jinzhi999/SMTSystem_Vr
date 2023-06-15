using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPanel1 : MonoBehaviour
{
    public VRUIButon 结构原理, 实操训练;
    public JumpEffect 结构原理_Jump;
    void Start()
    {
        结构原理.OnClickDn.AddListener(() => {

            StartCoroutine(DelayEnterScene()); 
        });
        结构原理.OnPointEnterEvent.AddListener(() => {
            // 停止运动
            ParticleSystem ps = 结构原理.GetComponent<ParticleSystem>();
            ParticleSystem.EmissionModule emis = ps.emission;
            emis.enabled = false;

            ParticleSystem pf = 结构原理.transform.Find("Fire").GetComponent<ParticleSystem>();
            ParticleSystem.EmissionModule emisf = pf.emission;
            emisf.enabled = false;

            结构原理_Jump.Stand();
        });
        结构原理.OnPointExitEvent.AddListener(() => {
            // 停止运动
            ParticleSystem ps = 结构原理.GetComponent<ParticleSystem>();
            ParticleSystem.EmissionModule emis = ps.emission;
            emis.enabled = true;

            ParticleSystem pf = 结构原理.transform.Find("Fire").GetComponent<ParticleSystem>();
            ParticleSystem.EmissionModule emisf = pf.emission;
            emisf.enabled = true;

            结构原理_Jump.Jump();
        });
    }
    IEnumerator DelayEnterScene()
    {
        yield return new WaitForSeconds(1.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("0_结构原理_课程选择");
    }
}
