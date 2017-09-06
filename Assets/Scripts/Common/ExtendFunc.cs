//================================
//===Author: easy
//===Email: gopiny@live.com
//===Date: 2017-09-06 10:29
//================================

using UnityEngine;
public static class ExtendFunc
{
    public static void DestroyChild(this GameObject obj)
    {
        var transform = obj.transform;
        while (transform.childCount != 0)
        {
            var child = transform.GetChild(0).gameObject;
            Object.Destroy(child);
        }
    }
}
