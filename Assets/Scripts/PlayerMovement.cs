using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    private PlayerInputActions inputActions;
    private Vector2 moveInput;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;

    private System.Action<InputAction.CallbackContext> performedCallback;
    private System.Action<InputAction.CallbackContext> canceledCallback;

    // Dash Variables
    public float dashDistance = 4f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    public bool isDashing = false;
    private bool canDash = true;

    private void Awake() {
        inputActions = InputManager.Instance;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        performedCallback = ctx => moveInput = ctx.ReadValue<Vector2>();
        canceledCallback = ctx => moveInput = Vector2.zero;

        inputActions.Player.Move.performed += performedCallback;
        inputActions.Player.Move.canceled += canceledCallback;

        inputActions.Player.Dash.performed += ctx => Dash();
    }

    private void OnDisable() {
        inputActions.Player.Move.performed -= performedCallback;
        inputActions.Player.Move.canceled -= canceledCallback;
    }

    private void FixedUpdate() {
        if (isDashing)
            return;

        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    private void Dash() {
        if (!canDash || isDashing || moveInput == Vector2.zero)
            return;
        StartCoroutine(DashRoutine());
    }

    private IEnumerator DashRoutine() {
        isDashing = true;
        canDash = false;

        Vector2 dashDir = moveInput.normalized;
        Vector2 startPos = rb.position;
        Vector2 targetPos = startPos + dashDir * dashDistance;

        CameraShake.instance.Shake(0.1f,0.15f);

        float elapsed = 0f;
        while (elapsed < dashDuration) {
            Debug.Log(startPos);
            Debug.Log(moveInput);
            rb.MovePosition(Vector2.Lerp(startPos, targetPos, elapsed / dashDuration));
            elapsed += Time.deltaTime;
            yield return null;
        }
        rb.MovePosition(targetPos);

        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
