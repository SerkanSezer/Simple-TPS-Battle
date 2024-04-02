using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private const int MAX_VOLUME = 10;
    public static AudioManager instance { get; set; }
    private AudioSource audioSource;
    [SerializeField] AudioClip finishedMusic;

    private void Awake() {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        LoadAudioVolume();
        audioSource.Play();
    }

    public void StopMusic() {
        audioSource.Stop();
    }
    
    public void PlayFinishedMusic() {
        audioSource.PlayOneShot(finishedMusic, audioSource.volume);
    }

    public void LoadAudioVolume() {
        string saveString = SaveManager.Load();
        SaveProps saveProps = JsonUtility.FromJson<SaveProps>(saveString);
        audioSource.volume = (float)saveProps.musicVolume/ MAX_VOLUME;
    }
    
}
