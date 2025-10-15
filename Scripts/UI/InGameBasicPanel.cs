using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameBasicPanel : MonoBehaviour
{

    private enum InGameBasicPanelObjs
    {
        AlertText,
        UseText,
        QDiaryText,
        TabInventoryText,
        PlayerSpeechText
    }
    private Coroutine currentSpeechCoroutine;


    private Dictionary<InGameBasicPanelObjs, GameObject> InGameBasicPanelObjsMap;


    private void Awake()
    {

        InGameBasicPanelObjsMap = Util.MapEnumChildObjects<InGameBasicPanelObjs, GameObject>(this.gameObject);


        // 시작 시 활성화
        InGameBasicPanelObjsMap[InGameBasicPanelObjs.QDiaryText].SetActive(true);
        InGameBasicPanelObjsMap[InGameBasicPanelObjs.TabInventoryText].SetActive(true);
        InGameBasicPanelObjsMap[InGameBasicPanelObjs.AlertText].SetActive(true);
        InGameBasicPanelObjsMap[InGameBasicPanelObjs.UseText].SetActive(true);
        InGameBasicPanelObjsMap[InGameBasicPanelObjs.PlayerSpeechText].SetActive(true);


        ManagerObject.instance.actionManager.RequireKeyEvent -= BindRequireKey;
        ManagerObject.instance.actionManager.RequireKeyEvent += BindRequireKey;


        ManagerObject.instance.actionManager.UseUIOnEvent -= BindUseUIOn;
        ManagerObject.instance.actionManager.UseUIOnEvent += BindUseUIOn;

        ManagerObject.instance.actionManager.UseUIOffEvent -= BindUseUIOff;
        ManagerObject.instance.actionManager.UseUIOffEvent += BindUseUIOff;

        ManagerObject.instance.actionManager.SpeechEvent -= BindSpeech;
        ManagerObject.instance.actionManager.SpeechEvent += BindSpeech;
    }

    private void OnDestroy()
    {

        ManagerObject.instance.actionManager.RequireKeyEvent -= BindRequireKey;
        ManagerObject.instance.actionManager.UseUIOnEvent -= BindUseUIOn;
        ManagerObject.instance.actionManager.UseUIOffEvent -= BindUseUIOff;
        ManagerObject.instance.actionManager.SpeechEvent -= BindSpeech;
    }



    private void RequirekeyAlert()
    {
        InGameBasicPanelObjsMap[InGameBasicPanelObjs.AlertText].GetComponent<Text>().text = "열쇠가 필요합니다.";
    }
    private void EndrequirekeyAlert()
    {
        InGameBasicPanelObjsMap[InGameBasicPanelObjs.AlertText].GetComponent<Text>().text = "";
    }




    public void Stopspeech()
    {
        if (currentSpeechCoroutine != null)
        {
            StopCoroutine(currentSpeechCoroutine); //새로운 대사 시작하면 기존 대사 코루틴 스탑
            currentSpeechCoroutine = null; //스탑 후 코루틴 비우기
        }
    }

    public void Startspeech(List<string> lines)
    {
        Stopspeech();
        currentSpeechCoroutine = StartCoroutine(StartspeechCoroutine(lines));
    }


    public IEnumerator StartspeechCoroutine(List<string> lines)
    {
        for (int i = 0; i < lines.Count; i++)
        {
            InGameBasicPanelObjsMap[InGameBasicPanelObjs.PlayerSpeechText].GetComponent<Text>().text = lines[i];
            yield return new WaitForSeconds(4.0f);
            InGameBasicPanelObjsMap[InGameBasicPanelObjs.PlayerSpeechText].GetComponent<Text>().text = ""; //끝나면 공백
        }
        currentSpeechCoroutine = null; //대사 끝
    }



    private void BindRequireKey()
    {
        RequirekeyAlert();
        Invoke(nameof(EndrequirekeyAlert), 3f);
    }

    private void BindUseUIOff()
    {
        InGameBasicPanelObjsMap[InGameBasicPanelObjs.UseText].GetComponent<Text>().text = "";
    }

    private void BindUseUIOn(string str)
    {
        InGameBasicPanelObjsMap[InGameBasicPanelObjs.UseText].GetComponent<Text>().text = str;
    }

    private void BindSpeech(List<string> speeches)
    {
        Stopspeech();
        Startspeech(speeches); // params string[] 라서 배열 또는 여러 인자 가능
    }




}
