using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticePickUp : MonoBehaviour, IPooledObject
{
    [SerializeField] private Image bg;
    [SerializeField] private Image icon;
    [SerializeField] private Text text;
    [SerializeField] private float appearTime;
    [SerializeField] private float disappearTime;



    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(appearTime);
        float timer = 0;
        while (timer <= disappearTime)
        {
            timer += Time.deltaTime;
            bg.color = Color.Lerp(bg.color, new Color(0, 0, 0, 0), timer / disappearTime);
            icon.color = Color.Lerp(icon.color, new Color(1, 1, 1, 0), timer / disappearTime);
            text.color = Color.Lerp(text.color, new Color(1, 1, 1, 0), timer / disappearTime);
            if (icon.color.a <= 0)
                break;
            yield return null;
        }
        ObjectPool.Instance.AddInPool(Pool.Type.NoticePickUp, this.gameObject);
    }

    public void SetInfo(Sprite icon, string text)
    {
        this.icon.sprite = icon;
        this.text.text = text;
        StartCoroutine(Disappear());
    }

    public void OnObjectSpawn()
    {
        this.bg.color = new Color(0, 0, 0, .5f);
        this.icon.color = new Color(1, 1, 1, 1);
        this.text.color = new Color(1, 1, 1, 1);
    }
}
