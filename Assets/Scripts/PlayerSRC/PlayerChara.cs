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

    public int level = 1;

    public GameObject RespawnPoint;
    private LevelCreate levelCreateSRC;

    public bool isDead = false;

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

    public bool deacrese = false;
    public Camera cam;
    

    public void takeDmg(float dmg)
    {
        hpCurrent -= dmg;
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
        cam = Camera.main;
        levelCreateSRC = GameObject.FindWithTag("lvlScr").GetComponent<LevelCreate>();
        CalcStats();
        assignStats();
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

    public IEnumerator Death()
    {
        isDead = true;
        cam.GetComponent<CameraFollow>().YouDied.SetActive(true);
        yield return new WaitForSeconds(3f);
        assignStats();
        pieces = 0f;
        gameObject.transform.position = RespawnPoint.transform.position;
        cam.GetComponent<CameraFollow>().YouDied.SetActive(false);

        levelCreateSRC.GetComponent<LevelCreate>().forEachEnemyFindDestroy();
        levelCreateSRC.GetComponent<LevelCreate>().foreachCycle(levelCreateSRC.GetComponent<LevelCreate>().Enemy, levelCreateSRC.GetComponent<LevelCreate>().EnemyPos);
        isDead = false;
    }
    
    void Update()
    {
        if (hpCurrent <= 0)
        {
            StartCoroutine(Death());
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
