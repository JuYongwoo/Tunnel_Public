using System.Collections.Generic;
using UnityEngine;

public class InGameAimPanel : MonoBehaviour
{

    private enum InGameAimPanelObjs
    {
        AimText
    }

    private Dictionary<InGameAimPanelObjs, GameObject> InGameAimPanelObjsMap;

    private void Awake()
    {

        InGameAimPanelObjsMap = Util.MapEnumChildObjects<InGameAimPanelObjs, GameObject>(gameObject);
        InGameAimPanelObjsMap[InGameAimPanelObjs.AimText].SetActive(true);
    }
}
