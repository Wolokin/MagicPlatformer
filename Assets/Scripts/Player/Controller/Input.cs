using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System;

namespace Controller
{

    public class Input : MonoBehaviour
    {
        private InputActionAsset inputAsset;
        private InputActionMap inputs;
        private PlayerInput player;
        private Rigidbody2D rigidBody;

        public InputAction move { get; private set; }
        public InputAction jump { get; private set; }
        public InputAction dash { get; private set; }
        public InputAction grab { get; private set; }
        public InputAction resetPosition { get; private set; }
        private InputAction _look;

        public Vector2 lookDir { get
        {
            var controller = player.currentControlScheme;
            var look = _look.ReadValue<Vector2>();
            if (controller.Equals("Gamepad"))
            {
                return look.normalized;
            } else if(controller.Equals("Keyboard&Mouse"))
            {
                return (look - rigidBody.position).normalized;
            }
            return Vector2.zero;
        } }

        public Vector2 moveRaw { get { 
            var mv = move.ReadValue<Vector2>();
                return new Vector2(Math.Sign(mv.x), Math.Sign(mv.y));
            } }

        private void Awake()
        {
            player = this.GetComponent<PlayerInput>();
            inputAsset = player.actions;
            inputs = inputAsset.FindActionMap("Player");
            rigidBody = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            inputs.Enable();

            move = inputs.FindAction("Move");
            jump = inputs.FindAction("Jump");
            dash = inputs.FindAction("Dash");
            grab = inputs.FindAction("Grab");
            resetPosition = inputs.FindAction("ResetPosition");
            _look = inputs.FindAction("Look");
        }

        private void OnDisable()
        {
            inputs.Disable();
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
