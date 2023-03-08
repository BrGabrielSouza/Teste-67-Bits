using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    // ======================================= Variaveis ================================================
    
    
    Animator Anim;   //Pega o animador em cena
    CapsuleCollider Col; // Pega o colider em cena
    float timeAnim = 2; // Tempo para ocorrer a animação
    public BoxCollider BoxPile; // Pega o colider em cena


    // ======================================= Void Start ================================================

    void Start()
    {
        Anim = GetComponent<Animator>();                  // Pega o animador dentro do proprio gameobject
        Col = GetComponent<CapsuleCollider>();            // Pega o colider dentro do proprio gameobject
    }


    // ======================================= Void Update ================================================

    void Update()
    {
       if(timeAnim <= 1)                         //Se o tempo for menor ou igual a 1:
        {
            timeAnim -= 1 * Time.deltaTime;      // Tempo desce 1 por segundo
        }
       if(timeAnim <= 0)                         // Se o tempo for menor ou igual a 0:
        {
            Anim.enabled = false;                // Animação do NPC para e vai pra Ragdoll
        }
       if(timeAnim <= -0.5f)                     // Se o tempo for menor ou igual a -0,5:
        {
            BoxPile.enabled = true;              // O colider para o player empilhar o corpo aparece
        }

       // Essas 3 estapas foram feitas para dar tempo ao jogador ver cada coisa ocorrendo.
       // Se não tiver isso o npc nocauteado vai direto pra pilha de corpos atras do player
    }


    // ======================================= Triggers ================================================
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")                // Ao colidir com o player:
        {
            timeAnim = 0.1f;                      // Tempo de 2 vai para 0.1 (Isso faz o tempo correr e gerar algumas ações no update)
           
            Col.enabled = false;                  // Desabilita o Colider de soco;
        }
    }

  
}
