using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleFX;
    [SerializeField] Transform muzzlePoint;
    [SerializeField] LayerMask layerMask;
    private AudioSource audioSource;
    private void Awake() {
        audioSource = GetComponent<AudioSource>();  
    }

    public void EnemyAttackToPlayer() {
        if(!muzzleFX.isPlaying) muzzleFX.Play();   
        Vector3 attackDir = (ThirdPersonShooterController.instance.transform.position - muzzlePoint.position).normalized;
        if (Physics.Raycast(muzzlePoint.position, attackDir, out RaycastHit hit ,999,layerMask)) {
            if (hit.transform.TryGetComponent<IDamagable>(out IDamagable damage)) {
                damage.Damage(5, hit.point);
                if(!audioSource.isPlaying) audioSource.Play();
            }
        }
        else {
            if(audioSource.isVirtual) audioSource.Stop();
        }
    }

    public void EnemyStopAttack() {
        if (muzzleFX.isPlaying) muzzleFX.Stop();
    }

}
