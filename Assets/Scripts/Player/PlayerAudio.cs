using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private const int MAX_VOLUME = 10;
    public AudioSource audioSourceFootStep;
    public AudioSource audioSourceMachinegun;
    [SerializeField] private AudioClip walkSound;
    [SerializeField] private AudioClip runSound;
    private void Start() {
        GetAudioVolume();
    }
    public AudioClip GetWalkSound() {
        return walkSound;
    }
    public AudioClip GetRunSound() {
        return runSound;
    }
    public void GetAudioVolume() {
        string saveString = SaveManager.Load();
        SaveProps saveProps = JsonUtility.FromJson<SaveProps>(saveString);
        audioSourceFootStep.volume = (float)saveProps.soundVolume/ MAX_VOLUME;
        audioSourceMachinegun.volume = (float)saveProps.soundVolume/ MAX_VOLUME;
    }
}
