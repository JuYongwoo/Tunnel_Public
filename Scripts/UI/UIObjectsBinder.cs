using System.Collections.Generic;
using System;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIObjectsBinder<T> : MonoBehaviour where T : Enum
{
    protected Dictionary<T, Transform> ObjectsMap;
    protected abstract Dictionary<T, UnityAction> GetHandlers();

    protected virtual void Awake()
    {
        ObjectsMap = new Dictionary<T, Transform>(); //맵 초기화

        var ObjTransform = GetComponentsInChildren<Transform>(true); //자식 오브젝트가 가진 컴포넌트 중 tranform(모든 obj가 가지고있음)
        foreach (var btn in ObjTransform)
        {
            if (Enum.TryParse(typeof(T), btn.gameObject.name, out object result)) //그 중에서 Enum 내의 이름과 gameobject.name이 일치하면
            {
                ObjectsMap[(T)result] = btn; //맵에 등록
            }
        }

        foreach (var pair in GetHandlers())
        {
            if (ObjectsMap.TryGetValue(pair.Key, out var obj))
                 obj.GetComponent<Button>().onClick.AddListener(pair.Value);
            else
                Debug.LogWarning($"[UIButtonBinder] Button '{pair.Key}' not found");
        }
    }
}
