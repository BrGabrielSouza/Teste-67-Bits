using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMachine : MonoBehaviour
{

    // ======================================= Variaveis ================================================
      
    public Slider SliderLvl;   // Slider da Maquina
    PlayerScript playerScript; // Script Player
    float SmaxValue = 50;      // Valor Maximo do Slider



    // ======================================= Void Start ===============================================
    private void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>(); //Pega o Script do Player
    }

    // ======================================= Void Update ==============================================
    private void Update()
    {
        SliderLvl.maxValue = SmaxValue;            // Valor maximo do Slider segue o Valor Maximo setado

        if (SliderLvl.value == SliderLvl.maxValue) // Se o valor coletado do slider atingir o maximo ocorre:
        {
            SliderLvl.value = 0;                   // O valor do slider volta a 0
            SmaxValue += 25;                       // O valor maximo cresce em +25
            playerScript.LevelUp();                // Aumenta Level
        }

    }

    // ======================================= Triggers ==================================================
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && playerScript.Gold > 0) // Se a tag da colisao for player e o ouro for maior que 0:
        {
            playerScript.Buy(10);                     // Gasta 10 de ouro por tempo que o player permanecer no trigger
            SliderLvl.value += 10;                    // Aumenta 10 de valor no slider por tempo permanecido no trigger
        }
    }
}
