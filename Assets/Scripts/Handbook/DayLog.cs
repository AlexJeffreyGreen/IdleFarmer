using Assets.Scripts.Handbook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayLog : MonoBehaviour
{
    [SerializeField] private DayEntry LeftEntry;
    [SerializeField] private DayEntry RightEntry;
    [SerializeField] private Button LeftEntryButton;
    [SerializeField] private Button RightEntryButton;

    public LinkedList<DayLogItem> listOfDays = new LinkedList<DayLogItem>();

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NavigationClick()
    {
        GameObject gO = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        Debug.Log(gO.name);
        //if(gO.name == "Previous")
        //{
        //    LinkedListNode<DayLogItem> LeftItem = LeftEntry.CurrentNode;
        //    LinkedListNode<DayLogItem> RightItem = RightEntry.CurrentNode;

        //    LeftItem = LeftItem.Previous;
        //    RightItem = LeftItem;
        //    LeftItem = LeftItem.Previous;

        //    LeftEntry.UpdateEntry(LeftItem);
        //    RightEntry.UpdateEntry(RightItem);

        //}
        //else
        //{
        //    LinkedListNode<DayLogItem> LeftItem = LeftEntry.CurrentNode;
        //    LinkedListNode<DayLogItem> RightItem = RightEntry.CurrentNode;

        //    RightItem = RightItem.Next;
        //    LeftItem = RightItem.Previous;
        //    RightItem = RightItem.Next;
        //}
        //Debug.Log($"{this.GetComponentInChildren<Button>().name}");
    }
}
