using UnityEngine;

public class EffectAutoDestroyer : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            ObjectPooler.ObjectInactive(ObjectPooler.effectHolder, gameObject);
        }
    }
}
