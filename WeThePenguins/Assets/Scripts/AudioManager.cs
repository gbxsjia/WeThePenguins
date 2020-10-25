using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;//单例

    public AudioSource MusicPlayer;
    public AudioSource SoundPlayer;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = this;

        //给对象添加AudioSource组件
        MusicPlayer = gameObject.AddComponent<AudioSource>();
        SoundPlayer = gameObject.AddComponent<AudioSource>();
        //设置不一开始就播放音效
        MusicPlayer.playOnAwake = false;
        SoundPlayer.playOnAwake = false;
    }

    //播放音乐
    public void PlayMusic(string name)
    {
        if (MusicPlayer.isPlaying == false)
        {
            AudioClip clip = Resources.Load<AudioClip>(name);
            MusicPlayer.clip = clip;
            MusicPlayer.Play();
        }

    }

    //播放音效
    public void PlaySound(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>(name);
        SoundPlayer.clip = clip;
        SoundPlayer.PlayOneShot(clip);
    }

}
