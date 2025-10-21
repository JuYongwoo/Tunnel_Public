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

        ManagerObject.instance.eventManager.GetContentFolderEvent -= GetContentFolder;
        ManagerObject.instance.eventManager.GetContentFolderEvent += GetContentFolder;

        ManagerObject.instance.eventManager.InGameESCKeyEvent -= CloseInventoryPanel;
        ManagerObject.instance.eventManager.InGameESCKeyEvent += CloseInventoryPanel;

        ManagerObject.instance.eventManager.InGameTabKeyEvent -= InventoryOpen;
        ManagerObject.instance.eventManager.InGameTabKeyEvent += InventoryOpen;
    }

    private void Start()
    {
        InGameInventoryPanelObjsMap[InGameInventoryPanelObjs.InGameInventoryView].SetActive(false);
    }



    private void OnDestroy()
    {
        ManagerObject.instance.eventManager.GetContentFolderEvent -= GetContentFolder;
        ManagerObject.instance.eventManager.InGameESCKeyEvent -= CloseInventoryPanel;
        ManagerObject.instance.eventManager.InGameTabKeyEvent -= InventoryOpen;

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
            ManagerObject.instance.eventManager.OnPlayAudioClip(ManagerObject.instance.resourceManager.soundsmap[SoundsEnum.papersound].Result, 0.3f, false);
            InGameInventoryPanelObjsMap[InGameInventoryPanelObjs.InGameInventoryView].SetActive(true);
        }
    }


}
