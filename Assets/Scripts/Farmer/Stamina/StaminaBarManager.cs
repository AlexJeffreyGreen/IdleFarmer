using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarManager : MonoBehaviour
{
    public int StaminaNodeLength;
    public StaminaBarNode MainNode;
    public LinkedList<StaminaBarNode> StaminaBar;// = new LinkedList<StaminaBarNode>();
    public Sprite[] Sprites;
    public float Spacing = 1.0f;
    // Start is called before the first frame update
    void Awake()
    {
        if(StaminaBar == null)
        {
            StaminaBar = new LinkedList<StaminaBarNode>();
            for(int i = 0; i < StaminaNodeLength; i++)
            {
                //node = Instantiate<StaminaBarNode>(MainNode, new Vector3(this.transform.position.x + i * Spacing, this.transform.position.y, 0), Quaternion.identity);

                StaminaBarNode node = null;
                if(i == 0)
                    node = Instantiate<StaminaBarNode>(MainNode, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
                else
                {
                    //TODO - Fix this semi hardcoded nonsense.
                    LinkedListNode<StaminaBarNode> prevNode = this.StaminaBar.Last;
                    node = Instantiate<StaminaBarNode>(MainNode, new Vector3(this.transform.position.x + i * Spacing, this.transform.position.y, 0), Quaternion.identity);
                }

                node.transform.SetParent(this.transform);
                StaminaBar.AddLast(node);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddStamina(int amountToAdd)
    {
        LinkedListNode<StaminaBarNode> lastAliveNode = this.GetLastAliveNode();
        if(lastAliveNode == null)
            return;
        UpdateNode(amountToAdd, lastAliveNode);
    }

    public void RemoveStamina(int amountToRemove)
    {
        LinkedListNode<StaminaBarNode> lastAliveNode = this.GetLastAliveNode();
        if(lastAliveNode == null)
            return;
        UpdateNode(-amountToRemove, lastAliveNode);
    }

    public void UpdateNode(int value, LinkedListNode<StaminaBarNode> currentNode)
    {
        //TODO - don't change sprite IF sprite is already the same sprite!
        currentNode.Value.UpdateStamina(value);

        //double percentage = (currentNode.Value.RetrieveStamina() / 100);
        //Debug.Log(percentage);
        Sprite selectedSprite = null;

        int percentage = currentNode.Value.RetrieveStamina();

        if(percentage > 75)
        {
            selectedSprite = this.Sprites[0];    
        }
        else if(percentage <= 75 && percentage > 50)
        {
            selectedSprite = this.Sprites[1];
        }
        else if(percentage <= 50 && percentage > 25)
        {
            selectedSprite = this.Sprites[2];
        }
        else if(percentage <= 25 && percentage > 0)
        {
            selectedSprite = this.Sprites[3];
        }
        else
        {
            selectedSprite = this.Sprites[4];
        }

        currentNode.Value.GetComponent<Image>().sprite = selectedSprite;
    }

    public LinkedListNode<StaminaBarNode> GetLastAliveNode(LinkedListNode<StaminaBarNode> previousNode = null)
    {
        LinkedListNode<StaminaBarNode> returnNode = null;

        //if(previousNode == null)
        //{
        //    returnNode = this.StaminaBar.Last;
        //}
        //else
        //    returnNode = previousNode;

        //if(returnNode.Value.RetrieveStamina() > 0)
        //{
        //    if(returnNode.Value.RetrieveStamina() == returnNode.Value.MaxStamina)
        //    {
        //        if(returnNode.Next == null)
        //            return null;
        //        else
        //            return returnNode.Next;
        //    }
        //    return returnNode;
        //}

        //else
        //    returnNode = this.GetLastAliveNode(returnNode.Previous);

        return returnNode;
    }
}
