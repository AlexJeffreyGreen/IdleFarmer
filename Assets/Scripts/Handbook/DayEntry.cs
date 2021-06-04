using Assets.Scripts.Handbook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayEntry : MonoBehaviour
{
    [SerializeField] private Text DayLabel;
    private LinkedListNode<DayLogItem> currentNode;
    public LinkedListNode<DayLogItem> CurrentNode { get { return currentNode; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateEntry(LinkedListNode<DayLogItem> item)
    {
        currentNode = item;
        DayLabel.text = $"Day {item.Value.DayCount}";
    }
}
