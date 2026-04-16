using UnityEngine;
using UnityEngine.InputSystem; // Sistema de input novo do Unity (teclado, controle, etc)

public class PlayerMovement : MonoBehaviour
{
    // =========================
    // COMPONENTES DO UNITY
    // =========================

    private CharacterController controller;
    private Transform myCamera; 
    private Animator animator; 

    // =========================
    // CONFIGURAÇÕES DE MOVIMENTO
    // =========================

    public float speed = 1.5f;
    public float runSpeed = 2.5f;
    public float rotationCam = 10f; 

    // =========================
    // CONFIGURAÇÕES DE CHÃO
    // =========================

    [SerializeField] private Transform foot;
    /*
     * "Foot" é um objeto vazio posicionado no pé do personagem.
     * Ele serve como ponto de verificação para saber se estamos no chão.
     */

    [SerializeField] private LayerMask colisionLayer;
    /*
     * Define quais layers contam como "chão".
     * Exemplo: Ground, Floor, etc.
     */

    private bool isGround;

    // =========================
    // GRAVIDADE E PULO
    // =========================

    private float yForce;
    /*
     * Controla toda a movimentação vertical:
     * - gravidade (queda)
     * - força do pulo (subida)
     */

    public float jumpForce = 10f;

    // =========================
    // INICIALIZAÇÃO
    // =========================

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        myCamera = Camera.main.transform;
    }

    // =========================
    // LOOP PRINCIPAL (RODA TODO FRAME)
    // =========================

    void Update()
    {
        GroundCheck();
        Move();        
        Jump();       
    }

    // =========================
    // MOVIMENTAÇÃO
    // =========================

    public void Move()
    {
        float horizontal = 0f;
        float vertical = 0f;
        bool isRunning = false;

        // =========================
        // INPUT DO TECLADO
        // =========================

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
                horizontal = -1f;

            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
                horizontal = 1f;

            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
                vertical = 1f;

            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
                vertical = -1f;

            if (Keyboard.current.leftShiftKey.isPressed || Keyboard.current.rightShiftKey.isPressed)
            {
                isRunning = true;
            }
        }

        // Cria um vetor de movimento (X, Y, Z)
        Vector3 movimento = new Vector3(horizontal, 0, vertical);
        float currentSpeed = isRunning ? runSpeed : speed; // Define a velocidade atual (correndo ou andando)

        // Faz o movimento seguir a direção da câmera
        movimento = myCamera.TransformDirection(movimento);
        movimento.y = 0; // Garante que não suba/abaixe por causa da câmera

        // Move o personagem no plano (horizontal)
        controller.Move(movimento * currentSpeed * Time.deltaTime);

        // =========================
        // GRAVIDADE
        // =========================

        yForce += -9.81f * Time.deltaTime; // Aplica gravidade
        controller.Move(new Vector3(0, yForce, 0) * Time.deltaTime);

        // =========================
        // ROTAÇÃO DO PERSONAGEM
        // =========================

        if (movimento != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(movimento),
                Time.deltaTime * rotationCam
            );
        }

        // =========================
        // ANIMAÇÕES
        // =========================

        bool isMoving = movimento != Vector3.zero;
        bool isRunningAndMoving = isRunning && isMoving; // Correção: só corre se estiver se movendo

        animator.SetBool("Move", isMoving);     // Idle ↔ Run
        animator.SetBool("Run", isRunningAndMoving); // Walk ↔ Run
        animator.SetBool("isGround", isGround); // Controle de pulo/queda
    }

    // =========================
    // DETECÇÃO DO CHÃO
    // =========================

    public void GroundCheck()
    {
        // Verifica se existe chão perto do "foot"
        isGround = Physics.CheckSphere(foot.position, 0.2f, colisionLayer);

        // Se está no chão e caindo, reseta a força
        if (isGround && yForce < 0)
        {
            yForce = -2f; // Mantém o personagem "colado" no chão
        }
    }

    // =========================
    // PULO
    // =========================

    public void Jump()
    {
        if (Keyboard.current != null)
        {
            // Só pula se apertar espaço E estiver no chão
            if (Keyboard.current.spaceKey.wasPressedThisFrame && isGround)
            {
                // Fórmula do pulo (define a altura)
                yForce = jumpForce;

                // Dispara animação de pulo
                animator.SetTrigger("Jump");
            }
        }
    }
}