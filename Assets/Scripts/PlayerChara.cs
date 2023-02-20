using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChara : MonoBehaviour
{
    #region BasicStats
    public float hpCurrent = 100f;
    public float hpBase = 100f;
    public float hpMax;

    public float spCurrent = 50f;
    public float spBase = 50f;
    public float spMax;
    
    private bool spRegenerating = false;

    #endregion

    #region MoveCost
    [SerializeField] public float jumpCost = 10f;
    [SerializeField] public float dashCost = 25f;
    #endregion

    #region Grade Stats

    public float pieces = 0f;
    public float piecesToGrade = 1f;
    public float hpPerc = 1f;
    public float spPerc = 1f;
    

    #endregion

    public bool deacrese = false;

    public void getPieces(float amount)
    {
        pieces += amount;
    }
    
    public bool canJump()
    {
        if (spCurrent >= jumpCost)
            return true;
        else
            return false;
    }

    public bool canDash()
    {
        if (spCurrent >= dashCost)
            return true;
        else
            return false;
    }

    void Start()
    {
        hpMax = hpBase * hpPerc;
        spMax = spBase * spPerc;

        hpCurrent = hpMax;
        spCurrent = spMax;
    }
    

    private IEnumerator RegenSP()
    {
        spRegenerating = true;
        yield return new WaitForSeconds(3f);
        while (true)
        {
            if (spCurrent < spMax)
            {
                spCurrent += 4;
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                spRegenerating = false;
                yield return null;
                break;
            }
        }
    }

    void Update()
    {
        if (spCurrent < 0)
            spCurrent = 0;
        if (spCurrent < spMax && !spRegenerating)
        {
            StartCoroutine(RegenSP());
        }
        if (spCurrent > spMax)
            spCurrent = spMax;
    }
}
