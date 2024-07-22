using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : Singleton<IntroManager>
{
    [SerializeField] private List<Intro> intros;

    private GameObject currentIntro;

    public bool isPlayIntro {  get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

  

    public void PlayCutSceen(Intro.IntroType type)
    {
        isPlayIntro = true;
        currentIntro = GetIntro(type);
        currentIntro.SetActive(true);
    }

    public void FinishIntro()
    {
        isPlayIntro = false;
        currentIntro.SetActive(false);
    }

    public GameObject GetIntro(Intro.IntroType type)
    {
        foreach (Intro intro in intros)
        {
            if (intro.type == type)
            {
                return intro.cutsceenObject;
            }
        }
        return null;
    }
}

[System.Serializable]
public class Intro
{
    public enum IntroType
    {
        OrkIntro,
    }

    public IntroType type;
    public GameObject cutsceenObject;
}