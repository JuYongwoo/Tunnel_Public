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
                ManagerObject.instance.eventManager.OnUseUIOff();
        }
        else
        {
            rayObject = null;
            ManagerObject.instance.eventManager.OnUseUIOff();
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
        ManagerObject.instance.eventManager.OnUseUIOn(message);
        if (Input.GetKeyDown(KeyCode.E))
        {


            if (type == ItemType.PictureBook)
            {
                ManagerObject.instance.eventManager.OnPlayAudioClip(ManagerObject.instance.resourceManager.soundsmap[SoundsEnum.papersound].Result, 0.3f, false);
                ManagerObject.instance.eventManager.OnUsePictureBook(obj.GetComponent<Image>().sprite);
            }
            else if (type == ItemType.Textbook)
            {
                ManagerObject.instance.eventManager.OnPlayAudioClip(ManagerObject.instance.resourceManager.soundsmap[SoundsEnum.papersound].Result, 0.3f, false);
                ManagerObject.instance.eventManager.OnUseTextBook(obj.GetComponent<Text>().text);

            }
            else if (type == ItemType.Door)
            {

                if (ManagerObject.instance.eventManager.OnFindInven(obj.GetComponent<Door>().KeyName)) // 해당 문의 door class의 KeyName이 인벤토리에 있는지 확인
                {
                    obj.transform.Rotate(0, 90, 0); //Y 90도 회전
                    obj.GetComponent<Collider>().enabled = false;
                    ManagerObject.instance.eventManager.OnPlayAudioClip(ManagerObject.instance.resourceManager.soundsmap[SoundsEnum.doorsound].Result, 0.3f, false);
                }
                else
                {
                    ManagerObject.instance.eventManager.OnRequireKey();
                }
            }
            else if (type == ItemType.Key)
            {
                ManagerObject.instance.eventManager.OnGetItem(obj);
                ManagerObject.instance.eventManager.OnPlayAudioClip(ManagerObject.instance.resourceManager.soundsmap[SoundsEnum.papersound].Result, 0.3f, false);
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
