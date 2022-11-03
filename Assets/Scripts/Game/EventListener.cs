using System;
using UnityEngine;

public static class EventListener
{
    public static void OnItemGet()
    {
        Debug.Log("Item Getted at " + DateTime.Now);
        GameDependencies.instance._vfxManager.ItemsWhoosh();
    }

    public static void OnItemSend()
    {
        Debug.Log("Item Sended at " + DateTime.Now);
        GameDependencies.instance._vfxManager.ItemsWhoosh();
    }

    public static void OnMoneyEarned() 
    {
        GameDependencies.instance._vfxManager.MoneyEarn();
     }

    public static void OnMoneyGenerated() 
    {
        GameDependencies.instance._vfxManager.MoneyGenerate();
     }

    public static void OnMoneySpended() { }

}