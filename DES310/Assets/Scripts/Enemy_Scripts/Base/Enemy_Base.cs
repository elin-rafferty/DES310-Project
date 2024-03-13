using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Base : MonoBehaviour, IDamageable, IEnemyMoveable, ITriggerCheckable
{
    [field: SerializeField] public EventHandler eventHandler { get; set; }
    [field: SerializeField] public GameObject alertIconPrefab { get; set; }
    public GameObject alertObject { get; set; }
    [field: SerializeField] public float meleeDamage { get; set; } = 5f;
    [field: SerializeField] public float rangedDamage { get; set; } = 0f;
    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    [field: SerializeField] public float aggroRange { get; set; } = 7f;
    [field: SerializeField] public float attackRange { get; set; } = 2f;
    [field: SerializeField] public float attackDelay { get; set; } = 1f;
    [field: SerializeField] public float speed { get; set; }
    [field: SerializeField] public float damageReduction { get; set; } = 0f;
    public string colliderTag { get; set; } = string.Empty;
    public float CurrentHealth { get; set; }
    public Rigidbody2D rb { get; set; }

    public bool IsAggro { get; set; }
    public bool IsWithinAttackRange { get; set; }
    public bool IsLineOfSight { get; set; }
    public float enemyTimer { get; set; }

    #region State Machine Variables

    public Enemy_State_Machine StateMachine { get; set; }
    public Enemy_IDLE_State IDLEState { get; set; }
    public Enemy_CHASE_State CHASEState { get; set; }
    public Enemy_SEARCH_State SEARCHState { get; set; }
    public Enemy_ATTACK_State ATTACKState { get; set; }

    #endregion

    #region Scriptable Object Variables

    [SerializeField] private Enemy_Idle_SO_Base EnemyIdleBase;
    [SerializeField] private Enemy_Chase_SO_Base EnemyChaseBase;
    [SerializeField] private Enemy_Search_SO_Base EnemySearchBase;
    [SerializeField] private Enemy_Attack_SO_Base EnemyAttackBase;

    public Enemy_Idle_SO_Base EnemyIdleBaseInstance { get; set; }
    public Enemy_Chase_SO_Base EnemyChaseBaseInstance { get; set; }
    public Enemy_Search_SO_Base EnemySearchBaseInstance { get; set; }
    public Enemy_Attack_SO_Base EnemyAttackBaseInstance { get; set; }

    #endregion

    private void Awake()
    {
        EnemyIdleBaseInstance = Instantiate(EnemyIdleBase);
        EnemyChaseBaseInstance = Instantiate(EnemyChaseBase);
        EnemySearchBaseInstance = Instantiate(EnemySearchBase);
        EnemyAttackBaseInstance = Instantiate(EnemyAttackBase);

        StateMachine = new Enemy_State_Machine();

        IDLEState = new Enemy_IDLE_State(this, StateMachine);
        CHASEState = new Enemy_CHASE_State(this, StateMachine);
        SEARCHState = new Enemy_SEARCH_State(this, StateMachine);
        ATTACKState = new Enemy_ATTACK_State(this, StateMachine);
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;

        rb = GetComponent<Rigidbody2D>();

        EnemyIdleBaseInstance.Initialise(gameObject, this);
        EnemyChaseBaseInstance.Initialise(gameObject, this);
        EnemySearchBaseInstance.Initialise(gameObject, this);
        EnemyAttackBaseInstance.Initialise(gameObject, this);

        StateMachine.Initialise(IDLEState);
    }

    private void Update() 
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
    }

    private void FixedUpdate() 
    {
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }


    #region Modifier Functions;

    virtual public void SetModifiers(Modifier_Behaviour modifier)
    {
        MaxHealth *= modifier.enemyHealthMultiplier;
        meleeDamage *= modifier.enemyDamageMultiplier;
        rangedDamage *= modifier.enemyDamageMultiplier;
        speed *= modifier.enemySpeedMultiplier;
        attackDelay /= modifier.enemyAttackSpeedMultiplier;
        attackRange *= modifier.enemyAttackRangeMultiplier;
        aggroRange *= modifier.enemyAggroRangeMultiplier;
    }

    #endregion

    #region HP / Damage Functions
    virtual public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //eventHandler.ChangeEnemyCount.Invoke(-1);
        Destroy(gameObject);
        if(alertObject) Destroy(alertObject);
    }

    #endregion

    #region Movement Functions

    public void MoveEnemy(Vector2 velocity)
    {
        rb.velocity = velocity;
    }

    #endregion

    #region Status Checks

    public void SetAggroStatus(bool aggroStatus)
    {
        IsAggro = aggroStatus;
    }

    public void SetWithinAttackRange(bool withinAttackRange)
    {
        IsWithinAttackRange = withinAttackRange;
    }

    public void SetLineOfSightStatus(bool lineOfSightStatus)
    {
        IsLineOfSight = lineOfSightStatus;
    }

    #endregion

    #region Animation Triggers

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentEnemyState.AnimationTriggerEvent(triggerType);
    }

    public enum AnimationTriggerType
    {
        EnemyDamaged,
        PlayFootsteps
    }

    #endregion
}
