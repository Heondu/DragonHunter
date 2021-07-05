public interface ILivingEntity
{
    void TakeDamage(int damage);
    SkillData GetSkillData();
    void ChangeState(State state, float t);
}

public enum State { None, Frozen }