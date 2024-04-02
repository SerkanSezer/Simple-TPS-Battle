using StarterAssets;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public event Action OnGameWin;
    private const string GAME_SCENE = "GameScene";
    private const string HOME_SCENE = "MainMenu";

    private StarterAssetsInputs starterAssetsInputs;
    private PlayerInput playerInput;

    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private TextMeshProUGUI loseText;
    
    [SerializeField] private TextMeshProUGUI durationText;
    [SerializeField] private TextMeshProUGUI killCountText;

    [SerializeField] private Transform gamePanel;
    [SerializeField] private Transform gamePausePanel;
    [SerializeField] private Transform gameFinishedPanel;

    private int totalEnemyCount;
    private int killCount;
    private void Awake() {
        instance = this;
        starterAssetsInputs = FindObjectOfType<StarterAssetsInputs>();
        playerInput = FindObjectOfType<PlayerInput>();
        playerInput.enabled = true;
    }
    private void Start() {
        ThirdPersonShooterController.instance.OnPlayerDead += ThirdPersonShooterController_OnGameFinished;
    }

    private void Update() {
        if (starterAssetsInputs.pause) {
            Pause();
            starterAssetsInputs.pause = false;
        }
    }

    public void AddEnemyCount() {
        totalEnemyCount++;
    }
    public void AddKillCount() {
        killCount++;
        if (killCount >= totalEnemyCount) {
            ThirdPersonShooterController_OnGameFinished(true);
            OnGameWin?.Invoke();
        }
    }

    private void ThirdPersonShooterController_OnGameFinished(bool obj) {
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayFinishedMusic();
        SetFinishedStatusText(obj);
        SetGameStats();
        ShowGameFinishedPanel();
        HideGamePanel();
        ResetInputs();
    }

    private void ResetInputs() {
        starterAssetsInputs.cursorInputForLook = false;
        starterAssetsInputs.cursorLocked = false;
        starterAssetsInputs.SetCursorState(false);
        starterAssetsInputs.shoot = false;
        playerInput.enabled = false;
    }
    public void ActivateInput() {
        starterAssetsInputs.cursorInputForLook = true;
        starterAssetsInputs.cursorLocked = true;
        starterAssetsInputs.SetCursorState(true);
        starterAssetsInputs.shoot = true;
        playerInput.enabled = true;
    }

    private void SetFinishedStatusText(bool status) {
        if (status) {
            winText.gameObject.SetActive(true);
        }
        else {
            loseText.gameObject.SetActive(true);
        }
    }

    private void SetGameStats() {
        durationText.SetText((Time.time / 60).ToString("F1")+" minutes");
        killCountText.SetText(killCount.ToString());
    }

    private void ShowGameFinishedPanel() {
        gameFinishedPanel.gameObject.SetActive(true);
    }
    private void HideGamePanel() {
        gamePanel.gameObject.SetActive(false);
    }

    public void Pause() {
        ResetInputs();
        gamePausePanel.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume() {
        ActivateInput();
        gamePausePanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame() {
        Time.timeScale = 1;
        SceneManager.LoadScene(GAME_SCENE);
    }
    public void GoHomeScene() {
        Time.timeScale = 1;
        SceneManager.LoadScene(HOME_SCENE);
    }
}
