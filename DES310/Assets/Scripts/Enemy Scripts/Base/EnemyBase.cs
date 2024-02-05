using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageable, IEnemyMoveable, ITriggerCheckable
{
    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    public float CurrentHealth { get; set; }
    public Rigidbody2D rb { get; set; }

    public bool IsAggro { get; set; }
    public bool IsWithinAttackRange { get; set; }

    #region State Machine Variables

    public EnemyStateMachine StateMachine { get; set; }
    public EnemyIDLEState IDLEState { get; set; }
    public EnemyCHASEState CHASEState { get; set; }
    public EnemySEARCHState SEARCHState { get; set; }
    public EnemyATTACKState ATTACKState { get; set; }

    #endregion

    public float RandomMovementRange = 2f;
    public float RandomAttackSpeed = 1f;

    private void Awake()
    {
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
