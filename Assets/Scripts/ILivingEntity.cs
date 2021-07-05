public interface ILivingEntity
{
    void TakeDamage(int damage);
    SkillData GetSkillData();
    void ChangeState(State state, float t);
    float GetHP();
}

public enum State { None, Frozen }