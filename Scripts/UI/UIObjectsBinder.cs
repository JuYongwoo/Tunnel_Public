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
        ObjectsMap = new Dictionary<T, Transform>(); //�� �ʱ�ȭ

        var ObjTransform = GetComponentsInChildren<Transform>(true); //�ڽ� ������Ʈ�� ���� ������Ʈ �� tranform(��� obj�� ����������)
        foreach (var btn in ObjTransform)
        {
            if (Enum.TryParse(typeof(T), btn.gameObject.name, out object result)) //�� �߿��� Enum ���� �̸��� gameobject.name�� ��ġ�ϸ�
            {
                ObjectsMap[(T)result] = btn; //�ʿ� ���
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
