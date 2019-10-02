using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money_Master : MonoBehaviour
{
    public delegate void MoneyEvents();

    public event MoneyEvents MoneyEvent;

    public void EventMoneyChanged()
    {
        MoneyEvent?.Invoke();
    }
}
