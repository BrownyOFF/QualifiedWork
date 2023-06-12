using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChara : MonoBehaviour
{
    #region BasicStats
    public float hpCurrent = 100f;

    public float spCurrent = 50f;

    private bool spRegenerating = false;
    private bool enduranceRegenerating = false;
    
    public bool isStunned = false;
    private float stunTime = 1f;
    
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
    public PlayerClass player;
    public bool deacrese = false;
    public Camera cam;
    public CameraFollow camSRC;
    private Animator animCont;
    
    public AudioSource blocksfx;
    public AudioSource parrysfx;
    public AudioSource hitSFX;
    
    public bool takeDmg(float dmg, int type)
    {
        if(GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().isDashing && (type == 0 || type == 1))  // if dodging && onlyDodge attack or default attack
            return true;

        if (GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().isDashing && type == 2) // if dodging && onlyParry attack
        {
            hpCurrent -= dmg;
            return false;
        }

        if (fight.isBlocking && type == 0 && !fight.isParry) // if block default attack
        {
            spCurrent -= dmg;
            blocksfx.Play();
            return false;
        }

        if (fight.isParry && (type == 0 || type == 2)) // if parry default or parryOnly attack 
        {
            parrysfx.Play();
            StopCoroutine(fight.CanParry());
            return true;
        }
            
        animCont.SetTrigger("Hurt"); // others
        hpCurrent -= dmg;
        hitSFX.Play();
        return false;
    }
    
    public void getPieces(float amount)
    {
        var target = player.pieces + amount;
        player.pieces += (int)amount;
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
        hpCurrent = player.hpMax;
        spCurrent = player.spMax;
    }
    
    private IEnumerator RegenSP()
    {
        spRegenerating = true;
        yield return new WaitForSeconds(3f);
        while (true)
        {
            if (spCurrent < player.spMax)
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
        deathCourotine = true;
        isDead = true;
        animCont.SetBool("isDead", true);
        cam.GetComponent<CameraFollow>().YouDied.SetActive(true);
        camSRC.BlackScreenTransparency(1);
        animCont.SetBool("isDead", false);
        yield return new WaitForSeconds(3f);
        assignStats();
        player.pieces /= 2;
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
        if (hpCurrent > player.hpMax)
        {
            hpCurrent = player.hpMax;
        }
            
        if (spCurrent < 0)
            spCurrent = 0;
        if (spCurrent < player.spMax && !spRegenerating)
        {
            StartCoroutine(RegenSP());
        }
        if (spCurrent > player.spMax)
            spCurrent = player.spMax;
    }
}
