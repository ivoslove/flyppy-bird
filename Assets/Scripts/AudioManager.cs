using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App.Dispatch;
using UnityEngine;

public class AudioManager:IGameBehaviour
{

    private bool m_enable = true;

    private AudioSource m_bgMusic;
    private AudioSource m_JumpMusic;
    private AudioSource m_scoreMusic;
    private AudioSource m_gameOverMusic;
    

    public AudioManager()
    {
        var audioManagerObj = GameObject.Find("AudioManager");
        var audioSources = audioManagerObj.GetComponents<AudioSource>();
        m_bgMusic = audioSources.FirstOrDefault(p => p.clip.name.Equals("BGMusic_mario"));
        m_JumpMusic = audioSources.FirstOrDefault(p => p.clip.name.Equals("Jump"));
        m_scoreMusic = audioSources.FirstOrDefault(p => p.clip.name.Equals("Score"));
        m_gameOverMusic = audioSources.FirstOrDefault(p => p.clip.name.Equals("GameOver"));

        Dispatcher.Listener<bool>("OpenOrCloseMusic",OpenOrCloseMusic);
    }

    public void Jump()
    {
        m_JumpMusic.Play();
    }

    public void Score()
    {
        m_scoreMusic.Play();
    }

    public void GameOver()
    {
        m_scoreMusic.Stop();
        m_JumpMusic.Stop();
        m_bgMusic.Stop();
       m_gameOverMusic.Play();
    }

    public void Pause()
    {
     
    }

    public void PlayBGMusic()
    {
        m_bgMusic.Play();
    }

    private void OpenOrCloseMusic(bool isOpen)
    {
        m_enable = isOpen;

            m_bgMusic.mute = !isOpen;
            m_JumpMusic.mute = !isOpen;
            m_gameOverMusic.mute = !isOpen;
            m_scoreMusic.mute = !isOpen;

  

    }

    public void ScalePlaySpeed(float scale)
    {
        m_bgMusic.pitch = scale;
        m_JumpMusic.pitch = scale;
        m_scoreMusic.pitch = scale;


    }
}
