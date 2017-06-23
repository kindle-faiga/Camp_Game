using UnityEngine;

namespace CampGame
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyManager : MonoBehaviour
    {
        [SerializeField]
        private float speed = 1.0f;

        private Rigidbody rigidBody;

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
        }

        public void Move(Vector3 distance)
        {
            rigidBody.velocity = distance*speed;
        }
    }
}