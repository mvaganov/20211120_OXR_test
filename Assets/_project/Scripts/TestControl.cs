using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestControl : MonoBehaviour {
	Rigidbody rb;
	public Transform _camera;
	public bool running = false;
	public float walkSpeed = 2, runSpeed = 5;
	public TMP_Text controlsText;
	public TMP_Text inputText;
	Vector2 moveVector;
	InputControl moveControl = null;
	Test_InputActions inputActions;
	public void Start() {
		rb = GetComponent<Rigidbody>();
		//inputActions = new Test_InputActions();
		//inputActions.Agent.Enable();
		//inputActions.Agent.Jump.performed += Jump_performed;
		//inputActions.Agent.Movement.Enable();
		////inputActions.Agent.Movement.performed += Movement_performed;
		////inputActions.Agent.Movement.canceled += Movement_performed;
		//inputActions.Agent.RunSpeed.started += c => running = true;
		//inputActions.Agent.RunSpeed.canceled += c => running = false;
		//controlsText.text = inputActions.bindings.JoinToString("\n");
	}

	private void Update() {
		if (inputActions != null) {
			moveVector = inputActions.Agent.Movement.ReadValue<Vector2>();
		}
		//InputAction movement = inputActions.Agent.Movement;
		//ProcessMovement(m);
		//inputText.text = m.ToString() + " " + movement.enabled + " " + movement.activeControl + " ";

		//moveVector = Vector2.zero;
		//if (Keyboard.current.wKey.ReadValue() != 0) { moveVector.y = 1; }
		//if (Keyboard.current.aKey.ReadValue() != 0) { moveVector.x =-1; }
		//if (Keyboard.current.sKey.ReadValue() != 0) { moveVector.y =-1; }
		//if (Keyboard.current.dKey.ReadValue() != 0) { moveVector.x = 1; }

		//if (Input.GetButtonDown("Jump")) { DoJump(); }

		inputText.text = Time.time + " " + moveVector.ToString() + " ";
	}
	private void FixedUpdate() {
		ProcessMovement(moveVector);
	}
	public bool MoveLogic(InputAction.CallbackContext context, ref float v, float coefficient) {
		switch (context.phase) {
		case InputActionPhase.Canceled: v = 0; return true;
		case InputActionPhase.Performed: v = context.ReadValue<float>() * coefficient; return true;
		}
		return false;
	}
	public void Move_RunHold(InputAction.CallbackContext context) {
		switch (context.phase) {
		case InputActionPhase.Canceled: running = false; return;
		case InputActionPhase.Started: running = true; return;
		}
	}
	public void Move_Up(InputAction.CallbackContext context) { MoveLogic(context, ref moveVector.y, 1); }
	public void Move_Left(InputAction.CallbackContext context) { MoveLogic(context, ref moveVector.x, -1); }
	public void Move_Down(InputAction.CallbackContext context) { MoveLogic(context, ref moveVector.y, -1); }
	public void Move_Right(InputAction.CallbackContext context) { MoveLogic(context, ref moveVector.x, 1); }
	public void Jump_performed(InputAction.CallbackContext context) {
		Debug.Log("jump");
		DoJump();
	}
	public void DoJump() {
		rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
	}
	public void Movement_performed(InputAction.CallbackContext context) {
		ProcessMovement(context.ReadValue<Vector2>());
	}
	public void ProcessMovement(Vector2 m) {
		Transform t = _camera;
		Vector3 v = m.x * t.right + m.y * t.forward;
		v *= (running) ? runSpeed : walkSpeed;
		v.y = rb.velocity.y;
		rb.velocity = v;
		//Debug.Log(m+" "+rb.velocity);
	}
	public void Movement(InputAction.CallbackContext context) {
		switch (context.phase) {
		case InputActionPhase.Canceled: moveVector = Vector2.zero; return;
		case InputActionPhase.Performed: moveVector = context.ReadValue<Vector2>(); return;
		}
	}
	public void Jump(InputAction.CallbackContext context) {
		if (!context.performed) { return; }
		Jump_performed(context);
	}
}
