using System.Collections.Generic;
using UnityEngine;

public class InGameInventoryPanel : MonoBehaviour
{

    private enum InGameInventoryPanelObjs
    {
        InGameInventoryView,
        ContentFolder

    }

    private Dictionary<InGameInventoryPanelObjs, GameObject> InGameInventoryPanelObjsMap;


    private GameObject GetContentFolder()
    {
        return InGameInventoryPanelObjsMap[InGameInventoryPanelObjs.ContentFolder];
    }

    private void Awake()
    {
        InGameInventoryPanelObjsMap = Util.MapEnumChildObjects<InGameInventoryPanelObjs, GameObject>(this.gameObject);

        InGameInventoryPanelObjsMap[InGameInventoryPanelObjs.InGameInventoryView].SetActive(false);

        ManagerObject.instance.actionManager.GetContentFolderEvent -= GetContentFolder;
        ManagerObject.instance.actionManager.GetContentFolderEvent += GetContentFolder;

        ManagerObject.instance.actionManager.InGameESCKeyEvent -= CloseInventoryPanel;
        ManagerObject.instance.actionManager.InGameESCKeyEvent += CloseInventoryPanel;

        ManagerObject.instance.actionManager.InGameTabKeyEvent -= InventoryOpen;
        ManagerObject.instance.actionManager.InGameTabKeyEvent += InventoryOpen;
    }



    private void OnDestroy()
    {
        ManagerObject.instance.actionManager.GetContentFolderEvent -= GetContentFolder;
        ManagerObject.instance.actionManager.InGameESCKeyEvent -= CloseInventoryPanel;
        ManagerObject.instance.actionManager.InGameTabKeyEvent -= InventoryOpen;

    }



    private void CloseInventoryPanel()
    {
        if (InGameInventoryPanelObjsMap[InGameInventoryPanelObjs.InGameInventoryView].activeSelf)
        {
            InGameInventoryPanelObjsMap[InGameInventoryPanelObjs.InGameInventoryView].SetActive(false);
        }
    }


    private void InventoryOpen()
    {
        if (!InGameInventoryPanelObjsMap[InGameInventoryPanelObjs.InGameInventoryView].activeSelf)
        {
            ManagerObject.instance.actionManager.OnPlayAudioClip(ManagerObject.instance.resourceManager.soundsmap[SoundsEnum.papersound].Result, 0.3f, false);
            InGameInventoryPanelObjsMap[InGameInventoryPanelObjs.InGameInventoryView].SetActive(true);
        }
    }


}
