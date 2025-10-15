using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractor : MonoBehaviour
{



    private GameObject rayObject;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(UnityEngine.Camera.main.transform.position, UnityEngine.Camera.main.transform.forward, out hit, 3))
        {
            rayObject = hit.transform.gameObject;
            if (!TryHandleRayTarget(rayObject))
                ManagerObject.instance.actionManager.OnUseUIOff();
        }
        else
        {
            rayObject = null;
            ManagerObject.instance.actionManager.OnUseUIOff();
        }
    }

    bool TryHandleRayTarget(GameObject obj)
    {
        if (obj == null) return false;

        string tag = obj.tag.ToLowerInvariant();

        return tag switch
        {
            "picturebook" => HandleInteraction(ItemType.PictureBook, obj, "E 읽기"),
            "textbook" => HandleInteraction(ItemType.Textbook, obj, "E 읽기"),
            "door" => HandleInteraction(ItemType.Door, obj, "E 열기"),
            "key" => HandleInteraction(ItemType.Key, obj, "E 획득"),
            "movingwall" => HandleInteraction(ItemType.MovingWall, obj, "E 밀기"),
            _ => false
        };
    }

    bool HandleInteraction(ItemType type, GameObject obj, string message)
    {
        ManagerObject.instance.actionManager.OnUseUIOn(message);
        if (Input.GetKeyDown(KeyCode.E))
        {


            if (type == ItemType.PictureBook)
            {
                ManagerObject.instance.soundManager.PlayAudioClip(ManagerObject.instance.resourceManager.soundsmap[SoundsEnum.papersound].Result, 0.3f, false);
                ManagerObject.instance.actionManager.OnUsePictureBook(obj.GetComponent<Image>().sprite);
            }
            else if (type == ItemType.Textbook)
            {
                ManagerObject.instance.soundManager.PlayAudioClip(ManagerObject.instance.resourceManager.soundsmap[SoundsEnum.papersound].Result, 0.3f, false);
                ManagerObject.instance.actionManager.OnUseTextBook(obj.GetComponent<Text>().text);

            }
            else if (type == ItemType.Door)
            {

                if (ManagerObject.instance.actionManager.OnFindInven(obj.GetComponent<Door>().KeyName)) // 해당 문의 door class의 KeyName이 인벤토리에 있는지 확인
                {
                    obj.transform.Rotate(0, 90, 0); //Y 90도 회전
                    obj.GetComponent<Collider>().enabled = false;
                    ManagerObject.instance.soundManager.PlayAudioClip(ManagerObject.instance.resourceManager.soundsmap[SoundsEnum.doorsound].Result, 0.3f, false);
                }
                else
                {
                    ManagerObject.instance.actionManager.OnRequireKey();
                }
            }
            else if (type == ItemType.Key)
            {
                ManagerObject.instance.actionManager.OnGetItem(obj);
                ManagerObject.instance.soundManager.PlayAudioClip(ManagerObject.instance.resourceManager.soundsmap[SoundsEnum.papersound].Result, 0.3f, false);
                obj.GetComponent<Key>().DisableAction();
                obj.SetActive(false);
            }
            else if (type == ItemType.MovingWall)
            {
                obj.GetComponent<MovingWall>().moveon = true;
            }




        }
        return true;
    }

}
