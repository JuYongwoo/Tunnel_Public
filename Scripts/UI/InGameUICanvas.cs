using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUICanvas :MonoBehaviour
{

    public enum InGameUICanvasObjsEnum
    {
        InventoryPanel,
        DiaryPanel,
        DiaryUIText,
        PictureBookPanel,
        PictureBookImg,
        TextBookPanel,
        TextBookTxt,
        BasicPanel,
        UsePanel,
        UseTxt,
        AlertPanel,
        AlertTxt,
        ContentFolder,
        PlayerSpeechPanel,
        PlayerSpeechTxt,
        FadePanel,
        FadeImg

    }

    private Action FadeAction;




    public Dictionary<InGameUICanvasObjsEnum, GameObject> inGameUICanvasObjsMap;



    private Coroutine currentSpeechCoroutine;

    public void Awake()
    {

        inGameUICanvasObjsMap = Util.MapEnumChildObjects<InGameUICanvasObjsEnum, GameObject>(this.gameObject);

        // ���۽� ��Ȱ��ȭ
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.InventoryPanel].SetActive(false);
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.DiaryPanel].SetActive(false); 
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.PictureBookPanel].SetActive(false);
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.TextBookPanel].SetActive(false);
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.UsePanel].SetActive(false);
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.AlertPanel].SetActive(false);

        // ���� �� Ȱ��ȭ
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.BasicPanel].SetActive(true);
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.PlayerSpeechPanel].SetActive(true);
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.FadePanel].SetActive(true); //���̵��� �ƿ��� ���İ����� ����, �׻� ��������

        assignUIEvents();
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().name == "Title") return;
        if (SceneManager.GetActiveScene().name == "GameOver") return;

        FadeAction?.Invoke();
    }


    private void SetAlpha(float alpha)
    {
        var image = inGameUICanvasObjsMap[InGameUICanvasObjsEnum.FadeImg].GetComponent<Image>();
        Color color = image.color;
        color.a = Mathf.Clamp01(alpha);
        image.color = color;
    }



    private void FadeIn()
    {
        var image = inGameUICanvasObjsMap[InGameUICanvasObjsEnum.FadeImg].GetComponent<Image>();
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
        var image = inGameUICanvasObjsMap[InGameUICanvasObjsEnum.FadeImg].GetComponent<Image>();
        Color color = image.color;
        color.a -= 0.001f;
        if (color.a <= 0f)
        {
            color.a = 0f;
            FadeAction -= FadeOut;
        }
        image.color = color;
    }

    private void requirekeyAlert()
    {
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.AlertPanel].SetActive(true);
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.AlertTxt].GetComponent<Text>().text = "���谡 �ʿ��մϴ�.";
    }
    private void endrequirekeyAlert()
    {
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.AlertPanel].SetActive(false);
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.AlertTxt].GetComponent<Text>().text = "";
    }

    public void closeAll()
    {
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.DiaryPanel].SetActive(false);
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.PictureBookPanel].SetActive(false);
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.TextBookPanel].SetActive(false);
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.InventoryPanel].SetActive(false);
    }



    public void stopspeech()
    {
        if (currentSpeechCoroutine != null)
        {
            StopCoroutine(currentSpeechCoroutine); //���ο� ��� �����ϸ� ���� ��� �ڷ�ƾ ��ž
            currentSpeechCoroutine = null; //��ž �� �ڷ�ƾ ����
        }
    }

    public void startspeech(List<string> lines)
    {
        stopspeech();
        currentSpeechCoroutine = StartCoroutine(StartspeechCoroutine(lines));
    }


    public IEnumerator StartspeechCoroutine(List<string> lines)
    {
        for (int i = 0; i < lines.Count; i++)
        {
            inGameUICanvasObjsMap[InGameUICanvasObjsEnum.PlayerSpeechTxt].GetComponent<Text>().text= lines[i];
            yield return new WaitForSeconds(4.0f);
            inGameUICanvasObjsMap[InGameUICanvasObjsEnum.PlayerSpeechTxt].GetComponent<Text>().text = ""; //������ ����
        }
        currentSpeechCoroutine = null; //��� ��
    }


    void assignUIEvents()
    {

        ManagerObject.instance.actionManager.UsePictureBook -= bindPictureBook;
        ManagerObject.instance.actionManager.UsePictureBook += bindPictureBook;

        ManagerObject.instance.actionManager.UseTextBook -= bindTextBook;
        ManagerObject.instance.actionManager.UseTextBook += bindTextBook;

        ManagerObject.instance.actionManager.requireKey -= bindRequireKey;
        ManagerObject.instance.actionManager.requireKey += bindRequireKey;

        ManagerObject.instance.actionManager.InGameTabKeyAction -= bindTabKeyAction;
        ManagerObject.instance.actionManager.InGameTabKeyAction += bindTabKeyAction;

        ManagerObject.instance.actionManager.InGameQKeyAction -= bindQKeyAction;
        ManagerObject.instance.actionManager.InGameQKeyAction += bindQKeyAction;

        ManagerObject.instance.actionManager.InGameESCKeyAction -= bindESCKeyAction;
        ManagerObject.instance.actionManager.InGameESCKeyAction += bindESCKeyAction;

        ManagerObject.instance.actionManager.UseUIOn -= bindUseUIOn;
        ManagerObject.instance.actionManager.UseUIOn += bindUseUIOn;

        ManagerObject.instance.actionManager.UseUIOff -= bindUseUIOff;
        ManagerObject.instance.actionManager.UseUIOff += bindUseUIOff;

        ManagerObject.instance.actionManager.Speech -= bindSpeech;
        ManagerObject.instance.actionManager.Speech += bindSpeech;

        ManagerObject.instance.actionManager.FadeIn -= bindFadeIn;
        ManagerObject.instance.actionManager.FadeIn += bindFadeIn;

        
        ManagerObject.instance.actionManager.FadeOut -= bindFadeOut;
        ManagerObject.instance.actionManager.FadeOut += bindFadeOut;
    }

    private void bindTabKeyAction()
    {
        if (!inGameUICanvasObjsMap[InGameUICanvasObjsEnum.InventoryPanel].activeSelf)
        {
            ManagerObject.instance.sound.PlayAudioClip(ManagerObject.instance.resource.soundsmap[SoundsEnum.papersound].Result, 0.3f, false);
            closeAll();
            inGameUICanvasObjsMap[InGameUICanvasObjsEnum.InventoryPanel].SetActive(true);
            inGameUICanvasObjsMap[InGameUICanvasObjsEnum.UsePanel].SetActive(false);
            inGameUICanvasObjsMap[InGameUICanvasObjsEnum.BasicPanel].SetActive(false);
        }
    }

    private void bindQKeyAction()
    {
        if (!inGameUICanvasObjsMap[InGameUICanvasObjsEnum.DiaryPanel].activeSelf)
        {
            ManagerObject.instance.sound.PlayAudioClip(ManagerObject.instance.resource.soundsmap[SoundsEnum.papersound].Result, 0.3f, false);
            closeAll();
            inGameUICanvasObjsMap[InGameUICanvasObjsEnum.DiaryPanel].SetActive(true);
            inGameUICanvasObjsMap[InGameUICanvasObjsEnum.DiaryUIText].GetComponent<Text>().text = ManagerObject.instance.actionManager.OnGetNowMissionText();
            inGameUICanvasObjsMap[InGameUICanvasObjsEnum.UsePanel].SetActive(false);
            inGameUICanvasObjsMap[InGameUICanvasObjsEnum.BasicPanel].SetActive(false);
        }
    }

    private void bindESCKeyAction()
    {
        if (inGameUICanvasObjsMap[InGameUICanvasObjsEnum.DiaryPanel].activeSelf
    || inGameUICanvasObjsMap[InGameUICanvasObjsEnum.PictureBookPanel].activeSelf
    || inGameUICanvasObjsMap[InGameUICanvasObjsEnum.TextBookPanel].activeSelf
    || inGameUICanvasObjsMap[InGameUICanvasObjsEnum.InventoryPanel].activeSelf) ManagerObject.instance.sound.PlayAudioClip(ManagerObject.instance.resource.soundsmap[SoundsEnum.papersound].Result, 0.3f, false);


        closeAll();
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.UsePanel].SetActive(true);
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.BasicPanel].SetActive(true);
    }

    private void bindUseUIOn(string str)
    {
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.UseTxt].GetComponent<Text>().text = str;
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.UsePanel].SetActive(true);
    }

    private void bindPictureBook(Sprite sprite)
    {
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.PictureBookImg].GetComponent<Image>().sprite = sprite;
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.PictureBookPanel].SetActive(true); // Ȱ��ȭ
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.UsePanel].SetActive(false);
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.BasicPanel].SetActive(false);
    }

    private void bindTextBook(string text)
    {
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.TextBookTxt].GetComponent<Text>().text = text;
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.TextBookPanel].SetActive(true); // Ȱ��ȭ
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.UsePanel].SetActive(false);
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.BasicPanel].SetActive(false);
    }

    private void bindRequireKey()
    {
        requirekeyAlert();
        Invoke("endrequirekeyAlert", 3f);
    }

    private void bindUseUIOff()
    {
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.UseTxt].GetComponent<Text>().text = "";
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.UsePanel].SetActive(false);
    }

    private void bindSpeech(List<string> speeches)
    {
        stopspeech();
        startspeech(speeches); // params string[] �� �迭 �Ǵ� ���� ���� ����
    }

    private void bindFadeIn()
    {
        SetAlpha(0f);
        FadeAction = FadeIn;
    }

    private void bindFadeOut()
    {
        SetAlpha(1f);
        FadeAction = FadeOut;
    }

    private void OnDestroy()
    {
        ManagerObject.instance.actionManager.UsePictureBook -= bindPictureBook;
        ManagerObject.instance.actionManager.UseTextBook -= bindTextBook;
        ManagerObject.instance.actionManager.requireKey -= bindRequireKey;

        ManagerObject.instance.actionManager.InGameTabKeyAction -= bindTabKeyAction;
        ManagerObject.instance.actionManager.InGameQKeyAction -= bindQKeyAction;
        ManagerObject.instance.actionManager.InGameESCKeyAction -= bindESCKeyAction;

        ManagerObject.instance.actionManager.UseUIOn -= bindUseUIOn;
        ManagerObject.instance.actionManager.UseUIOff -= bindUseUIOff;
        ManagerObject.instance.actionManager.Speech -= bindSpeech;
        ManagerObject.instance.actionManager.FadeIn -= bindFadeIn;
        ManagerObject.instance.actionManager.FadeOut -= bindFadeOut;
    }
}
