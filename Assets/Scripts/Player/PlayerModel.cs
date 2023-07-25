using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerModel
    {
        private Transform[] playerLocationTransforms;
    
        private int locationIndex;
    
        public PlayerModel(int amount)
        {
            locationIndex = 2;
            playerLocationTransforms = new Transform[amount];
        }
    
        internal void AddAllLocationObjectsFromParentObject(GameObject parentObject)
        {
            int childrenAmount = parentObject.transform.childCount;
            for (int i = 0; i < childrenAmount; ++i)
                playerLocationTransforms[i] = parentObject.transform.GetChild(i);
        }
    
        internal Transform GetLocationAtPosition(int index)
        {
            return playerLocationTransforms[index];
        }
    
        internal bool IsRightmostPosition()
        {
            return locationIndex == playerLocationTransforms.Length - 1;
        }
        
        internal bool IsLeftmostPosition()
        {
            return locationIndex == 0;
        }
    
        internal Transform MoveRight()
        {
            locationIndex++;
            if (locationIndex > playerLocationTransforms.Length - 1)
            {
                locationIndex = playerLocationTransforms.Length - 1;
                Debug.LogError("Tried to move too much to the right");
                return null;
            }
            return playerLocationTransforms[locationIndex];
        }
    
        internal Transform MoveLeft()
        {
            locationIndex--;
            if (locationIndex < 0)
            {
                locationIndex = 0;
                Debug.LogError("Tried to move too much to the left");
                return null;
            }
            return playerLocationTransforms[locationIndex];
        }
    }
}

