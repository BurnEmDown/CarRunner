using System;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerInput playerInput;
        private PlayerMovement playerMovement;
        private PlayerModel playerModel;

        [SerializeField] private int laneAmount = 5; // default lane amount, can be adjusted later if needed
        
        // the player controller probably shouldn't handle this but I'm choosing to do it this way to save time
        [SerializeField] private GameObject laneParentObject;
        

        private void Awake()
        {
            playerInput = new PlayerInput();
            playerModel = new PlayerModel(laneAmount);
            playerModel.AddAllLocationObjectsFromParentObject(laneParentObject);
            playerMovement = GetComponent<PlayerMovement>();
            
            // There was an issue which I couldn't find a better fix for where the player would immediately trigger 
            // collisions with the enemy cars, which trigger game over immediately, so I decided to enable the 
            // players' collision only 2 seconds after it activates
            Invoke(nameof(EnableCollider), 2f);
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

        private void EnableCollider()
        {
            // playerMovement holds reference to playImage so I decided to call the method from it
            playerMovement.EnableColliderOnPlayerImage();
        }

        private void OnRightButtonPressed()
        {
            if (playerModel.IsRightmostPosition())
                return;
            
            Transform newPosition = playerModel.MoveRight();
            playerMovement.MoveToPosition(newPosition);
        }
        
        private void OnLeftButtonPressed()
        {
            if (playerModel.IsLeftmostPosition())
                return;

            Transform newPosition = playerModel.MoveLeft();
            playerMovement.MoveToPosition(newPosition);
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
