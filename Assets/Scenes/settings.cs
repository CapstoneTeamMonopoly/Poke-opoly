using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class settings : MonoBehaviour
{
    [SerializeField] Slider volumeSlider = null;
   
    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            load();
        }
        else
        {
            load();
        }
    }

    public void changeVol()
    {
        AudioListener.volume = volumeSlider.value;
        save();
    }

    private void load()
    {
        PlayerPrefs.GetFloat("musicVolume", volumeSlider.value);
    }
    
    private void save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
