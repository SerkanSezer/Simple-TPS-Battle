using Cinemachine;
using StarterAssets;
using System;
using UnityEngine;

public class ThirdPersonShooterController : MonoBehaviour{

    public static ThirdPersonShooterController instance { get; set; }

    [SerializeField] private CinemachineVirtualCamera aimCamera;
    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    public event Action<bool> OnPlayerDead;

    private Item currentWeapon;

    [SerializeField] private Transform spawnBulletPos;
    [SerializeField] private Transform bulletImpactFX;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();

    private PlayerInteract playerInteract;
    private PlayerAnimation playerAnimation;
    private PlayerAudio playerAudio;
    private AudioSource audioSource;

    public ParticleSystem muzzleFX;
    private float SHOOT_TIMER_MAX;
    private float shootTimer =1;
    private const float SHAKE_AMPLITUDE = 0.3f;

    private Vector3 hitPosition;

    private void Awake() {
        instance = this;
        playerAnimation = GetComponent<PlayerAnimation>();
        playerInteract = GetComponent<PlayerInteract>();
        playerInteract.OnWeaponChange += PlayerInteract_OnWeaponChange;
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        playerAudio = GetComponent<PlayerAudio>();
        audioSource = playerAudio.audioSourceMachinegun;
    }
    private void Start() {
        GameManager.instance.OnGameWin += ResetPlayer;
    }

    private void PlayerInteract_OnWeaponChange(Item item) {
        if(item.GetItemType() == ItemType.Rifle) {
            currentWeapon = item;
            SHOOT_TIMER_MAX = currentWeapon.GetItemSO().WEAPON_TIMER_MAX;
            muzzleFX = currentWeapon.GetMuzzleFX();
        }
    }

    private void Update() {
        Vector3 mouseWorldPos = Vector3.zero;
        Vector2 screenPoint = new Vector2(Screen.width / 2, Screen.height / 2);

        Ray ray = Camera.main.ScreenPointToRay(screenPoint);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f, aimColliderLayerMask)) {
            hitPosition = hit.point;
            mouseWorldPos = hit.point;
            Shoot(hit.transform, hit.point);
        }

        if (starterAssetsInputs.aim) {
            aimCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);

            Vector3 aimTarget = mouseWorldPos;
            aimTarget.y = transform.position.y;
            Vector3 aimDir = (aimTarget - transform.position).normalized;
            float rotSpeed = 50;
            transform.forward = Vector3.Lerp(transform.forward, aimDir, rotSpeed * Time.deltaTime);
        }
        else {
            thirdPersonController.SetSensitivity(normalSensitivity);
            aimCamera.gameObject.SetActive(false);
            thirdPersonController.SetRotateOnMove(true);
        }
    }

    public void Shoot(Transform targetTransform, Vector3 hitPos) {
        if (starterAssetsInputs.aim && starterAssetsInputs.shoot) {

            shootTimer += Time.deltaTime;

            if (shootTimer > SHOOT_TIMER_MAX) {
                if (currentWeapon.GetMagCount() > 0) {
                    if (currentWeapon.GetBulletCount() > 0) {
                        playerAnimation.SetShoot(true);
                        if (!muzzleFX.isPlaying) muzzleFX.Play(true);
                        aimCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = SHAKE_AMPLITUDE;
                        audioSource.enabled = true;
                        Vector3 impactDir = (spawnBulletPos.position - hitPosition).normalized;
                        Instantiate(bulletImpactFX, hitPosition, Quaternion.LookRotation(impactDir, Vector3.up));
                        shootTimer = 0;

                        if (targetTransform.TryGetComponent<IDamagable>(out IDamagable damage)) {
                            damage.Damage(30, hitPos);
                        }

                        currentWeapon.SetBulletCount(currentWeapon.GetBulletCount()-1);
                        currentWeapon.UpdateBulletCount();
                    }
                    else {
                        currentWeapon.SetBulletCount(currentWeapon.GetBulletCapacity());
                        currentWeapon.SetMagCount(currentWeapon.GetMagCount()-1);
                        currentWeapon.UpdateMagCount();
                    }
                }
                else {
                    audioSource.enabled = false;
                    playerAnimation.SetShoot(false);
                    if (muzzleFX) muzzleFX.Stop(true);
                    currentWeapon.SetMagCount(0);
                    currentWeapon.UpdateMagCount();
                }
            }
        }
        else {
            shootTimer = 1;
            aimCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
            playerInteract.GetPlayerInventory().UpdateItemCount(currentWeapon);
            audioSource.enabled = false;
            playerAnimation.SetShoot(false);
            if (muzzleFX) muzzleFX.Stop(true);
        }
    }

    public void PlayerResetGameOver() {
        OnPlayerDead?.Invoke(false);
    }
    public void ResetPlayer() {
        playerAnimation.SetShoot(false);
        if (muzzleFX) muzzleFX.Stop(true);
    }

}
