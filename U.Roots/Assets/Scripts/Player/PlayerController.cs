using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputs _inputs;

    private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private AudioManager am;

    [HideInInspector] public float movementInput;
    [HideInInspector] public bool isMoving;
    [Header("Movement")]
    [SerializeField] public float moveSpeed;

    [Header("Attack")]
    private bool canAttack = true;
    [SerializeField] private Collider2D meleeBox;
    [SerializeField] private SpriteRenderer sickle;
    [SerializeField] private float attackDelay;
    private float attackTimer;
    
    void OnEnable()
    {
        _inputs.Actions.Enable();
        _inputs.UI.Enable();
    }
    void OnDisable()
    {
        _inputs.Actions.Disable();
        _inputs.UI.Disable();
    }
    
    void SubscribeInputEvents()
    {
        _inputs = new PlayerInputs();
        // input events
        _inputs.Actions.HorizontalMovements.performed += OnMovement;
        _inputs.Actions.HorizontalMovements.canceled += OnMovement;
        _inputs.Actions.Attack.performed += OnAttack;
    }

    void AssignComponents()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Awake()
    {
        SubscribeInputEvents();
        AssignComponents();
    }

    void FixedUpdate()
    {
        rb.MovePosition(transform.position + movementInput * transform.right * moveSpeed * Time.fixedDeltaTime);
    }

    private void Update()
    {
        if (!canAttack)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer > attackDelay)
            {
                canAttack = true;
            }
        }

        sr.gameObject.GetComponent<Animator>().Play(isMoving ? "PlayerWalking": "PlayerIdling");
    }

    void OnMovement(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<float>();
        isMoving = movementInput != 0;
        sr.flipX = movementInput > 0;
    }

    void OnAttack(InputAction.CallbackContext ctx)
    {
        if (canAttack)
        {
            meleeBox.gameObject.SetActive(true);
            sickle.gameObject.GetComponent<Animator>().Play("Sickle");
            Invoke("TurnOffMeleeBox", .15f);
            canAttack = false;
            attackTimer = 0;
            am.PlaySfx(UnityEngine.Random.Range(0f, 1f) >= .5f ? AudioManager.Sound.Slash2 : AudioManager.Sound.SlashWind);
        }
    }

    void TurnOffMeleeBox()
    {
        meleeBox.gameObject.SetActive(false);
    }
}
