using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RosterManager : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] ArmyManger ArmyManger_Player;
    [SerializeField] ArmyManger ArmyManger_Enemy;
    [SerializeField] Animator animator;
    [SerializeField] Canvas canvas;
    [Header("Input Value")]
    [SerializeField] RosterSide rostertype;

    [Header("Info")]

    //temp
    [SerializeField] Roster rosterinfo;
    //[SerializeField] float moventspeed;
    //[SerializeField] float attackcooldown;
    //[SerializeField] float attackrange;

    float attackcooldown_sp;
    bool attackable_sp;
    [SerializeField] bool landable_sp;

    [Header("Code Support")]
    [SerializeField] float distant;
    [SerializeField] RosterManager target;
    

    private void Awake()
    {
        if (ArmyManger_Player == null)
        {
            ArmyManger[] allArmyManager = FindObjectsOfType<ArmyManger>();
            ArmyManger_Player = allArmyManager.FirstOrDefault(x => x.Get_Rostertype() == rostertype);
            ArmyManger_Enemy = allArmyManager.FirstOrDefault(x => x.Get_Rostertype() != rostertype);
        }
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (canvas == null)
        {
            canvas = GetComponent<Canvas>();
        }
    }
    private void Update()
    {
        Debug.Log(transform.position.y);
        // set up layer oder
        canvas.sortingOrder = Mathf.RoundToInt((transform.position.y - transform.localPosition.y) * 10f * -1);

        // cooldown to attack
        if (attackcooldown_sp < 0)
        {
            attackable_sp = true;
        }
        else
        {
            attackcooldown_sp -= Time.deltaTime;
        }

        // set up flyerland
        if (transform.localPosition.y < 50)
        {
            landable_sp = true;
        }
        else
        {
            landable_sp = false;
        }

        // AI
        target = DetectEnemy();
        if (target == null)
        {
            Stop();
        }
        else
        {
            distant = Vector2.Distance(target.transform.position, transform.position);
            //if (Vector2.Distance(target.GetComponent<RectTransform>().localPosition, GetComponent<RectTransform>().localPosition) < attackrange)
            if (distant < rosterinfo.attackrange)
            {
                Stop();
                Attack(target);
            }
            else
            {
                switch (rosterinfo.rostertype)
                {
                    case (int)ERosterType.Flyer_meele: // flyer meele 
                        switch (target.rosterinfo.rostertype)
                        {
                            case 110001: // land to attack infantry
                                if (distant < 400)
                                {
                                    transform.DOLocalMoveY(0, 4);
                                }
                                else
                                {
                                    transform.DOLocalMoveY(150, 4);
                                }
                                break;
                        }
                        break;
                }
                ChaseEnemy(target);
                //Debug.Log(Vector2.Distance(target.GetComponent<RectTransform>().localPosition, GetComponent<RectTransform>().localPosition));
            }
        }
    }
    // Detect enemy ----
    RosterManager DetectEnemy()
    {
        List<RosterManager> lstall = ArmyManger_Enemy.Get_AllRoster();
        float minDistance = Mathf.Infinity;
        RosterManager minDistance_rostermanager = null;
        if (gameObject.name == "100001-debug")
        {
            for (int i = 0; i < lstall.Count; i++)
            {
                if (lstall[i].rosterinfo.rostertype == (int)ERosterType.Infantry_meele // infantry melee
                    || lstall[i].rosterinfo.rostertype == (int)ERosterType.Infantry_missle // infantry missle
                    || (lstall[i].rosterinfo.rostertype == (int)ERosterType.Flyer_meele && lstall[i].landable_sp)) // flyer melee - land
                {
                    float Distance = Vector2.Distance(lstall[i].transform.position, transform.position);
                    if (Distance < minDistance)
                    {
                        minDistance = Distance;
                        minDistance_rostermanager = lstall[i];
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < lstall.Count; i++)
            {
                if (lstall[i].rosterinfo.rostertype == (int)ERosterType.Infantry_meele // infantry melee
                    || lstall[i].rosterinfo.rostertype == (int)ERosterType.Infantry_missle // infantry missle
                    || (lstall[i].rosterinfo.rostertype == (int)ERosterType.Flyer_meele && lstall[i].landable_sp)) // flyer melee - land
                {
                    float Distance = Vector2.Distance(lstall[i].transform.position, transform.position);
                    if (Distance < minDistance)
                    {
                        minDistance = Distance;
                        minDistance_rostermanager = lstall[i];
                    }
                }
            }
        }
        
        return minDistance_rostermanager;
    }

    // -----------------
    // Chase enemy ----
    void ChaseEnemy(RosterManager roster)
    {
        if (roster != null)
        {
            animator.SetBool("Move", true);
            transform.parent.position = Vector3.MoveTowards(transform.parent.position, roster.transform.parent.position, rosterinfo.moventspeed * Time.deltaTime);
        }
    }
    // -----------------
    // Stop ----
    void Stop()
    {
        animator.SetBool("Move", false);
    }
    // -----------------
    // Attack ----
    void Attack(RosterManager target)
    {
        if (attackable_sp)
        {
            attackable_sp = false;
            animator.SetTrigger("Attack");
            Debug.Log("Attack");
            attackcooldown_sp = rosterinfo.attackcooldown;

            if (rosterinfo.missletype != 0)
            {
                // load image 130001
                GameObject arrow = ObjectPool.Instance.GetfromPool("130001");
                arrow.transform.position = transform.position;
                arrow.GetComponent<MissleManager>().SetupMissle(transform, target.transform);

                //arrow.transform.DOMove(target.transform.position, 4);
                //arrow.transform.position = Vector3.MoveTowards(arrow.transform.position, target.transform.position, 100 * Time.deltaTime); // config 
            }
        }
    }
    // -----------------

    #region GET
    public float Get_moventspeed()
    {
        return rosterinfo.moventspeed;
    }
    #endregion
}
