using UnityEngine;

namespace CampGame
{
    [RequireComponent(typeof(RigidbodyManager))]
    public class PlayerManager : MonoBehaviour
    {
        private RigidbodyManager rigidbodyManager;
		private Transform shotPoint;
        private BulletManager bulletManager;

        private void Start()
        {
            rigidbodyManager = GetComponent<RigidbodyManager>();
            shotPoint = transform.GetChild(0);
            GameObject bullet = Instantiate(Resources.Load("Prefabs/Shot", typeof(GameObject))) as GameObject;
            bulletManager = bullet.GetComponentInChildren<BulletManager>();
        }

        private void Update()
        {
            if (!bulletManager.GetIsActive() && Input.GetButtonDown("Shot"))
            {
                StartCoroutine(bulletManager.Shot(shotPoint.position, transform.eulerAngles.y));
            }
        }

        private void FixedUpdate()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            if(Mathf.Abs(x) > Mathf.Abs(z))
            {
                if (x < -0.1f)
                {
                    transform.eulerAngles = new Vector3(0, -90.0f, 0);
                }
                else if (0.1f < x)
                {
					transform.eulerAngles = new Vector3(0, 90.0f, 0);
                }
            }
            else
            {
                if (z < -0.1f)
				{
					transform.eulerAngles = new Vector3(0, 180.0f, 0);
				}
				else if (0.1f < z)
				{
					transform.eulerAngles = new Vector3(0, 0.0f, 0);
				}
            }

            Vector3 distance = new Vector3(x, 0, z);

            rigidbodyManager.Move(distance);
        }
    }
}
