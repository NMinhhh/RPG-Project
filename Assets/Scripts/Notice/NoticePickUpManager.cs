using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticePickUpManager : Singleton<NoticePickUpManager>
{
    [SerializeField] private Transform noticeHolder;

    public void Notice(Sprite image, string text)
    {
        GameObject notice = ObjectPool.Instance.SpawnFromPool(Pool.Type.NoticePickUp, noticeHolder.position, Quaternion.identity);
        notice.transform.SetParent(noticeHolder.transform);
        notice.GetComponent<NoticePickUp>().SetInfo(image, text);
    }
}
