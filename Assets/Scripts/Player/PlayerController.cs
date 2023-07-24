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
