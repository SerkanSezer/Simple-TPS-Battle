using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool get;
		public bool pause;
		public bool select;
		public bool sprint;
        public bool aim;
        public bool shoot;

        [Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}
		public void OnGet(InputValue value)
		{
			GetInput(value.isPressed);
		}
		public void OnPause(InputValue value)
		{
			PauseInput(value.isPressed);
		}
		
		public void OnSelect(InputValue value)
		{
			SelectInput(value.isPressed);
		}
        public void OnShoot(InputValue value) {
            ShootInput(value.isPressed);
        }

        public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
		
		public void OnAim(InputValue value)
		{
			AimInput(value.isPressed);
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}
		public void GetInput(bool newGetState)
		{
			get = newGetState;
		}
		public void PauseInput(bool newPauseState)
		{
			pause = newPauseState;
		}
		public void SelectInput(bool newSelectState)
		{
			select = newSelectState;
		}
        public void ShootInput(bool newShootState) {
            shoot = newShootState;
        }

        public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
        public void AimInput(bool newAimState) {
            aim = newAimState;
        }

		//private void OnApplicationFocus(bool hasFocus) {
		//	SetCursorState(cursorLocked);
		//}

		public void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}