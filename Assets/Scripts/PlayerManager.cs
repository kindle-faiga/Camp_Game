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
            if (!bulletManager.GetIsActive() && Input.GetButtonDown("Shot"))
            {
                StartCoroutine(bulletManager.Shot());
            }
        }

        private void FixedUpdate()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            if(Mathf.Abs(x) > Mathf.Abs(z))
            {
                if(x < 0)
                {
                    transform.localEulerAngles = new Vector3(0, -90.0f, 0);
                }
                else
                {
					transform.localEulerAngles = new Vector3(0, 90.0f, 0);
                }
            }
            else
            {
				if (z < 0)
				{
					transform.localEulerAngles = new Vector3(0, 180.0f, 0);
				}
				else
				{
					transform.localEulerAngles = new Vector3(0, 0.0f, 0);
				}
            }

            Vector3 distance = new Vector3(x, 0, z);

            rigidbodyManager.Move(distance);
        }
    }
}
