using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct StaminaValue
{
    public int whole;
    public int remainder;
}

public class StaminaBarManager : MonoBehaviour
{
    public int MaxSum;
    public int CurrentSumOfStamina;
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
                AddStaminaBarNode(false);
            }
            this.CurrentSumOfStamina = this.GetCurrentSum();
            this.MaxSum = this.GetMaxSum();
        }
        
        this.RecalculateTextProperty();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RecalculateTextProperty()
    {
        this.GetComponentInChildren<Text>().text = this.CurrentSumOfStamina.ToString("D3") + "/" + this.MaxSum.ToString("D3");
    }
    
    public void EvaluateNode(int amount, out int remainder, LinkedListNode<StaminaBarNode> currentNode = null)
    {
        if (currentNode == null)
        {
            currentNode = this.GetLastAliveNode();
        }
        //int currentStamina = currentNode.CurrentStamina;
        if (((currentNode.Value.CurrentStamina == currentNode.Value.MaxStamina)
            || (currentNode.Value.CurrentStamina == currentNode.Value.MinStamina))
            && (amount > 0 && currentNode.Next == null))
        {
            remainder = 0;
            Debug.Log("Cannot add due to being at max value");
            return;
        }
        else
            remainder = amount;

        int newAmount = amount + currentNode.Value.CurrentStamina;
        int absVal = 0;
        
        if (newAmount >= currentNode.Value.MaxStamina)
            remainder = Mathf.Abs(newAmount + currentNode.Value.MaxStamina);
        else if (newAmount <= currentNode.Value.MinStamina)
            remainder = -Mathf.Abs(newAmount + currentNode.Value.MinStamina);
        remainder = 0;
        
        currentNode.Value.UpdateStamina(newAmount);
        this.UpdateNode(newAmount, currentNode);
        
        Debug.Log($"Remainder is : {remainder}");
        
        //currentNode.Value.UpdateStamina(remainder);

    }

    public StaminaValue CalcWholeAndPart(int total)
    {
        StaminaValue v;
        v.whole = total / this.MainNode.MaxStamina;
        v.remainder = total / this.MainNode.MaxStamina;
        return v;
    }

    //Keep
    private void AddStaminaBarNode(bool isDirty)
    {
        int position = this.StaminaBar.Count;
        StaminaBarNode node = Instantiate<StaminaBarNode>(MainNode, new Vector3(this.transform.position.x + position * Spacing, this.transform.position.y, 0), Quaternion.identity);
        node.transform.SetParent(this.transform);
        StaminaBar.AddLast(node);
        if(isDirty)
        {
            CurrentSumOfStamina = this.GetCurrentSum();
            MaxSum = this.GetMaxSum();
        }
    }

    //Keep
    private int GetCurrentSum()
    {
        int returnValue = 0;

        LinkedListNode<StaminaBarNode> currentNode = this.StaminaBar.First;

        while(currentNode != null)
        {
            returnValue += currentNode.Value.CurrentStamina;
            currentNode = currentNode.Next;
        }

        return returnValue;
    }
    //Keep
    private int GetMaxSum()
    {
        int returnValue = 0;

        LinkedListNode<StaminaBarNode> currentNode = this.StaminaBar.First;

        while(currentNode != null)
        {
            returnValue += currentNode.Value.MaxStamina;
            currentNode = currentNode.Next;
        }

        return returnValue;
    }


    public void AddStamina(int amountToAdd) //250
    {
        //StaminaValue v = this.CalcWholeAndPart(amountToAdd);
        
        #region tried stuff
        //int remainder = amountToAdd;
        //while(remainder > 0)
        //{
        //    LinkedListNode<StaminaBarNode> node = this.GetLastAliveNode();
        //    int max = Mathf.Min(amountToAdd, node.Value.MaxStamina); //250, 100
        //    remainder = amountToAdd - max;
        //    node.Value.UpdateStamina(max);
        //}

        //int wholeNodes = amountToAdd / MainNode.MaxStamina; //2 100
        //int lastNodeValue = amountToAdd % MainNode.MaxStamina; //1 50
        //int totalNodes = this.StaminaBar.Count;

        //if(amountToAdd > this.MaxSum)
        //{

        //}

        ////int SumOfNodes = this.CurrentSumOfStamina;
        ////int Max = this.MaxSum;

        ////SumOfNodes += amountToAdd;
        ////while(remainder > 0)
        ////{
        //LinkedListNode<StaminaBarNode> lastAliveNode = this.GetLastAliveNode();
        //if(lastAliveNode == null)
        //    return;

        //for(int i = 0; i < wholeNodes; i++) { }
        //for(int i = 0; i < lastNodeValue; i++) { }
        //    //UpdateNode(MainNode.MaxStamina, lastAliveNode);

        ////find the value you can add to the node
        ////store the remainder as the remainder int
        ////jump to next node.
        //UpdateNode(amountToAdd, lastAliveNode);
        ////}
        #endregion

        int i = 0;
        this.EvaluateNode(amountToAdd, out i);
    }

    public void RemoveStamina(int amountToRemove)
    {
        int i = amountToRemove;
        while (i > 0)
        {
            this.EvaluateNode(i, out i);
        }
        
        
        
        //LinkedListNode<StaminaBarNode> lastAliveNode = this.GetLastAliveNode();
        //if(lastAliveNode == null)
        //    return;
        //UpdateNode(-amountToRemove, lastAliveNode);
    }

    public void UpdateNode(int value, LinkedListNode<StaminaBarNode> currentNode)
    {
        //TODO - don't change sprite IF sprite is already the same sprite!
        currentNode.Value.UpdateStamina(value);

        //double percentage = (currentNode.Value.RetrieveStamina() / 100);
        //Debug.Log(percentage);
        Sprite selectedSprite = null;

        int percentage = currentNode.Value.CurrentStamina;

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

        if(currentNode.Value.GetComponent<Image>().sprite == selectedSprite)
            return;

        currentNode.Value.GetComponent<Image>().sprite = selectedSprite;
    }

    //Keep
    public LinkedListNode<StaminaBarNode> GetLastAliveNode(LinkedListNode<StaminaBarNode> previousNode = null)
    {
        LinkedListNode<StaminaBarNode> currentNode = previousNode;

        if(currentNode == null)
            currentNode = this.StaminaBar.First;

        if(currentNode.Value.CurrentStamina == this.MainNode.MaxStamina)
        {
            if(currentNode.Next == null)
                return currentNode;
            else
                return GetLastAliveNode(currentNode.Next);
        }
        else
            return currentNode;
        //if(previousNode == null)
        //{
        //    returnNode = this.StaminaBar.First;
        //}
        //else
        //    returnNode = previousNode;

        //if(returnNode.Value.CurrentStamina > 0)
        //{
        //    if(returnNode.Value.CurrentStamina == returnNode.Value.MaxStamina)
        //        return null;
        //    else
        //        returnNode = this.GetLastAliveNode(returnNode.Next);
        //}
        //else
        //{
        //    returnNode = this.GetLastAliveNode(returnNode.Next);
        //}

        ////if(returnNode.Value.RetrieveStamina() > 0)
        ////{
        ////    if(returnNode.Value.RetrieveStamina() == returnNode.Value.MaxStamina)
        ////    {
        ////        if(returnNode.Next == null)
        ////            return null;
        ////        else
        ////            return returnNode.Next;
        ////    }
        ////    return returnNode;
        ////}

        ////else
        ////    returnNode = this.GetLastAliveNode(returnNode.Previous);

        //return returnNode;
    }
}
