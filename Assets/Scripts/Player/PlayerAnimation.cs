using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    private Animator animator;
    private const string _shoot = "Shoot";
    private const string _dying = "Dying";
    private const string _hit = "Hit";
    private void Awake() {
        animator = GetComponent<Animator>();
    }
    public void SetHit() {
        animator.SetTrigger(_hit);
    }
    public void SetDying() {
        animator.SetTrigger(_dying);
    }
    public void SetShoot(bool shoot) {
        animator.SetBool(_shoot, shoot);
    }
}
