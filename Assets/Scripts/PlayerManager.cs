using UnityEngine;

namespace CampGame
{
    [RequireComponent(typeof(RigidbodyManager))]
    public class PlayerManager : MonoBehaviour
    {
        private RigidbodyManager rigidbodyManager;
        private BulletManager bulletManager;

        private void Start()
        {
            rigidbodyManager = GetComponent<RigidbodyManager>();
            bulletManager = GetComponentInChildren<BulletManager>();
        }

        private void Update()
        {
            if (!bulletManager.GetIsActive() && Input.GetButton("Shot"))
            {
                StartCoroutine(bulletManager.Shot());
            }
        }

        private void FixedUpdate()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 distance = new Vector3(x, 0, z);

            rigidbodyManager.Move(distance);
        }
    }
}
