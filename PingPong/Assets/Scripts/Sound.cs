using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]/*Permite que a nossa Classe seja vista no Inspector*/
public class Sound 
{
    [Range(0f,1f)]
    public float volume;

    [Range(0.1f, 3f)]
    public float pitch;

    public string name;

    public AudioClip clip;

    public bool loop;
    //public bool mute;

    [HideInInspector]//é uma variavel que apesar de ser publica queremos que nao apareca no inspector
    public AudioSource source;
}
