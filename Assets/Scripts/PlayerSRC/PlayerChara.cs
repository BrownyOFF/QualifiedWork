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

    public float enduranceMax = 50f;
    public float enduranceCurrent = 0f;
    
    private bool spRegenerating = false;
    private bool enduranceRegenerating = false;
    
    public bool isStunned = false;
    private float stunTime = 1f;
    
    public int level = 1;

    public GameObject RespawnPoint;
    private LevelCreate levelCreateSRC;
    private FightBehaviour fight;

    public bool isDead = false;
    public bool deathCourotine = false;

    public bool inDialogue = false;

    #endregion

    #region MoveCost
    [SerializeField] public float jumpCost = 10f;
    [SerializeField] public float dashCost = 25f;
    #endregion

    #region Grade Stats

    public float pieces = 0f;
    public float piecesToGrade = 10f;
    public float hpPerc = 1f;
    public float spPerc = 1f;
    

    #endregion

    #region Items
    public int flowerAmount; // 0 code
    public int shardAmount;  // 1 code
    #endregion
    
    public bool deacrese = false;
    public Camera cam;
    public CameraFollow camSRC;
    public GameObject enduranceBar;
    private float barScaleCurrent;
    private float barScaleMax;
    private float scaleDiff;
    private Animator animCont;


    public void TakeItem(int type)
    {
        if (type == 0)
            flowerAmount++;
        else
            shardAmount++;
    }
    
    public void takeDmg(float dmg)
    {
        if (fight.isBlocking && !fight.isParry)
        {
            enduranceCurrent += dmg;
            Debug.Log("Endurance damage: " +dmg);
            if (enduranceCurrent >= enduranceMax)
            {
                StartCoroutine(Stun());
            }
        }
        else if (fight.isBlocking && fight.isParry)
        {
            animCont.SetTrigger("Parry");
            fight.isBlocking = false;
            StopCoroutine(fight.CanParry());
            enduranceCurrent += dmg / 2;
        }
        else if(isStunned)
        {
            StopCoroutine(Stun());
            isStunned = false;
            enduranceCurrent = 0;
            hpCurrent -= 1.25f * dmg;
        }
        else
        {
            animCont.SetTrigger("Hurt");
            hpCurrent -= dmg;
            enduranceCurrent += dmg / 4;
        }

        fight.isRecentlyHit = true;
    }
    
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
        animCont = GetComponent<Animator>();
        cam = Camera.main;
        camSRC = cam.GetComponent<CameraFollow>();
        levelCreateSRC = GameObject.FindWithTag("lvlScr").GetComponent<LevelCreate>();
        RespawnPoint = GameObject.FindWithTag("playerPos");
        fight = GetComponent<FightBehaviour>();
        CalcStats();
        assignStats();
        var bar = GameObject.Find("PlayerEnduranceBar").gameObject;
        enduranceBar = bar.transform.GetChild(1).gameObject;
        barScaleMax = enduranceBar.GetComponent<RectTransform>().localScale.x;
        scaleDiff = enduranceMax / barScaleMax;
    }

    private void BarRenderer()
    {
        barScaleCurrent = enduranceCurrent / scaleDiff;
        if (barScaleCurrent > barScaleMax)
            barScaleCurrent = barScaleMax;
        enduranceBar.GetComponent<RectTransform>().localScale = new Vector3(barScaleCurrent,enduranceBar.GetComponent<RectTransform>().localScale.y,enduranceBar.GetComponent<RectTransform>().localScale.z);
    }

    public void RespawnPointAssign(GameObject pos)
    {
        RespawnPoint = pos;
    }
    
    public void CalcStats()
    {
        hpMax = hpBase * hpPerc;
        spMax = spBase * spPerc;
    }
    
    public void assignStats()
    {
        hpCurrent = hpMax;
        spCurrent = spMax;
        enduranceCurrent = 0f;
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
                yield return new WaitForSeconds(0.3f);
            }
            else
            {
                spRegenerating = false;
                yield return null;
                break;
            }
        }
    }

    private IEnumerator RegenEndurance()
    {
        while (true)
        {
            if (enduranceCurrent > 0)
            {
                enduranceCurrent -= 5;
                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                enduranceRegenerating = false;
                enduranceCurrent = 0;
                break;
            }
        }
    }
    public IEnumerator Death()
    {
        animCont.SetBool("isDead", true);
        deathCourotine = true;
        isDead = true;
        cam.GetComponent<CameraFollow>().YouDied.SetActive(true);
        camSRC.BlackScreenTransparency(1);
        animCont.SetBool("isDead", false);
        yield return new WaitForSeconds(3f);
        assignStats();
        pieces = 0f;
        gameObject.transform.position = RespawnPoint.transform.position;
        cam.GetComponent<CameraFollow>().YouDied.SetActive(false);

        levelCreateSRC.GetComponent<LevelCreate>().forEachEnemyFindDestroy();
        levelCreateSRC.GetComponent<LevelCreate>().foreachCycle(levelCreateSRC.GetComponent<LevelCreate>().Enemy, levelCreateSRC.GetComponent<LevelCreate>().EnemyPos);
        isDead = false;

        camSRC.BlackScreenTransparency(0);
    }

    private IEnumerator Stun()
    {
        isStunned = true;
        yield return new WaitForSeconds(stunTime);
        isStunned = false;
    }
    
    
    void Update()
    {
        if (hpCurrent <= 0 && !deathCourotine)
        {
            StartCoroutine(Death());
        }

        BarRenderer();
            
        if (spCurrent < 0)
            spCurrent = 0;
        if (spCurrent < spMax && !spRegenerating)
        {
            StartCoroutine(RegenSP());
        }
        if (spCurrent > spMax)
            spCurrent = spMax;

        if (enduranceCurrent > 0 && !fight.isRecentlyHit && !enduranceRegenerating)
        {
            enduranceRegenerating = true;
            StartCoroutine(RegenEndurance());
        }
    }
}
