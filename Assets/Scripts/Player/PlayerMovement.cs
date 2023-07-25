using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Transform playerCarImage;

        internal void MoveToPosition(Transform newPosition)
        {
            playerCarImage.SetParent(newPosition);
            playerCarImage.localPosition = Vector3.zero;
        }

        internal void EnableColliderOnPlayerImage()
        {
            playerCarImage.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}