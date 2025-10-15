using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameFadePanel : MonoBehaviour
{

    private enum InGameFadePanelObjs
    {
        FadeImg

    }

    private Action FadeAction;
    private Dictionary<InGameFadePanelObjs, GameObject> InGameFadePanelObjsMap;


    private void Awake()
    {

        InGameFadePanelObjsMap = Util.MapEnumChildObjects<InGameFadePanelObjs, GameObject>(this.gameObject);
        InGameFadePanelObjsMap[InGameFadePanelObjs.FadeImg].SetActive(true); //페이드인 아웃은 알파값으로 조절, 항상 켜진상태

        ManagerObject.instance.actionManager.FadeInEvent -= BindFadeIn;
        ManagerObject.instance.actionManager.FadeInEvent += BindFadeIn;

        ManagerObject.instance.actionManager.FadeOutEvent -= BindFadeOut;
        ManagerObject.instance.actionManager.FadeOutEvent += BindFadeOut;
    }

    private void Update()
    {
        FadeAction?.Invoke();
    }


    private void SetAlpha(float alpha)
    {
        var image = InGameFadePanelObjsMap[InGameFadePanelObjs.FadeImg].GetComponent<Image>();
        Color color = image.color;
        color.a = Mathf.Clamp01(alpha);
        image.color = color;
    }



    private void FadeIn()
    {
        var image = InGameFadePanelObjsMap[InGameFadePanelObjs.FadeImg].GetComponent<Image>();
        Color color = image.color;
        color.a += 0.001f;
        if (color.a >= 1f)
        {
            color.a = 1f;
            FadeAction -= FadeIn;
        }
        image.color = color;
    }

    private void FadeOut()
    {
        var image = InGameFadePanelObjsMap[InGameFadePanelObjs.FadeImg].GetComponent<Image>();
        Color color = image.color;
        color.a -= 0.001f;
        if (color.a <= 0f)
        {
            color.a = 0f;
            FadeAction -= FadeOut;
        }
        image.color = color;
    }





    private void BindFadeIn()
    {
        SetAlpha(0f);
        FadeAction = FadeIn;
    }

    private void BindFadeOut()
    {
        SetAlpha(1f);
        FadeAction = FadeOut;
    }

    private void OnDestroy()
    {
        ManagerObject.instance.actionManager.FadeInEvent -= BindFadeIn;
        ManagerObject.instance.actionManager.FadeOutEvent -= BindFadeOut;
    }
}
