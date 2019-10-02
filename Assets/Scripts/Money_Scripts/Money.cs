using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public Money_Master moneyMaster;
    //money variable
    public int money = 100;
    // maximum value of money attainable (also will double currently as *Win* value)
    public int maxMoney = 10000;
    // amount of increase each tick
    public int increaseValue = 20;


    public void OnEnable()
    {
        moneyMaster.MoneyEvent += MoneyChange;
    }

    public void OnDisable()
    {
        moneyMaster.MoneyEvent -= MoneyChange;
    }


    public void FixedUpdate()
    {
        //this is called every tick, per the proccessor speed that is across all systems...
        moneyMaster.EventMoneyChanged();
    }

    private void MoneyChange()
    {
        // set the money value to money plus the increase value, if its not already at the max value...
        money = money >= maxMoney ? money = maxMoney : money += increaseValue;

        if (money >= maxMoney)
        {
            Debug.Log("Hey, You won the game");
        }
    }
}
