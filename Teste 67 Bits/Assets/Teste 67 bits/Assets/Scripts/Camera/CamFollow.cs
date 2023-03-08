using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    // ======================================= Variaveis ================================================

    public Transform player; // coleta o transform do player
    Vector3 posAdd; // vetor 3(xyz) que adiciona um valor a mais a posi��o do player


    // ======================================= Fixed Update ================================================

    private void FixedUpdate()
    {
        posAdd = new Vector3(player.position.x,player.position.y +8,player.position.z -7); // coleta a posi��o do player adicionando valores em Y e Z
        // O intuito desse possAdd � que a camera fique acima do player ja que o transform por si s� ficaria no ponto zero (nos p�s)
        
        transform.position = Vector3.Lerp(transform.position, posAdd, 0.1f); 
        // Pega a posic�o do posAdd e coloca na camera com um pequeno atrazo
    }
}
