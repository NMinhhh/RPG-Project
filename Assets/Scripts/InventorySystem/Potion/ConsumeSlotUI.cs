using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumeSlotUI : MonoBehaviour
{
    [SerializeField] private Text numberText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        EquipSystem.Instance.ChangeNumberOfItem += SetUI;
    }

    private void OnDestroy()
    {
        EquipSystem.Instance.ChangeNumberOfItem -= SetUI;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUI()
    {
        numberText.text = EquipSystem.Instance.numberOfItem+"";
    }

}
