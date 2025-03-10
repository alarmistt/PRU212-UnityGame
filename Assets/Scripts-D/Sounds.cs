using UnityEngine.Audio;
using UnityEngine;

public class DSounds : MonoBehaviour
{
    public AudioSource CoinAudioSoure;
    public AudioSource SwordSoure;
    
    public void PlayCoinSound()
    {
        CoinAudioSoure.Play();
    }

    public void PlaySwordSound()
    {
        SwordSoure.Play();
    }
}
