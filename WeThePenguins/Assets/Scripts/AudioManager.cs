using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;//单例

    //音频源
    public AudioClip[] audioClips;
    public AudioClip bgm;
    
    private AudioSource MusicPlayer;
    private AudioSource[] SoundPlayer;
    private Dictionary<string, AudioClip> _DicAudio; //音频库(字典)

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        //给对象添加AudioSource组件
        MusicPlayer = gameObject.AddComponent<AudioSource>();
        MusicPlayer.playOnAwake = false;
        MusicPlayer.loop = true;
        MusicPlayer.clip = bgm;
        MusicPlayer.Play();

        _DicAudio = new Dictionary<string, AudioClip>();
        foreach (var item in audioClips)
        {
            _DicAudio.Add(item.name, item);
        }
    }


    //播放音效
    public void PlaySound(string name)
    {
        //当传进来的名字不为空且在音频库中
        if (_DicAudio.ContainsKey(name) && !string.IsNullOrEmpty(name))
        {
            AudioClip clip = _DicAudio[name];
            SoundPlayer = this.GetComponents<AudioSource>();
            //如果有空余的audiosource就播放
            for (int i = 1; i < SoundPlayer.Length; i++)
            {
                if (!SoundPlayer[i].isPlaying)
                {
                    SoundPlayer[i].clip = clip;
                    SoundPlayer[i].PlayOneShot(clip);
                    return;
                }
            }
            //如果没有就新建一个
            AudioSource newAs = gameObject.AddComponent<AudioSource>();
            newAs.loop = false;
            newAs.clip = clip;
            newAs.playOnAwake = false;
            newAs.Play();
        }
    }

}
