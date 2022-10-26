using System;
using UnityEngine;

public static class EventListener
{
    public static void OnItemGet()
    {
        Debug.Log("Item Getted at " + DateTime.Now);
    }

    public static void OnItemSend()
    {
        Debug.Log("Item Sended at " + DateTime.Now);
    }

    public static void OnMoneyEarned() { }

    public static void OnMoneySpended() { }

}