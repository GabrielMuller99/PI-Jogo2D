using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controleSons : MonoBehaviour
{
    public static AudioClip jogadorTomaDano;
    public static AudioClip jogadorIdle;
    public static AudioClip jogadorPula;
    public static AudioClip jogadorMorre;
    public static AudioClip jogadorEspinho;
    public static AudioClip jogadorGira;
    public static AudioClip jogadorSpark;

    static AudioSource audioSrc;

    void Start()
    {
        jogadorTomaDano = Resources.Load<AudioClip>("dano");
        jogadorIdle = Resources.Load<AudioClip>("idle");
        jogadorPula = Resources.Load<AudioClip>("pulo");
        jogadorMorre = Resources.Load<AudioClip>("morte");
        jogadorEspinho = Resources.Load<AudioClip>("espinho");
        jogadorGira = Resources.Load<AudioClip>("andando");
        jogadorSpark = Resources.Load<AudioClip>("spark");

        audioSrc = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public static void TocarAudio (string nome)
    {
        switch (nome)
        {
            case "dano":
                audioSrc.PlayOneShot(jogadorTomaDano);
                break;

            case "idle":
                audioSrc.PlayOneShot(jogadorIdle);
                break;

            case "pulo":
                audioSrc.PlayOneShot(jogadorPula);
                break;

            case "morte":
                audioSrc.PlayOneShot(jogadorMorre);
                break;

            case "espinho":
                audioSrc.PlayOneShot(jogadorEspinho);
                break;

            case "andando":
                audioSrc.PlayOneShot(jogadorGira);
                break;

            case "spark":
                audioSrc.PlayOneShot(jogadorSpark);
                break;

            default:
                break;
        }
    }
}
