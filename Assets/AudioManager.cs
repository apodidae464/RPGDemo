using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] sfx;
    public AudioSource[] bgm;

    private bool isplay = false;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void swichSound()
    {
        string scene = SceneManager.GetActiveScene().name;
        switch(scene)
        {
            case "Begin":
                playBGM(4);
                break;
            case "Shopee":
                playBGM(4);
                break;
            case "DragonsDungeon":
                playBGM(0);
                break;
            default:
                playBGM(4);
                break;
        }
    }

    public void playSFX(int sfxId)
    {
        if(sfxId < sfx.Length)
            sfx[sfxId].Play();
    }

    public void playBGM(int musicId)
    {
        if(!bgm[musicId].isPlaying)
        {
            stopMusic();

            if (musicId < bgm.Length)
                bgm[musicId].Play();
        }
    }

    public void stopMusic()
    {
        for(int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }
}
