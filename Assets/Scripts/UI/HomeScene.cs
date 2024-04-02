using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScene : MonoBehaviour
{
    private const string GAME_SCENE = "GameScene";
    [SerializeField] Transform settingsPanel;
    [SerializeField] Transform menuPanel;
    public void StartGame() {
        SceneManager.LoadScene(GAME_SCENE);
    }

    public void OpenSettingsPanel() {
        settingsPanel.gameObject.SetActive(true);
        menuPanel.gameObject.SetActive(false);
    }

    public void CloseSettingsPanel() {
        settingsPanel.gameObject.SetActive(false);
        menuPanel.gameObject.SetActive(true);
    }
    public void QuitGame() {
        Application.Quit();
    }
}
