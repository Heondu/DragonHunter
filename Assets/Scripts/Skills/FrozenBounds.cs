using UnityEngine;

public class FrozenBounds : Explosive
{
    protected override void Attack(ILivingEntity entity)
    {
        base.Attack(entity);
        entity.ChangeState(State.Frozen, 1);
    }
}
