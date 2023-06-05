using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChara : MonoBehaviour
{
    #region BasicStats
    public float hpCurrent = 100f;
    public float hpMax = 100f;

    public float spCurrent = 50f;
    public float spMax = 50f;

    private bool spRegenerating = false;
    private bool enduranceRegenerating = false;
    
    public bool isStunned = false;
    private float stunTime = 1f;
    
    //public int level = 1;

    //public Vector3 resPos;
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
    /*
    #region Grade Stats
    public float pieces = 0f;
    public float piecesToGrade = 10f;
    public float hpPerc = 1f;
    public float spPerc = 1f;
    #endregion
    */
    public PlayerClass player;
    public bool deacrese = false;
    public Camera cam;
    public CameraFollow camSRC;
    private Animator animCont;
    
    public void takeDmg(float dmg)
    {
        if (fight.isBlocking && !fight.isParry)
        {
            spCurrent -= dmg;
        }
        else if (fight.isBlocking && fight.isParry)
        {
            animCont.SetTrigger("Parry");
            fight.isBlocking = false;
            StopCoroutine(fight.CanParry());
        }
        else if(isStunned)
        {
            StopCoroutine(Stun());
            isStunned = false;
            hpCurrent -= 1.25f * dmg;
        }
        else
        {
            animCont.SetTrigger("Hurt");
            hpCurrent -= dmg;
        }
        fight.isRecentlyHit = true;
    }
    
    public void getPieces(float amount)
    {
        var target = player.pieces + amount;
        player.pieces = Mathf.Lerp(player.pieces, target, 1);
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
        player = GetComponent<PlayerClass>();
        animCont = GetComponent<Animator>();
        cam = Camera.main;
        camSRC = cam.GetComponent<CameraFollow>();
        levelCreateSRC = GameObject.FindWithTag("lvlScr").GetComponent<LevelCreate>();
        RespawnPoint = GameObject.FindWithTag("playerPos");
        fight = GetComponent<FightBehaviour>();
        assignStats();
    }

    public void RespawnPointAssign(GameObject pos)
    {
        RespawnPoint = pos;
    }

    public void assignStats()
    {
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
        player.pieces = 0f;
        gameObject.transform.position = RespawnPoint.transform.position;
        cam.GetComponent<CameraFollow>().YouDied.SetActive(false);

        levelCreateSRC.GetComponent<LevelCreate>().forEachEnemyFindDestroy();
        levelCreateSRC.GetComponent<LevelCreate>().foreachCycle(levelCreateSRC.GetComponent<LevelCreate>().Enemy, levelCreateSRC.GetComponent<LevelCreate>().EnemyPos);
        isDead = false;
        deathCourotine = false;
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
        if (hpCurrent > hpMax)
        {
            hpCurrent = hpMax;
        }
            
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
