using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] AudioSource Music;
    [SerializeField] AudioSource SoundEffects;

    [Header("Music")]
    [SerializeField] AudioClip goblinFightTheme;
    [SerializeField] AudioClip mainMenuTheme;

    [Header("Sound Effects")]
    [Header("Bows/Arrows")]
    [SerializeField] AudioClip arrowHit1;
    [SerializeField] AudioClip arrowHit2;
    [SerializeField] AudioClip arrowHit3;
    [SerializeField] AudioClip arrowHit4;
    [SerializeField] AudioClip bowFire1;
    [SerializeField] AudioClip bowFire2;
    [SerializeField] AudioClip bowFire3;
    [SerializeField] AudioClip bowGroup;

    [Header("Victory/Defeat")]
    [SerializeField] AudioClip victory;
    [SerializeField] AudioClip gameOver;

    [Header("Gob Sounds")]
    [SerializeField] AudioClip goblinDeath1;
    [SerializeField] AudioClip goblinDeath2;
    [SerializeField] AudioClip goblinDeath3;
    [SerializeField] AudioClip goblinDeath4;
    [SerializeField] AudioClip horn;
    [SerializeField] AudioClip orcRoar;
    [SerializeField] AudioClip slash;
    [SerializeField] AudioClip sliceGory1;
    [SerializeField] AudioClip sliceGory2;
    [SerializeField] AudioClip sliceGory3;

    [Header("Human Sounds")]
    [SerializeField] AudioClip soldierDeath1;
    [SerializeField] AudioClip soldierDeath2;
    [SerializeField] AudioClip soldierDeath3;
    [SerializeField] AudioClip soldierDeath4;
    [SerializeField] AudioClip soldierDeath5;
    [SerializeField] AudioClip soldierDeath6;

    [Header("UI sounds")]
    [SerializeField] AudioClip buttonClick1;
    [SerializeField] AudioClip buttonClick2;

    private void Start()
    {
        Music.loop = true;
        Music.volume = 1f;
        SoundEffects.volume = 0.5f;
    }

    /**********************
     * FX Functions Below *
     **********************/

    public void FXArrowHit()
    {
        int randSound = Random.Range(0, 4);

        switch(randSound)
        {
            case 0:
                SoundEffects.PlayOneShot(arrowHit1);
                break;
            case 1:
                SoundEffects.PlayOneShot(arrowHit2);
                break;
            case 2:
                SoundEffects.PlayOneShot(arrowHit3);
                break;
            case 3:
                SoundEffects.PlayOneShot(arrowHit4);
                break;
            default:
                SoundEffects.PlayOneShot(arrowHit1);
                break;
        }
    }

    public void FXBowFire()
    {
        int randSound = Random.Range(0, 3);

        switch (randSound)
        {
            case 0:
                SoundEffects.PlayOneShot(bowFire1);
                break;
            case 1:
                SoundEffects.PlayOneShot(bowFire2);
                break;
            case 2:
                SoundEffects.PlayOneShot(bowFire3);
                break;
            default:
                SoundEffects.PlayOneShot(bowFire1);
                break;
        }
    }

    public void FXBowGroup()
    {
        SoundEffects.PlayOneShot(bowGroup);
    }

    public void FXVictory()
    {
        SoundEffects.PlayOneShot(victory);
    }

    public void FXDefeat()
    {
        SoundEffects.PlayOneShot(gameOver);
    }

    public void FXGoblinDeath()
    {
        int randSound = Random.Range(0, 4);

        switch (randSound)
        {
            case 0:
                SoundEffects.PlayOneShot(goblinDeath1);
                break;
            case 1:
                SoundEffects.PlayOneShot(goblinDeath2);
                break;
            case 2:
                SoundEffects.PlayOneShot(goblinDeath3);
                break;
            case 3:
                SoundEffects.PlayOneShot(goblinDeath4);
                break;
            default:
                SoundEffects.PlayOneShot(goblinDeath1);
                break;
        }
    }

    public void FXHorn()
    {
        SoundEffects.PlayOneShot(horn);
    }

    public void FXRoar()
    {
        SoundEffects.PlayOneShot(orcRoar);
    }

    public void FXSlash()
    {
        int randSound = Random.Range(0, 4);

        switch (randSound)
        {
            case 0:
                SoundEffects.PlayOneShot(slash);
                break;
            case 1:
                SoundEffects.PlayOneShot(sliceGory1);
                break;
            case 2:
                SoundEffects.PlayOneShot(sliceGory2);
                break;
            case 3:
                SoundEffects.PlayOneShot(sliceGory3);
                break;
            default:
                SoundEffects.PlayOneShot(slash);
                break;
        }
    }

    public void FXSoldierDeath()
    {
        int randSound = Random.Range(0, 6);

        switch (randSound)
        {
            case 0:
                SoundEffects.PlayOneShot(soldierDeath1);
                break;
            case 1:
                SoundEffects.PlayOneShot(soldierDeath2);
                break;
            case 2:
                SoundEffects.PlayOneShot(soldierDeath3);
                break;
            case 3:
                SoundEffects.PlayOneShot(soldierDeath4);
                break;
            case 4:
                SoundEffects.PlayOneShot(soldierDeath5);
                break;
            case 5:
                SoundEffects.PlayOneShot(soldierDeath6);
                break;
            default:
                SoundEffects.PlayOneShot(soldierDeath1);
                break;
        }
    }

    public void FXButtonClick()
    {
        int randSound = Random.Range(0, 2);

        switch (randSound)
        {
            case 0:
                SoundEffects.PlayOneShot(buttonClick1);
                break;
            case 1:
                SoundEffects.PlayOneShot(buttonClick2);
                break;
            default:
                SoundEffects.PlayOneShot(buttonClick1);
                break;
        }
    }

    public void BGMainTheme()
    {
        Music.clip = goblinFightTheme;
        Music.volume = 1f;
        Music.loop = true;
        Music.Play();
    }

    public void BGMenuTheme()
    {
        Music.clip = mainMenuTheme;
        Music.volume = 1f;
        Music.loop = true;
        Music.Play();
    }

    public void Pause()
    {
        Music.Pause();
        SoundEffects.Pause();
    }

    bool ducked = false;
    public void ToggleDuck()
    {
        if (!ducked)
        {
            Music.volume = 0.2f;
            SoundEffects.volume = 0.1f;
        } else
        {
            Music.volume = 1f;
            SoundEffects.volume = 0.5f;
        }

        ducked = !ducked;
    }

    public void UnPause()
    {
        Music.UnPause();
        SoundEffects.UnPause();
    }

    public void Stop()
    {
        Music.Stop();
        SoundEffects.Stop();
    }

    public void FXStop()
    {
        SoundEffects.Stop();
    }
}