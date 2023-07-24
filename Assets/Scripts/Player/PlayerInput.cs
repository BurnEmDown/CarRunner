using System;
using UnityEngine;

namespace Player
{
    public class PlayerInput
    {
        public event Action RightButtonPressed;
        public event Action LeftButtonPressed;
        
        public void CheckInput()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                RightButtonPressed?.Invoke();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                LeftButtonPressed?.Invoke();
            }
        }
    }

    
}