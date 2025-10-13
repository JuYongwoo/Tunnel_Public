using System;
using System.Collections;
using System.Reflection;

static class DeepCloneHelper
{
    static readonly BindingFlags BF = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

    public static object Clone(object src)
    {
        if (src == null) return null;

        var t = src.GetType();

        // UnityEngine.Object �迭�� �������� �ʰ� ���� ����
        if (typeof(UnityEngine.Object).IsAssignableFrom(t)) return src;

        // �� ����/���ڿ�/decimal �� �Һ� ��ü�� �״�� ��ȯ
        if (t.IsValueType || t == typeof(string)) return src;

        // �迭
        if (t.IsArray)
        {
            var arr = (Array)src;
            var elemType = t.GetElementType();
            var cloned = Array.CreateInstance(elemType, arr.Length);
            for (int i = 0; i < arr.Length; i++)
                cloned.SetValue(Clone(arr.GetValue(i)), i);
            return cloned;
        }

        // IList (List<T> ��)
        if (typeof(IList).IsAssignableFrom(t))
        {
            var list = (IList)src;
            var cloned = (IList)Activator.CreateInstance(t);
            foreach (var item in list) cloned.Add(Clone(item));
            return cloned;
        }

        // IDictionary (Dictionary<K,V> ��)
        if (typeof(IDictionary).IsAssignableFrom(t))
        {
            var dict = (IDictionary)src;
            var cloned = (IDictionary)Activator.CreateInstance(t);
            foreach (DictionaryEntry e in dict) cloned.Add(Clone(e.Key), Clone(e.Value));
            return cloned;
        }

        // ��Ÿ ������: �ʵ� ���� ����
        var inst = Activator.CreateInstance(t, true); // ����� ������ ���
        foreach (var f in t.GetFields(BF))
        {
            // [NonSerialized] �ʵ�� �ǳʶ�
            if (Attribute.IsDefined(f, typeof(NonSerializedAttribute))) continue;

            var value = f.GetValue(src);
            f.SetValue(inst, Clone(value));
        }
        return inst;
    }
}