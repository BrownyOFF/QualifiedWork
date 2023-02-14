using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChara : MonoBehaviour
{
    #region BasicStats
    [SerializeField] public float hp = 100f;
    [SerializeField] public float hpMax = 100f;

    [SerializeField] public float sp = 50f;
    [SerializeField] public float spMax = 50f;

    private bool spRegenerating = false;

    #endregion

    #region MoveCost
    [SerializeField] public float jumpCost = 10f;
    [SerializeField] public float dashCost = 25f;
    #endregion

    public bool deacrese = false;

    public bool canJump()
    {
        if (sp >= jumpCost)
            return true;
        else
            return false;
    }

    public bool canDash()
    {
        if (sp >= dashCost)
            return true;
        else
            return false;
    }

    void Start()
    {
    }

    private IEnumerator RegenSP()
    {
        spRegenerating = true;
        yield return new WaitForSeconds(3f);
        while (true)
        {
            if (sp < spMax)
            {
                sp += 4;
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
        if (sp < 0)
            sp = 0;
        if (sp < spMax && !spRegenerating)
        {
            StartCoroutine(RegenSP());
        }
        if (sp > spMax)
            sp = spMax;
    }
}
