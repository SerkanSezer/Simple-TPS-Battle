using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    private const float ROAM_RANGE = 5;
    private const float CHASE_RANGE = 15;
    private const float ATTACK_RANGE = 10;

    private NavMeshAgent navMeshAgent;
    private Vector3 startingPosition;
    private EnemyAnimation enemyAnimation;
    private EnemyAttack enemyAttack;
    private PlayerHealth playerHealth;

    private const float TIMER_ATTACK_MAX = 0.3f;
    private float timerAttack;

    public enum State {
        Roaming,
        Chase,
        Attack,
        Death
    }
    private State state;
    private void Awake() {
        enemyAttack = GetComponent<EnemyAttack>();
        enemyAnimation = GetComponent<EnemyAnimation>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Start() {
        playerHealth = ThirdPersonShooterController.instance.GetComponent<PlayerHealth>();
        ThirdPersonShooterController.instance.OnPlayerDead += ThirdPersonShooterController_OnGameFinished;
        startingPosition = transform.position;
        Roam();
        state = State.Roaming;
    }
    private void ThirdPersonShooterController_OnGameFinished(bool gameStatus) {
        if (!gameStatus) {
            state = State.Roaming;
            Roam();
            enemyAttack.EnemyStopAttack();
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y, 0));
        }
    }
    private void Update() {
        switch (state) {
            case State.Roaming:
                enemyAnimation.SetWalk();
                navMeshAgent.speed = 1.5f;
                if (Vector3.Distance(transform.position, navMeshAgent.destination) < 0.2f) {
                    Roam();
                }
                if (playerHealth.IsPlayerLive() && Vector3.Distance(transform.position, ThirdPersonShooterController.instance.transform.position) < CHASE_RANGE) {
                    state = State.Chase;
                }
                break;
            case State.Chase:
                enemyAnimation.SetRun();
                navMeshAgent.speed = 2.5f;
                if (Vector3.Distance(transform.position, ThirdPersonShooterController.instance.transform.position) < ATTACK_RANGE) {
                    state = State.Attack;
                    Stop();
                }
                else if (Vector3.Distance(transform.position, ThirdPersonShooterController.instance.transform.position) > CHASE_RANGE) {
                    state = State.Roaming;
                    MoveToStartinPosition();
                }
                else {
                    Chase();
                }
                break;
            case State.Attack:
                enemyAnimation.SetShoot(true);
                navMeshAgent.speed = 0f;
                transform.LookAt(ThirdPersonShooterController.instance.transform);
                Attack();
                if (Vector3.Distance(transform.position, ThirdPersonShooterController.instance.transform.position) > ATTACK_RANGE) {
                    state = State.Chase;
                    enemyAttack.EnemyStopAttack();
                }
                break;
            case State.Death:
                break;
            default:
                break;
        }
    }

    public void MoveToStartinPosition() {
        navMeshAgent.SetDestination(startingPosition);
    }

    public void Roam() {
        if (NavMesh.SamplePosition(transform.position + (Random.insideUnitSphere * ROAM_RANGE), out NavMeshHit hit, ROAM_RANGE, NavMesh.AllAreas)) {
            navMeshAgent.SetDestination(hit.position);
        }
    }

    public void Chase() {
        navMeshAgent.SetDestination(ThirdPersonShooterController.instance.transform.position);
    }
    
    public void Stop() {
        navMeshAgent.SetDestination(transform.position);
    }

    public void Attack() {
        timerAttack += Time.deltaTime;
        if (timerAttack > TIMER_ATTACK_MAX) {
            enemyAttack.EnemyAttackToPlayer();
            timerAttack = 0;
        }
    }

    public void SetEnemyDeath() {
        state = State.Death;
        navMeshAgent.enabled = false;
        enemyAttack.EnemyStopAttack();
    }

}
