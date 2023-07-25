﻿using UnityEngine;
using UnityEngine.Audio;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [Header("Sound Effects")]
        [SerializeField] private AudioClip CarDoorOpenSound;
        [SerializeField] private AudioClip CarCrashSound;
        [SerializeField] private AudioClip CarExplosionSound;
        [SerializeField] private AudioClip CarDriveOnRoadSound;
        [SerializeField] private AudioClip DrivePastCarSound;
        [SerializeField] private AudioSource currentActiveSoundEffect;
        [SerializeField] private AudioSource currentActiveContinuousSoundEffect;
    
        [Header("Music")]
        [SerializeField] private AudioClip soundtrack;
        [SerializeField] private AudioSource currentActiveMusic;
        
        [SerializeField] private AudioSource[] allAudioSources;
        
        [Header("Mixer Groups")]
        [SerializeField] private AudioMixerGroup musicMixerGroup;
        [SerializeField] private AudioMixerGroup sfxMixerGroup;
        [SerializeField] private AudioMixerGroup sfxContinuousMixerGroup;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("Tried to create another AudioManager");
            }
            
            DontDestroyOnLoad(gameObject);
            allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        }
        
        void StopAllAudio() {
            foreach( AudioSource audios in allAudioSources) {
                audios.Stop();
            }
        }

        void Start()
        {
            PlaySoundtrack();
        }

        private void PlaySoundtrack()
        {
            //currentActiveMusic.clip = ;
            currentActiveMusic.Play();
            currentActiveMusic.loop = true;
        }

        public void PlayCarPassSoundEffect()
        {
            currentActiveSoundEffect.Stop();
            currentActiveSoundEffect.clip = DrivePastCarSound;
            currentActiveSoundEffect.Play();
        }

        public void PlayCarCrashSoundEffect()
        {
            currentActiveSoundEffect.Stop();
            currentActiveSoundEffect.clip = CarCrashSound;
            currentActiveSoundEffect.Play();
        }

        public void PlayCarDestroyedSoundEffect()
        {
            currentActiveSoundEffect.Stop();
            currentActiveSoundEffect.clip = CarExplosionSound;
            currentActiveSoundEffect.Play();
        }
        
        public void PlayCarDoorOpenSoundEffect()
        {
            currentActiveSoundEffect.Stop();
            currentActiveSoundEffect.clip = CarDoorOpenSound;
            currentActiveSoundEffect.Play();
        }

        public void PlayCarDriveContinuousSoundEffect()
        {
            currentActiveContinuousSoundEffect.Stop();
            currentActiveContinuousSoundEffect.clip = CarDriveOnRoadSound;
            currentActiveContinuousSoundEffect.Play();
            currentActiveContinuousSoundEffect.loop = true;
        }
    }
}
