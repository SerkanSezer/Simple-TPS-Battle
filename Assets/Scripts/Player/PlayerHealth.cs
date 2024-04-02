using UnityEngine;
public class PlayerHealth : MonoBehaviour, IDamagable
{
    [SerializeField] Transform healthBar;
    [SerializeField] private Transform bloodSplat;
    private PlayerAnimation playerAnimation;
    private const int TOTAL_PLAYER_HEALTH = 100;
    private int playerHealth = 100;
    private bool playerLive = true;
    private void Awake() {
        playerAnimation = GetComponent<PlayerAnimation>();
    }
    public void Damage(int damageAmount, Vector3 hitPos) {
        if (playerLive) {
            playerHealth -= damageAmount;
            CreateBloodSplat(hitPos);
            UpdatePlayerHealthBar();
            if (playerHealth <= 0) {
                playerAnimation.SetDying();
                playerLive = false;
                ThirdPersonShooterController.instance.PlayerResetGameOver();
            }
            else {
                playerAnimation.SetHit();

            }
        }
    }

    public bool IsPlayerLive() {
        return playerLive;
    }
    public void UpdatePlayerHealthBar() {
        float scaledHealth = (float)playerHealth / (float)TOTAL_PLAYER_HEALTH;
        if (scaledHealth >= 0) {
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
