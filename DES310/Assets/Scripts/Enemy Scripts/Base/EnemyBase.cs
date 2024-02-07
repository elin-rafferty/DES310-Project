using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageable, IEnemyMoveable, ITriggerCheckable
{
    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    [SerializeField] private CircleCollider2D aggroZone;
    private float speedMultiplier = 1f;
    public float CurrentHealth { get; set; }
    public Rigidbody2D rb { get; set; }

    public bool IsAggro { get; set; }
    public bool IsWithinAttackRange { get; set; }
    public bool IsLineOfSight { get; set; }

    #region State Machine Variables

    public EnemyStateMachine StateMachine { get; set; }
    public EnemyIDLEState IDLEState { get; set; }
    public EnemyCHASEState CHASEState { get; set; }
    public EnemySEARCHState SEARCHState { get; set; }
    public EnemyATTACKState ATTACKState { get; set; }

    #endregion

    #region Scriptable Object Variables

    [SerializeField] private EnemyIdleSOBase EnemyIdleBase;
    [SerializeField] private EnemyChaseSOBase EnemyChaseBase;
    [SerializeField] private EnemySearchSOBase EnemySearchBase;
    [SerializeField] private EnemyAttackSOBase EnemyAttackBase;

    public EnemyIdleSOBase EnemyIdleBaseInstance { get; set; }
    public EnemyChaseSOBase EnemyChaseBaseInstance { get; set; }
    public EnemySearchSOBase EnemySearchBaseInstance { get; set; }
    public EnemyAttackSOBase EnemyAttackBaseInstance { get; set; }

    #endregion

    private void Awake()
    {
        EnemyIdleBaseInstance = Instantiate(EnemyIdleBase);
        EnemyChaseBaseInstance = Instantiate(EnemyChaseBase);
        EnemySearchBaseInstance = Instantiate(EnemySearchBase);
        EnemyAttackBaseInstance = Instantiate(EnemyAttackBase);

        StateMachine = new EnemyStateMachine();

        IDLEState = new EnemyIDLEState(this, StateMachine);
        CHASEState = new EnemyCHASEState(this, StateMachine);
        SEARCHState = new EnemySEARCHState(this, StateMachine);
        ATTACKState = new EnemyATTACKState(this, StateMachine);
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
    public void SetModifiers(ModifierBehaviour modifier)
    {
        CurrentHealth *= modifier.enemyHealthMultiplier;
        aggroZone.radius *= modifier.enemyAggroRangeMultiplier;
        speedMultiplier = modifier.enemySpeedMultiplier;
    }
    #endregion

    #region HP / Damage Functions
    public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    #endregion

    #region Movement Functions

    public void MoveEnemy(Vector2 velocity)
    {
        rb.velocity = velocity * speedMultiplier;
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
