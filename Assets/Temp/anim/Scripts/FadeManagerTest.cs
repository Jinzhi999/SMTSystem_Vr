using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FadeManagerTest : MonoBehaviour
{
    Dictionary<Renderer, Material[]> renderDict = new Dictionary<Renderer, Material[]>();

    public Material fadeMateril, gridMaterial;
    void Start()
    {
        List<Renderer> renderList = transform.GetComponentsInChildren<Renderer>(true).ToList();
        foreach (var item in renderList)
        {
            renderDict.Add(item, item.materials);
        }
    }


    [ContextMenu("»Ø¸´")]
    public void SetInitMaterial()
    {
        foreach (var item in renderDict)
        {
            item.Key.materials = item.Value;
        }
    }
    [ContextMenu("Íø¸ñ")]
    public void SetGridMaterial()
    {
       // StartCoroutine(Delay());
       // return;
        foreach (var item in renderDict)
        {
            Material[] ms = item.Key.materials;
            for (int i = 0; i < ms.Length; i++)
            {
                ms[i] = gridMaterial;
            }
            item.Key.materials = ms;
        }
    }
    [ContextMenu("Í¸Ã÷")]
    public void SetFadeMaterial()
    {
        foreach (var item in renderDict)
        {
            Material[] ms = item.Key.materials;
            for (int i = 0; i < ms.Length; i++)
            {
                ms[i] = fadeMateril;
            }
            item.Key.materials = ms;
        }
    }

    IEnumerator Delay()
    {
        foreach (var item in renderDict)
        {
            Material[] ms = item.Key.materials;
            for (int i = 0; i < ms.Length; i++)
            {
                ms[i] = gridMaterial;
            }
            item.Key.materials = ms;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
