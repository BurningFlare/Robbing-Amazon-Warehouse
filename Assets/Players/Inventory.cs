using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<MerchBase> merchList;
    public int totalCost;
    public int totalWeight;

    private void Awake()
    {
        merchList = new List<MerchBase>();
        totalCost = 0;
        totalWeight = 0;
    }

    public void Add(MerchBase merch)
    {
        merchList.Add(merch);
        totalCost += merch.cost;
        totalWeight += merch.weight;
    }

    public override string ToString()
    {
        StringBuilder listString = new StringBuilder(500);
        listString.Append("TotalCost: ");
        listString.Append(totalCost);
        listString.Append(", TotalWeight: ");
        listString.Append(totalWeight);
        if (merchList.Count > 0)
        {
            listString.Append("\nItems:");
            foreach (MerchBase merch in merchList)
            {
                listString.Append("(");
                listString.Append(merch.ToString());
                listString.Append("), ");
            }
            listString.Remove(listString.Length - 2, 2);
        } else
        {
            listString.Append("\nNo Items");
        }
        return listString.ToString();
    }

    public void clear()
    {
        merchList.Clear();
        totalCost = 0;
        totalWeight = 0;
    }
}