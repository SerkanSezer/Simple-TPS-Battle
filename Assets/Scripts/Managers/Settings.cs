using TMPro;
using UnityEngine;

public class Settings : MonoBehaviour
{
    private const int MAX_VOLUME = 10;
    [SerializeField] AudioSource AudioSource;
    private int soundVolume;
    private int musicVolume;
    [SerializeField] private TextMeshProUGUI soundText;
    [SerializeField] private TextMeshProUGUI musicText;
    private void Start() {
        SaveManager.Init();
        LoadAudioVolumes();
    }
    public void SoundVolumeUp() {
        if(soundVolume < 10) {
            soundVolume++;
            UpdateSoundVolumeText();
            SaveAudioVolumes();
        }
    }
    public void SoundVolumeDown() {
        if (soundVolume > 0) {
            soundVolume--;
            UpdateSoundVolumeText();
            SaveAudioVolumes();
        }
    }

    public void MusicVolumeUp() {
        if (musicVolume < 10) {
            musicVolume++;
            UpdateMusicVolumeText();
            SaveAudioVolumes();
        }
    }
    public void MusicVolumeDown() {
        if (musicVolume > 0) {
            musicVolume--;
            UpdateMusicVolumeText();
            SaveAudioVolumes();
        }
    }
    private void UpdateSoundVolumeText() {
        soundText.SetText(soundVolume.ToString());
    }
    private void UpdateMusicVolumeText() {
        musicText.SetText(musicVolume.ToString());
        AudioSource.volume = (float)musicVolume / MAX_VOLUME;
    }

    private void LoadAudioVolumes() {
        string saveString = SaveManager.Load();
        SaveProps saveProps = JsonUtility.FromJson<SaveProps>(saveString);
        soundVolume = saveProps.soundVolume;
        musicVolume = saveProps.musicVolume;
        UpdateSoundVolumeText();
        UpdateMusicVolumeText();
    }

    private void SaveAudioVolumes() {
        SaveProps saveProps = new SaveProps();
        saveProps.soundVolume = soundVolume;
        saveProps.musicVolume = musicVolume;
        string saveString = JsonUtility.ToJson(saveProps);
        SaveManager.Save(saveString);
    }

}
