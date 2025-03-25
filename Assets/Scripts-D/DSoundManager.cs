using UnityEngine;
using UnityEngine.UI;

public class DSoundManager : MonoBehaviour
{
    [SerializeField] Slider Slider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        } else
        {
            Load();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeVolume()
    {
        AudioListener.volume = Slider.value;
    }

    private void Load()
    {
        Slider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", Slider.value);
    }
}
