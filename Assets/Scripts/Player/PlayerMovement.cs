using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {

        internal void MoveToPosition(Transform newPosition)
        {
            transform.SetParent(newPosition);
            transform.localPosition = Vector3.zero;
        }
    }
}