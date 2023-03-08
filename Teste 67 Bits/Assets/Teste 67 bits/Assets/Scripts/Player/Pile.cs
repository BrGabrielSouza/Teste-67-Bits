using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pile : MonoBehaviour
{
    // ======================================= Variaveis ================================================

    public GameObject NPC; // pega o gameobject npc selecionado dentro da cena

    PlayerScript ScriptPlayer; // pega o script do player


    // ======================================= Void Start ================================================

    private void Start()
    {
        ScriptPlayer = GameObject.Find("Player").GetComponent<PlayerScript>(); // procura o gameobject com nome player e coleta o script
    }


    // ======================================= Triggers ================================================

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")                                 // Se a tag em Colisao for Player:
        {
            if (ScriptPlayer.CountPile < ScriptPlayer.LimitPile)   // se a pilha de corpos for menor que o limite:
            {
                Destroy(NPC);                                      // Destroi o Corpo no Chao
            }
        }
    }
}
