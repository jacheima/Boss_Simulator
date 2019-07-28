using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateNPC : NeedsManager
{


    public void UpdateNeeds(ItemData itemData)
    {
        //if the happiness effect is not 0
        if (itemData.HappinessEffect != 0)
        {
            AddHappiness(itemData.HappinessEffect);
        }
        

        //if the productivity effect is not 0
        if (itemData.ProductivityEffect != 0)
        {
            AddProductivity(itemData.ProductivityEffect);
        }
        

        //if the social effect is not 0
        if (itemData.SociallEffect != 0)
        {
           AddSocial(itemData.SociallEffect);
        }
        

        //if the stress effect is not 0
        
    }
}
