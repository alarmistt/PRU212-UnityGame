using UnityEngine.Audio;
using UnityEngine;

public class DSounds : MonoBehaviour
{
    public AudioSource CoinAudioSoure;
    public AudioSource SwordSoure;
    public AudioSource HealManaSource;
    
    public void PlayCoinSound()
    {
        CoinAudioSoure.Play();
    }

    public void PlaySwordSound()
    {
        SwordSoure.Play();
    }

    public void HealManaheath ()
    {
        HealManaSource.Play();
    }
}
