using System;
using System.Collections.Generic;
using UnityEngine;


public class ActionManager
{
    public event Action<GameObject> getItemAction;
    public event Func<string, bool> findInvenAction;

    public event Action InGameTabKeyAction;
    public event Action InGameQKeyAction;
    public event Action InGameESCKeyAction;

    public event Action FadeIn;
    public event Action FadeOut;
    public event Action<List<string>> Speech;


    public event Action<string> UseUIOn;
    public event Action UseUIOff;
    public event Action<Sprite> UsePictureBook;
    public event Action<string> UseTextBook;
    public event Action requireKey;

    public event Func<string> getNowMissionText;

    public GameObject ThisScenePlayer;

    public void OnGetItem(GameObject go)
    {
        getItemAction?.Invoke(go);
    }

    public bool OnFindInvenAction(string itemName)
    {
        return findInvenAction?.Invoke(itemName) ?? false;
    }

    public void OnInGameTabKeyAction()
    {
        InGameTabKeyAction?.Invoke();
    }

    public void OnInGameQKeyAction()
    {
        InGameQKeyAction?.Invoke();
    }

    public void OnInGameESCKeyAction()
    {
        InGameESCKeyAction?.Invoke();
    }

    public void OnFadeIn() {
        FadeIn?.Invoke();
    }

    public void OnFadeOut() {
        FadeOut?.Invoke();
    }

    public void OnSpeech(List<string> speeches) {
        Speech?.Invoke(speeches);
    }

    public void OnUseUIOn(string text) {
        UseUIOn?.Invoke(text);
    }

    public void OnUseUIOff() {
        UseUIOff?.Invoke();
    }


    public void OnUsePictureBook(Sprite sprite)
    {
        UsePictureBook?.Invoke(sprite);
    }

    public void OnUseTextBook(string text)
    {
        UseTextBook?.Invoke(text);
    }

    public void OnrequireKey()
    {
        requireKey?.Invoke();
    }

    public string OnGetNowMissionText()
    {
        return getNowMissionText?.Invoke() ?? "";
    }


}
