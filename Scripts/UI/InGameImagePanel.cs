using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameImagePanel :MonoBehaviour
{

    private enum InGameUICanvasObjsEnum
    {
        DiaryImage,
        DiaryContentText,
        PictureBookImg,
        TextBookBGImg,
        TextBookTxt,

    }



    private Dictionary<InGameUICanvasObjsEnum, GameObject> inGameUICanvasObjsMap;




    private void Awake()
    {

        inGameUICanvasObjsMap = Util.MapEnumChildObjects<InGameUICanvasObjsEnum, GameObject>(this.gameObject);

        // 시작시 비활성화
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.DiaryImage].SetActive(false); 
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.PictureBookImg].SetActive(false);
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.TextBookBGImg].SetActive(false);


        ManagerObject.instance.actionManager.UsePictureBookEvent -= BindPictureBook;
        ManagerObject.instance.actionManager.UsePictureBookEvent += BindPictureBook;

        ManagerObject.instance.actionManager.UseTextBookEvent -= BindTextBook;
        ManagerObject.instance.actionManager.UseTextBookEvent += BindTextBook;


        ManagerObject.instance.actionManager.InGameQKeyEvent -= BindQKeyAction;
        ManagerObject.instance.actionManager.InGameQKeyEvent += BindQKeyAction;

        ManagerObject.instance.actionManager.InGameESCKeyEvent -= BindESCKeyAction;
        ManagerObject.instance.actionManager.InGameESCKeyEvent += BindESCKeyAction;

    }




    private void CloseAll()
    {
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.DiaryImage].SetActive(false);
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.PictureBookImg].SetActive(false);
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.TextBookBGImg].SetActive(false);
    }

    private void BindQKeyAction()
    {
        if (!inGameUICanvasObjsMap[InGameUICanvasObjsEnum.DiaryImage].activeSelf)
        {
            ManagerObject.instance.actionManager.OnPlayAudioClip(ManagerObject.instance.resourceManager.soundsmap[SoundsEnum.papersound].Result, 0.3f, false);
            CloseAll();
            inGameUICanvasObjsMap[InGameUICanvasObjsEnum.DiaryImage].SetActive(true);
            inGameUICanvasObjsMap[InGameUICanvasObjsEnum.DiaryContentText].GetComponent<Text>().text = ManagerObject.instance.actionManager.OnGetNowMissionText();
        }
    }

    private void BindESCKeyAction()
    {
        if (inGameUICanvasObjsMap[InGameUICanvasObjsEnum.DiaryImage].activeSelf
    || inGameUICanvasObjsMap[InGameUICanvasObjsEnum.PictureBookImg].activeSelf
    || inGameUICanvasObjsMap[InGameUICanvasObjsEnum.TextBookBGImg].activeSelf) ManagerObject.instance.actionManager.OnPlayAudioClip(ManagerObject.instance.resourceManager.soundsmap[SoundsEnum.papersound].Result, 0.3f, false);


        CloseAll();
    }

    private void BindPictureBook(Sprite sprite)
    {
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.PictureBookImg].GetComponent<Image>().sprite = sprite;
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.PictureBookImg].SetActive(true); // 활성화
    }

    private void BindTextBook(string text)
    {
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.TextBookTxt].GetComponent<Text>().text = text;
        inGameUICanvasObjsMap[InGameUICanvasObjsEnum.TextBookBGImg].SetActive(true); // 활성화
    }


    private void OnDestroy()
    {
        ManagerObject.instance.actionManager.UsePictureBookEvent -= BindPictureBook;
        ManagerObject.instance.actionManager.UseTextBookEvent -= BindTextBook;

        ManagerObject.instance.actionManager.InGameQKeyEvent -= BindQKeyAction;
        ManagerObject.instance.actionManager.InGameESCKeyEvent -= BindESCKeyAction;

    }
}
