using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;
    private const string _shoot = "Shoot";
    private const string _speed = "Speed";
    private const string _dying = "Dying";
    private const string _hit = "Hit";

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void SetIdle() {
        SetShoot(false);
        animator.SetFloat(_speed, 0);
    }
    public void SetWalk() {
        SetShoot(false);
        animator.SetFloat(_speed, 2.5f);
    }

    public void SetRun() {
        SetShoot(false);
        animator.SetFloat(_speed, 3);
    }

    public void SetShoot(bool shoot) {
        animator.SetBool(_shoot, shoot);
    }

    public void SetHit() {
        animator.SetTrigger(_hit);
    }

    public void SetDying() {
        animator.SetTrigger(_dying);
    }
}
