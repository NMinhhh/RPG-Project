using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionEffect : MonoBehaviour
{
    [SerializeField] private float transTimer;
    [SerializeField] private Image image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTransition()
    {
        image.color = new Color(0, 0, 0, 0);
        image.gameObject.SetActive(true);
        LeanTween.value(0, 1, transTimer).setOnUpdate((value) =>
        {
            image.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), value);
        });
    }

    public void StopTransition()
    {
        image.color = new Color(0, 0, 0, 1);
        image.gameObject.SetActive(true);
        LeanTween.value(0, 1, transTimer).setOnUpdate((value) =>
        {
            image.color = Color.Lerp(new Color(0, 0, 0, 1), new Color(0, 0, 0, 0), value);
        });
    }

    public void ImageDisable()
    {
        image.gameObject.SetActive(false);
    }
}
