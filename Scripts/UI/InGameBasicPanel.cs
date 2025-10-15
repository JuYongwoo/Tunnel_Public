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


        // ���� �� Ȱ��ȭ
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
        InGameBasicPanelObjsMap[InGameBasicPanelObjs.AlertText].GetComponent<Text>().text = "���谡 �ʿ��մϴ�.";
    }
    private void EndrequirekeyAlert()
    {
        InGameBasicPanelObjsMap[InGameBasicPanelObjs.AlertText].GetComponent<Text>().text = "";
    }




    public void Stopspeech()
    {
        if (currentSpeechCoroutine != null)
        {
            StopCoroutine(currentSpeechCoroutine); //���ο� ��� �����ϸ� ���� ��� �ڷ�ƾ ��ž
            currentSpeechCoroutine = null; //��ž �� �ڷ�ƾ ����
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
            InGameBasicPanelObjsMap[InGameBasicPanelObjs.PlayerSpeechText].GetComponent<Text>().text = ""; //������ ����
        }
        currentSpeechCoroutine = null; //��� ��
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
        Startspeech(speeches); // params string[] �� �迭 �Ǵ� ���� ���� ����
    }




}
