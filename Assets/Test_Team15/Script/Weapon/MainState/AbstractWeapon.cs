using UnityEngine;


//하위 클래스에서 코루틴 사용할 수 있게 모노비해버 상속받아야함.
public abstract class AbstractWeapon : MonoBehaviour, IWeapon
{
    protected WeaponState currentState;

    protected virtual void Awake()
    {
        currentState = WeaponState.IDLE;
    }

    public virtual void Update()
    {
        switch (currentState)
        {
            case WeaponState.IDLE:
                UpdateIdle();
                break;
            case WeaponState.ATTACKING:
                UpdateAttacking();
                break;
            case WeaponState.COOLDOWN:
                UpdateCooldown();
                break;
        }
    }

    public void ChangeState(WeaponState newState)
    {
        currentState = newState;
    }

    public abstract void Attack();

    protected abstract void UpdateIdle();
    protected abstract void UpdateAttacking();
    protected abstract void UpdateCooldown();
}