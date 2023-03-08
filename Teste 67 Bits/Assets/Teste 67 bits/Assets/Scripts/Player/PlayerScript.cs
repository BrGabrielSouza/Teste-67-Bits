using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    
    
    // ======================================= Variaveis ================================================


    //---------------------------------------- Caminhar do player ---------------------------------------
    
    public float speed;                                // Velocidade do Player
    public FloatingJoystick Joy;                       // Coleta o joystick
    Vector3 direction;                                 // vetor de direção
    Rigidbody rb;                                      // rigidbody do player
    Animator Anim;                                     // animação do player
    float walkV, walkH;                                // valor do caminhar do player referente a cada direção
    Quaternion look;                                   // verifica onde o player olha


    //---------------------------------------- Soco do player -------------------------------------------

    public bool punch;                                 // bool para verificar se esta dando soco


    //---------------------------------------- Pilhagem de Corpos ---------------------------------------

    public int LimitPile = 3;                          // Seta o limite da pilha de corpos

    public GameObject[] Bodypile;                      // pega os gameobjects baseados no array
    public int CountPile = 0;                          // Conta a pilha de corpos


    // --------------------------------------- Dinheiro -------------------------------------------------

    public Text GoldText;                              // Texto com valor do Ouro ()/HUD
    public int Gold;                                   // valor inteiro do Ouro
    float TimeCount;                                   // Contagem de Tempo


    // --------------------------------------- Level Up ----------------------------------------------------
    
    int playerLevel = 1;                               // numero inteiro do nivel do player
    public Text PlayerlvlText;                         // Texto do Level do Player (HUD)


    // --------------------------------------- Cores ----------------------------------------------------

    public Material PlayerMat;                         // Material do player
    public Color[] color;                              // array de cores


    // ======================================= Void Start ================================================

    private void Start()
    {
        rb = GetComponent<Rigidbody>();                // Coleta o rigidbody dentro do player
        Anim = GetComponent<Animator>();               // Coleta o Animador dentro do player

    }


    // ======================================= Void Update ===============================================

    private void Update()
    {
        if (TimeCount > 0)                             // Se o tempo for maior que 0:
        {
            TimeCount -= 1 * Time.deltaTime;           // Tempo diminui
        }
        else if (TimeCount < 0)                        // Se for menor:
        {
            TimeCount = 0;                             // Volta a 0
        }
    }


    // ======================================= Void Fixed Update =========================================
   
    public void FixedUpdate()
    {
        direction = Vector3.forward * Joy.Vertical + Vector3.right * Joy.Horizontal;
        // direção recebe vetor vezes valor dos joysticks

        rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        // Rigidbody do player recebe força pegando a direção e velocidade setados

        AnimationPlayer();                             // Atualiza a void de animação a cada update
        PunchFalse();                                  // Atualiza a void de Soco a cada update

      

    }

   
    // ======================================= Trigger Enter ================================================
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Punch")               // Se o objeto tiver a tag Punch:
        {
            Anim.SetBool("Punch", true);       // A animação de Bater ocorre
            punch = true;                      // A bool de bater fica verdadeira para ocorrer um gatilho futuro
        }
        if (other.tag == "ToPile")             // Se tiver a tag To Pile:
        {
            if (CountPile < LimitPile)         // Se a Pilha dos Corpos for menor que o limite:
            {
               
                OnPile();                      // Chama a void Onpile
            }
        }

       
    }
    // ======================================= Trigger Exit ================================================
    private void OnTriggerExit(Collider other)        // Ao Sair do Trigger:
    {
        if (other.tag == "Punch")                     // Se a tag for Punch:
        {
            Anim.SetBool("Punch", false);             // Para a Animação de Bater
            punch = false;                            // Bool punch recebe falsa
        }
    }

    public void PunchFalse()                      // Uma void para deixar a animação de Bater Falsa em Caso do trigger exit não funcionar
    {
        if (!punch)                               // Se A Bool punch for falsa:
        {
            Anim.SetBool("Punch", false);         // Animação de Bater para
        }
    }


    // ======================================= Trigger Stay ================================================

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "ToMoney") // Se a colisao tiver a tag To Money:
        {

            if (CountPile > 0 && TimeCount <= 0) // Se a Contagem da pilha for maior que 0 e o tempo for menor:
            {

                TimeCount = 0.25f;      // Tempo conta novamente
                RemovePile();           // Remove um corpo da pilha
                MoreGold(30);           // Recebe 30 ouros

            }
        }
    }
        

    // ======================================= Voids Gerais ================================================


    //---------------------------------------- Caminhar do player ---------------------------------------

    void AnimationPlayer()                             // Void que atualiza a animação de andar do player
    {
        walkV = Mathf.Lerp(walkV, Joy.Vertical, 5 * Time.deltaTime);   // atualiza o float de andar de acordo com o joystick com um pequeno atraso
        walkH = Mathf.Lerp(walkH, Joy.Horizontal, 5 * Time.deltaTime); // atualiza o float de andar de acordo com o joystick com um pequeno atraso
        Anim.SetFloat("BlendV", walkV);                                // Adiciona o float de andar a animação
        Anim.SetFloat("BlendH", walkH);                                // Adiciona o float de andar a animação
        if (Joy.Vertical != 0 || Joy.Horizontal != 0)                  // Se o joystick estiver movimentando:
        {
            look = Quaternion.LookRotation(direction);                 // Atualiza a direção do player
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, look, 10 * Time.deltaTime); // coleta a direção do look e implementa no player
    }


    // --------------------------------------- Pilhagem de corpos -------------------------------------------

    void OnPile()                                                      // Aumenta o corpos empilhados atrás do player
    {

        Bodypile[CountPile].SetActive(true);                           // Seta como ativo o corpo de acordo com o contador de pilha
        CountPile += 1;                                                // Contador de pilha recebe mais um a cada corpo que ativa
    }

    void RemovePile()                                                  // Diminui os corpos empilhados atrás do player
    {
        CountPile -= 1;                                                //  Contador de pilha recebe menos um a cada corpo que desativa
        Bodypile[CountPile].SetActive(false);                          // Seta como desativo o corpo de acordo com o contador de pilha

    }

    
    // --------------------------------------- Dinheiro --------------------------------------------------------

    void MoreGold(int GoldGain)                                        // void com variavel inteira com o valor do ganho de ouro
    {
        Gold += GoldGain;                                              // Ouro recebe o valor estipulado por quem usar a void
        GoldText.text = Gold + "";                                     // O Texto recebe o valor do ouro
    }

    public void Buy(int buy)                                           // void com variavel onde diminui o valor do ouro
    {
        Gold -= buy;                                                   // Ouro diminui o valor estipulado por quem usar a void
        GoldText.text = Gold + "";                                     // O Texto recebe o valor do ouro
    }


    // --------------------------------------- Level Up --------------------------------------------------------

    public void LevelUp()                                              // Void Para Aumentar Nivel
    {
        playerLevel += 1;                                              // Nivel recebe mais 1
        PlayerlvlText.text = "Level: " + playerLevel;                  // Texto do Nivel recebe o valor do Nivel
        LimitPile += 1;                                                // o Limite de pilha aumenta de acordo com o nivel
        ChangeMat();                                                   // Muda a cor do player
    }


    // --------------------------------------- Cores -----------------------------------------------------------

    void ChangeMat()                                                   // muda cor do player usando o array de acordo com o level
    {

        PlayerMat.color = color[playerLevel - 2];
    }
}
