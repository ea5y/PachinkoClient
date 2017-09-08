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
        int counter = 0;
        while (transform.childCount != 0)
        {
            if (counter > 10)
            {
                Debug.Log("Fail");
                break;
            }
            var child = transform.GetChild(0).gameObject;
            //Object.Destroy(child);
            //GameObject.Destroy(child);
            //GameObject.DestroyObject(child);
            GameObject.DestroyImmediate(child);
            counter++;
        }
    }
}
