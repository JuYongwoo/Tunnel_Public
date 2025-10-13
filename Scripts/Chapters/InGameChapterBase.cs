using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class InGameChapterBase : MonoBehaviour
{
    public TriggerEventManager triggerEvent = new TriggerEventManager();
    public DoorKeyManager doorKey = new DoorKeyManager();
    private GameObject[] inven = new GameObject[30];
    private string[] invencontentname = new string[30];
    private int invencount = 0;
    private InGameUICanvas inGameUICanvas;

    private GameObject InCodePrefab;


    protected virtual void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        ManagerObject.instance.resource.cacheSceneObjects(); //���� �ִ� ������Ʈ���� ĳ��

        ManagerObject.instance.actionManager.ThisScenePlayer = GameObject.FindGameObjectWithTag("Player"); // ���� �������� Player ������Ʈ, ������ �ѹ��� ����


        inGameUICanvas = GameObject.FindFirstObjectByType<InGameUICanvas>();
        triggerEvent.OnStart(SceneManager.GetActiveScene()); //������� Ʈ���� �ε�
        doorKey.OnStart(); //������� ���� ���� ��Ī
        ManagerObject.instance.actionManager.getItemAction -= getItem;
        ManagerObject.instance.actionManager.getItemAction += getItem;
        ManagerObject.instance.actionManager.findInvenAction -= findinven;
        ManagerObject.instance.actionManager.findInvenAction += findinven;

    }

    protected void Update()
    {
        triggerEvent.OnUpdate();
    }


    protected virtual void OnDestroy()
    {
        ManagerObject.instance.actionManager.getItemAction -= getItem;
        ManagerObject.instance.actionManager.findInvenAction -= findinven;
        triggerEvent.OnDestroy();

    }



    public void getItem(GameObject item)
    {
        //������ ȹ�� �� �ڵ����� ����Ǵ� �̺�Ʈ�� TriggerEventManager���� ��������Ʈ�� ó�� �ϵ��� �����Ͽ���.
        inven[invencount] = item;
        invennamesubmit();
        invencount += 1;

        GameObject itemininventoryprefab = new GameObject();
        itemininventoryprefab.AddComponent<Image>();
        InCodePrefab = GameObject.Instantiate(itemininventoryprefab); //�κ��丮�� ��ư(������)����
        InCodePrefab.transform.SetParent(inGameUICanvas.inGameUICanvasObjsMap[InGameUICanvas.InGameUICanvasObjsEnum.ContentFolder].transform); //������ ��ȯ �� content �Ʒ��� ����
        InCodePrefab.GetComponent<Image>().sprite = item.GetComponent<Image>().sprite;
    }
    public void invennamesubmit()
    {
        invencontentname[invencount] = inven[invencount].name;
    }
    public bool findinven(string name)
    {
        int pos = Array.IndexOf(invencontentname, name);
        if (pos > -1)
        {
            return true;
        }
        else
        {
            return false;
        }

    }


}