using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using DG.Tweening;

public class ToastScript
{

    static Transform s_canvas = null;

    static long beforeTime = 0;

    public static void show(string text)
    {
        try
        {
            if (s_canvas == null)
            {
                s_canvas = GameObject.Find("Canvas").transform;
            }

            GameObject prefab = Resources.Load("Prefabs/Commons/Toast") as GameObject;
            GameObject obj = GameObject.Instantiate(prefab, s_canvas);
            obj.transform.Find("Text").GetComponent<Text>().text = text;

            // 立即刷新Text尺寸
            //LayoutRebuilder.ForceRebuildLayoutImmediate(obj.transform.Find("Text").GetComponent<RectTransform>());
            //obj.GetComponent<RectTransform>().sizeDelta = new Vector2(obj.transform.Find("Text").GetComponent<RectTransform>().sizeDelta.x + 50, 80);

            long nowTime = CommonUtil.getTimeStamp_Millisecond();
            if ((nowTime - beforeTime) < 200)
            {
                obj.transform.localPosition = new Vector3(0, -(nowTime - beforeTime) - 100, 0);
                obj.transform.DOLocalMove(new Vector3(0, 300, 0), 1.3f).OnComplete(() =>
                {
                    GameObject.Destroy(obj);
                });
            }
            else
            {
                obj.transform.DOLocalMove(new Vector3(0, 300, 0), 1).OnComplete(() =>
                {
                    GameObject.Destroy(obj);
                });
            }
            beforeTime = nowTime;
        }
        catch (Exception exp)
        {
            Debug.Log("ToastScript.show异常：" + exp.ToString());
        }
    }
}
