using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer gameMixer;
    [SerializeField] private Slider musicSlider, SFXSlider;
    private const string MusicVolumeKey = "MusicVolume";
    private const string SFXVolumeKey = "SFXVolume";
    private const string MusicMutedKey = "MusicMuted";
    private const string SFXMutedKey = "SFXMuted";

    private bool isMusicMuted;
    private bool isSFXMuted;
    public Button musicBtn, SFXBtn;

    private void OnEnable()
    {
        LoadVolume();
        MusicBtnStatus();
        SFXBtnStatus();
    }

    private void LoadVolume()
    {
        if (PlayerPrefs.HasKey(MusicVolumeKey) && PlayerPrefs.HasKey(SFXVolumeKey))
        {
            musicSlider.value = PlayerPrefs.GetFloat(MusicVolumeKey);
            SFXSlider.value = PlayerPrefs.GetFloat(SFXVolumeKey);
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
        }

        LoadMutedStates();
    }

    private void LoadMutedStates()
    {
        isMusicMuted = PlayerPrefs.GetInt(MusicMutedKey, 0) == 1;
        isSFXMuted = PlayerPrefs.GetInt(SFXMutedKey, 0) == 1;

        ApplyMutedStates();
    }

    private void ApplyMutedStates()
    {
        float musicVolume = isMusicMuted ? -80f : PlayerPrefs.GetFloat(MusicVolumeKey);
        float sfxVolume = isSFXMuted ? -80f : PlayerPrefs.GetFloat(SFXVolumeKey);

        gameMixer.SetFloat("Music", Mathf.Log10(musicVolume) * 20);
        gameMixer.SetFloat("SFX", Mathf.Log10(sfxVolume) * 20);
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        gameMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
    }

    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        gameMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(SFXVolumeKey, volume);
    }

    public void ToggleMusic()
    {
        isMusicMuted = !isMusicMuted;
        PlayerPrefs.SetInt(MusicMutedKey, isMusicMuted ? 1 : 0);
        ApplyMutedStates();
        MusicBtnStatus();
    }

    public void ToggleSFX()
    {
        isSFXMuted = !isSFXMuted;
        PlayerPrefs.SetInt(SFXMutedKey, isSFXMuted ? 1 : 0);
        ApplyMutedStates();
        SFXBtnStatus();
    }

    private void MusicBtnStatus()
    {
        if (PlayerPrefs.GetInt(MusicMutedKey) == 1)
        {
            musicBtn.GetComponent<Image>().color = new Color(119f / 255f, 119f / 255f, 119f / 255f);
        }
        else if (PlayerPrefs.GetInt(MusicMutedKey) == 0)
        {
            musicBtn.GetComponent<Image>().color = Color.white;
        }
    }

    private void SFXBtnStatus()
    {
        if (PlayerPrefs.GetInt(SFXMutedKey) == 1)
        {
            SFXBtn.GetComponent<Image>().color = new Color(119f / 255f, 119f / 255f, 119f / 255f);
        }
        else if (PlayerPrefs.GetInt(SFXMutedKey) == 0)
        {
            SFXBtn.GetComponent<Image>().color = Color.white;
        }
    }

}