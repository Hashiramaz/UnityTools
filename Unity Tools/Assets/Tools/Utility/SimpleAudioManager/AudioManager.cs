using UnityEngine.Audio;
using System;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
public class AudioManager : MonoBehaviour {

    public AudioMixerGroup masterAudioChannel;
    public AudioMixerGroup musicAudioChannel;    
    public AudioMixerGroup fxAudioChannel;
    public Sound[] sounds;
    public static AudioManager instance;
	// Use this for initialization
	void Awake () {
        if(instance == null)
            instance = this;
		foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            s.source.outputAudioMixerGroup = GetAudioMixer(s.soundType);
        }
	}

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();

//        Debug.Log("Tocando Audio " + name);
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
    public void FadeOut(string _name){
        StartCoroutine(FadeOutStop(_name,1f));
    }

    public  IEnumerator FadeOutStop(string name, float FadeTime) {
        
        Sound s = Array.Find(sounds, sound => sound.name == name);
       
        float startVolume = s.source.volume;


        yield return null;
        while (s.source.volume > 0.1f) {
            s.source.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
        s.source.Stop();
    }
    
    public void FadeIn(string _name){
        StartCoroutine(FadeInPlay(_name,1f));
    }

     public  IEnumerator FadeInPlay(string audioSourceName, float FadeTime) {

        Sound s = Array.Find(sounds, sound => sound.name == audioSourceName);
        s.source.Play();
        
        s.source.volume = 0f;
        while (s.source.volume < 0.9f) {
            s.source.volume += Time.deltaTime / FadeTime;
            yield return null;
        }
    }   
    private void Start()
    {
        //Play("LevelMusic");
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void StopAllSounds(){

    }

    public void FadeOutMusicMixer(){
        StartCoroutine(FadeOutChannel(musicAudioChannel));
    }

    public void FadeInMusicMixer(){
        StartCoroutine(FadeInChannel(musicAudioChannel));
    }


    public IEnumerator FadeOutChannel(AudioMixerGroup channel){

         float startVolume;
         float actualVolume;
         // = channel.audioMixer.SetFloat(channel.name);
        channel.audioMixer.GetFloat(channel.name,out startVolume);
        channel.audioMixer.GetFloat(channel.name,out actualVolume);
        
        Debug.Log("Start Volume: " + startVolume );

        float newvolume = startVolume;

        yield return null;
        while (startVolume > -79f) {

            newvolume = Mathf.Lerp(startVolume, -80f, 2f * Time.deltaTime);
           // newvolume -= startVolume * Time.deltaTime / 1f;

            channel.audioMixer.SetFloat(channel.name, newvolume);
            channel.audioMixer.GetFloat(channel.name,out startVolume);
            yield return null;

            
        }

    }

        public IEnumerator FadeInChannel(AudioMixerGroup channel){

         float startVolume;
         float actualVolume;
         // = channel.audioMixer.SetFloat(channel.name);
        channel.audioMixer.GetFloat(channel.name,out startVolume);
        channel.audioMixer.GetFloat(channel.name,out actualVolume);
        
        Debug.Log("Start Volume: " + startVolume );

        float newvolume = startVolume;

        yield return null;
        while (startVolume < -1f) {

            newvolume = Mathf.Lerp(startVolume, 0f, 2f * Time.deltaTime);
           //newvolume += Time.deltaTime / 2f;

            channel.audioMixer.SetFloat(channel.name, newvolume);
            channel.audioMixer.GetFloat(channel.name,out startVolume);
            yield return null;
        }

    }


    public AudioMixerGroup GetAudioMixer(AudioType soundType){
        if(soundType == AudioType.Music)
            return musicAudioChannel;
        else{
            return fxAudioChannel;
        }
    }
}
