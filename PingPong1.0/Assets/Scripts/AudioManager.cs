using UnityEngine.Audio;
using System;//esta tag permite-nos usar uma sintaxe de procura numa array de uma forma mais rapida
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;//instancia 

    public Sound[] sounds; /*Criacao de um vector de objectos da Classe Sound*/

    private void Awake()
    {
        /*Esta condicao serve para verificar se quando mudamos de scene caso existam 2 AudioManager isso ja 
         nao vai ser possivel graças a este codigo pois se existir 1 o outro já será destruido.*/
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);//opcao para a musica continuar sem cortes caso haja uma mudança de scene
        foreach(Sound s in sounds) //loop
        {
            s.source = gameObject.AddComponent<AudioSource>();//adicionamos AudioSource ao clip armazenado na lista de sons da Classe Sounds

            /*Estamos a definir que o que esta colocado no inspector no AudioManager fique igual quando criamos um AudioSource*/
            s.source.clip = s.clip;//audio
            s.source.volume = s.volume;//volume
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;//se se repete ao fim de acabar
            
        }

    }
    public void Start()
    {
        Play("Theme");
        
    }
    void OnLevelWasLoaded()
    {
        Play("Theme");
    }

    public void EndSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        /*Se nao encontrar colocamos uma mensagem de aviso!*/
        if (s == null)
        {
            Debug.LogWarning("O nome introduzido " + name + " nao foi encontrado!");//mensagem que aparece quando colocamos um nome como arguemento da funcao play que nao existe na lista
            return;
        }
        s.source.Stop();
    }
      
    
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        /*Se nao encontrar colocamos uma mensagem de aviso!*/
        if(s == null)
        { 
            Debug.LogWarning("O nome introduzido " + name + " nao foi encontrado!");//mensagem que aparece quando colocamos um nome como arguemento da funcao play que nao existe na lista
            return;
        }
        
        s.source.Play();
    }
    public void IncreasePitch(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        /*Se nao encontrar colocamos uma mensagem de aviso!*/
        if (s == null)
        {
            Debug.LogWarning("O nome introduzido " + name + " nao foi encontrado!");//mensagem que aparece quando colocamos um nome como arguemento da funcao play que nao existe na lista
            return;
        }
         s.source.pitch +=  0.02f;//casting
    }
}
