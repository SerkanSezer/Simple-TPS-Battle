using UnityEngine;
public class EnemyHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private Transform healthBar;
    [SerializeField] private Transform bloodSplat;
    private EnemyMove enemyMove;
    private EnemyAnimation enemyAnimation;
    private const int TOTAL_ENEMY_HEALTH = 100;
    private int enemyHealth = 100;
    private bool enemyLive = true;
    private void Awake() {
        enemyMove = GetComponent<EnemyMove>();
        enemyAnimation = GetComponent<EnemyAnimation>();
    }
    private void Start() {
        GameManager.instance.AddEnemyCount();
    }
    public void Damage(int damageAmount, Vector3 hitPos) {
        if (enemyLive) {
            enemyHealth -= damageAmount;
            CreateBloodSplat(hitPos);
            UpdateEnemyHealthBar();
            if (enemyHealth <= 0) {
                healthBar.parent.gameObject.SetActive(false);
                enemyAnimation.SetDying();
                enemyMove.SetEnemyDeath();
                enemyLive = false;
                GameManager.instance.AddKillCount();
            }
            else {
                enemyAnimation.SetHit();
            }
        }
    }
    public void UpdateEnemyHealthBar() {
        float scaledHealth = (float)enemyHealth / (float)TOTAL_ENEMY_HEALTH;
        if(scaledHealth >= 0) {
            healthBar.localScale = new Vector3(scaledHealth, 1, 1);
        }
        else {
            healthBar.localScale = new Vector3(0, 1, 1);
        }
    }
    private void CreateBloodSplat(Vector3 hitPos) {
        Instantiate(bloodSplat, hitPos, transform.rotation);
    }

}
