using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private PlayerChara chara;
    private PlayerMovement move;
    private FightBehaviour fight;
    public int flowerAmount;
    public int shardAmount;
    public int quickCurrent = 0;
    public List<int> inv;
    public List<int> amount;
    public List<int> quickSlots;
    public TextAsset itemsList;
    private string itemsListString;
    private int flaskMax = 2;
    
    void Start()
    {
        chara = GetComponent<PlayerChara>();
        move = GetComponent<PlayerMovement>();
        fight = GetComponent<FightBehaviour>();
        itemsListString = itemsList.text;
        inv.Add(3);
        amount.Add(flaskMax);
        quickSlots.Add(inv[0]);
    }
    public void TakeItem(int id)
    {
        if (inv.Contains(id))
        {
            int i = inv.IndexOf(id);
            amount[i]++;
        }else
        {
            inv.Add(id);
            amount.Add(1);
        }
    }

    public void UseItem(int id)
    {
        var tmpID = inv.IndexOf(id);
        if (id == 3 && amount[tmpID] > 0)
        {
            chara.hpCurrent += 25;
            amount[tmpID]--;
        }
    }

    public void ResetFlask()
    {
        amount[inv.IndexOf(3)] = flaskMax;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log(inv);
        }
        else if (Input.GetKeyDown(KeyCode.Q) && !fight.isAttacking && !fight.isBlocking)
        {
            UseItem(quickSlots[quickCurrent]);
        }
    }
}