using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rules;
using static InputManager;
using System;

public class Monster : Unit
{
    public enum Modes { Search, Hunt, Happy, Dead };
    public Modes currentMode;

    Animator animator;



    MonsterLight light;
    MonsterFlame flame;

    Vector3 startPosition;
    float moveTimer;
    Player player;
    float actionWait;
    float timeBetweenSteps;

    float happyTimer;
    float monsterGrowth;

    private Cell lastKnownCell;
    private float timeSinceSpotted;

    float newSearchPointTimer;
    Cell searchCell;

    public LayerMask searchMask;
    RaycastHit hit;

    protected override void RefreshForNewWorld()
    {
        base.RefreshForNewWorld();
        moveTimer = 3;
        hp = new Health();
        hp.SetMaxHealth(ruleSet.MONSTER_HEALTH + monsterGrowth);
        hp.SetCurrentHealth(ruleSet.MONSTER_HEALTH + monsterGrowth);
        if (GetComponent<iTween>())
        {
            ServiceLocator.GetDebugProvider().Log("remove itween");
            Destroy(GetComponent<iTween>());
        }
        this.transform.position = new Vector3(pos.x, 0, pos.y);
    }

    void Start()
    {

        animator = GetComponentInChildren<Animator>();
        light = GetComponentInChildren<MonsterLight>();
        flame = GetComponentInChildren<MonsterFlame>();
        Cell randomStartPosition = GridHolder.GetRandomWalkableCell();
        pos.x = randomStartPosition.pos.x;
        pos.y = randomStartPosition.pos.y;
        // pos.x = 5;
        // pos.y = 5;
        if (GameManager.INSTANCE.GetGameState() == GameManager.GameState.gameFinish)
        {
            //player previous pos
            monsterGrowth = ruleSet.MONSTER_GROWTH_FACTOR;
            this.transform.position = startPosition;
        }
        else
        {
            //new pos
            monsterGrowth = 0;
            this.transform.position = new Vector3(pos.x, 0, pos.y);
        }

        moveTimer = 3f;
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        actionWait = ruleSet.MONSTER_MOVEMENT_TICK_SEARCH - monsterGrowth;
        hp = new Health();
        hp.SetMaxHealth(ruleSet.MONSTER_HEALTH + monsterGrowth);
        hp.SetCurrentHealth(ruleSet.MONSTER_HEALTH + monsterGrowth);

        hp.IsDead += GameManager.INSTANCE.GameFinished;
        hp.IsDead += GetPlayerPosition;

        GridHolder.BranchSnap += TargetSteppedOnBranch;

        GameManager.INSTANCE.NewWorldCreated += RefreshForNewWorld;

        ChangeMode(Modes.Search);
    }



    private void OnDisable()
    {
        hp.IsDead -= GameManager.INSTANCE.GameFinished;
        hp.IsDead -= GetPlayerPosition;
        GameManager.INSTANCE.NewWorldCreated -= RefreshForNewWorld;
        GridHolder.BranchSnap -= TargetSteppedOnBranch;


    }

    private void ChangeMode(Modes newMode)
    {
        currentMode = newMode;
        flame.ChangeMode(currentMode);
        switch (currentMode)
        {
            case Modes.Search:
                animator.SetBool("FoundPlayer", false);
                animator.SetBool("Moving", true);
                newSearchPointTimer = ruleSet.MONSTER_SEARCH_TIMER.GetRandomValue();
                searchCell = GridHolder.GetRandomWalkableCell();
                timeBetweenSteps = ruleSet.MONSTER_MOVEMENT_TICK_SEARCH;
                break;
            case Modes.Hunt:
                animator.SetBool("FoundPlayer", true);
                animator.SetBool("Moving", true);
                ServiceLocator.GetAudioProvider().PlaySoundEvent("Inhale");
                timeBetweenSteps = ruleSet.MONSTER_MOVEMENT_TICK_HUNT;
                timeSinceSpotted = 0;
                break;
            case Modes.Happy:
                animator.SetBool("FoundPlayer", true);
                animator.SetBool("Moving", false);
                timeBetweenSteps = int.MaxValue;
                happyTimer = ruleSet.MONSTER_WAIT_TIME;
                break;
            case Modes.Dead:
                animator.SetBool("FoundPlayer", false);
                animator.SetBool("Moving", false);
                timeBetweenSteps = int.MaxValue;
                if (GetComponent<iTween>())
                {
                    ServiceLocator.GetDebugProvider().Log("remove itween");
                    Destroy(GetComponent<iTween>());
                }
                Instantiate(Resources.Load<GameObject>("Prefabs/Poof/Poof"), this.transform.position, Quaternion.identity);
                this.transform.position = Vector3.one * 5000;
                break;
        }
    }

    public void TargetSteppedOnBranch(Vector3Int pos)
    {
        ChangeMode(Modes.Hunt);
        searchCell = GridHolder.GetCell(pos);
        timeSinceSpotted = -2;
    }

    void Update()
    {
        if (GameManager.INSTANCE.GetGameState() == GameManager.GameState.pause)
        {
            return;
        }
        if (GameManager.INSTANCE.GetGameState() == GameManager.GameState.gameOver)
        {
            return;
        }
        if (GameManager.INSTANCE.GetGameState() == GameManager.GameState.gameFinish)
        {
            return;
        }

        if (currentMode == Modes.Dead)
        {
            return;
        }

        light.UpdateColor(hp.GetCurrentHealth(), hp.GetMaxHealth());

        hp.TakeDamage(ruleSet.MONSTER_HEALTH_LOSS_RATE * Time.deltaTime);


        if (hp.GetCurrentHealth() <= 0 && currentMode != Modes.Dead)
        {
            ChangeMode(Modes.Dead);

        }

        if (currentMode == Modes.Dead)
        {
            return;
        }

        switch (currentMode)
        {
            case Modes.Search:

                if (LookForPlayer() != null)
                {
                    ChangeMode(Modes.Hunt);
                    moveTimer = 0.25f;
                    return;
                }

                newSearchPointTimer -= Time.deltaTime;
                if (newSearchPointTimer <= 0)
                {
                    newSearchPointTimer = ruleSet.MONSTER_SEARCH_TIMER.GetRandomValue();
                    searchCell = GridHolder.GetRandomWalkableCell();
                }

                moveTimer -= Time.deltaTime;
                if (moveTimer <= 0)
                {
                    moveTimer = timeBetweenSteps;
                    Direction direction = Pathfinding.GetNextStepDirection(this.pos.GetAsVector3Int(), searchCell.pos.GetAsVector3Int());
                    if (direction != Direction.NONE)
                    {
                        MoveAlongDirection(direction, ruleSet.MONSTER_MOVEMENT_TICK_SEARCH);
                    }
                }


                break;
            case Modes.Hunt:

                Cell prevCell = searchCell;
                Cell LookingAtCell = LookForPlayer();
                if (LookingAtCell != null)
                {
                    searchCell = LookingAtCell;
                    timeSinceSpotted = 0;
                }

                if (prevCell == searchCell)
                {
                    timeSinceSpotted += Time.deltaTime;
                    if (timeSinceSpotted > 1)
                    {
                        ChangeMode(Modes.Search);
                    }
                }

                moveTimer -= Time.deltaTime;
                if (moveTimer <= 0)
                {
                    moveTimer = timeBetweenSteps;
                    Direction direction = Pathfinding.GetNextStepDirection(this.pos.GetAsVector3Int(), searchCell.AsVector3Int());
                    if (direction != Direction.NONE)
                    {
                        MoveAlongDirection(direction, ruleSet.MONSTER_MOVEMENT_TICK_HUNT);
                    }
                }
                break;
            case Modes.Happy:
                happyTimer -= Time.deltaTime;
                if (happyTimer <= 0)
                {
                    ChangeMode(Modes.Search);
                }
                break;
            case Modes.Dead:
                break;
            default:
                break;
        }

    }

    public override void MoveAlongDirection(Direction direction, float _speed)
    {
        if (currentMode == Modes.Dead)
        {
            return;
        }
        if (NextToPlayer())
        {
            AttackPlayer();
            ChangeMode(Modes.Happy);
        }
        else
        {
            switch (currentMode)
            {
                case Modes.Search:
                    base.MoveAlongDirection(direction, ruleSet.MONSTER_MOVEMENT_TICK_SEARCH);
                    break;
                case Modes.Hunt:
                    base.MoveAlongDirection(direction, ruleSet.MONSTER_MOVEMENT_TICK_HUNT);
                    break;
                case Modes.Happy:
                    break;
                case Modes.Dead:
                    break;
                default:
                    break;
            }
        }

    }

    private void AttackPlayer()
    {
        player.GetAttacked();
    }

    private bool NextToPlayer()
    {
        int xDif = Mathf.Abs(pos.x - player.pos.x);
        int yDif = Mathf.Abs(pos.y - player.pos.y);

        return ((xDif + yDif) == 1);
    }

    private Cell LookForPlayer()
    {
        if (Vector3.Distance(transform.position, player.gameObject.transform.position) <= ruleSet.LIGHT_RADIUS)
        {
            if (Physics.Linecast(transform.position, player.transform.position, out hit, searchMask))
            {
                if (hit.collider.gameObject == player.gameObject)
                {
                    return GridHolder.GetCell(player.pos.GetAsVector3Int());
                }
            }
        }
        return null;
    }

    public void GetPlayerPosition()
    {
        startPosition = player.transform.position;
    }
}
