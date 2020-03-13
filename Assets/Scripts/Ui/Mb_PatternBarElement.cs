using UnityEngine;

public class Mb_PatternBarElement : MonoBehaviour
{
    public ParticleSystem[] FX;
    
    public void PlayFX(int index)
    {
        FX[index].Play();
    }
}
