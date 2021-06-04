using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FarmersHandbook : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button[] uiButtons;
    [SerializeField] private RectTransform[] mainUiTransforms;
    [SerializeField] private Button handbookButton;
    private RectTransform lastOpenForm;
    void Start()
    {
        foreach (RectTransform _rectTransforms in mainUiTransforms)
        {
            //Image i = _rectTransforms.GetComponent<Image>();
            //i.enabled = false;
            _rectTransforms.gameObject.SetActive(false);
        }

        foreach (Button _button in uiButtons)
        {
            _button.enabled = false;
        }
    }

    public void OnFarmersHandbookClick()
    {
        if (lastOpenForm == null)
            lastOpenForm = this.mainUiTransforms.First();
        
        bool isActive = lastOpenForm.gameObject.activeSelf;
        
        lastOpenForm.gameObject.SetActive(!isActive);
        
        foreach (Button _button in uiButtons)
        {
            _button.enabled = !isActive;
        }
        
        Debug.Log("Main handbook clicked.");
    }

    public void OnInventoryButtonClick()
    {
        this.ResetUI(this.mainUiTransforms[0]);
        Debug.Log("Inventory clicked.");
    }

    public void OnDayLogClick()
    {
        this.ResetUI(this.mainUiTransforms[1]);
        Debug.Log("Day log clicked.");
    }

    public void OnCompendiumClick()
    {
        this.ResetUI(this.mainUiTransforms[2]);
        Debug.Log("Compendium clicked.");
    }

    void ResetUI(RectTransform _newMainForm)
    {
        this.lastOpenForm = _newMainForm;
        foreach(RectTransform t in this.mainUiTransforms)
            t.gameObject.SetActive(false);
        this.lastOpenForm.gameObject.SetActive(true);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
