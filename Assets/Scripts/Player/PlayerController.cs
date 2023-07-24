using System;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerInput playerInput;
        private PlayerMovement playerMovement;
        private PlayerModel playerModel;


        private void Awake()
        {
            playerInput = new PlayerInput();
            playerModel = new PlayerModel();
        }

        private void OnEnable()
        {
            UnsubscribeFromEvents();
            SubscribeToEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        private void Update()
        {
            playerInput.CheckInput();
        }

        private void OnRightButtonPressed()
        {
            
        }
        
        private void OnLeftButtonPressed()
        {
            
        }

        private void SubscribeToEvents()
        {
            playerInput.RightButtonPressed += OnRightButtonPressed;
            playerInput.LeftButtonPressed += OnLeftButtonPressed;
        }

        private void UnsubscribeFromEvents()
        {
            playerInput.RightButtonPressed -= OnRightButtonPressed;
            playerInput.LeftButtonPressed -= OnLeftButtonPressed;
        }
    }
}
