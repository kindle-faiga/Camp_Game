using UnityEngine;

namespace CampGame
{
    [RequireComponent(typeof(RigidbodyManager))]
    public class PlayerManager : MonoBehaviour
    {
        public static readonly float THRESHOLD = 0.001f;
        private RigidbodyManager rigidbodyManager;
		private Transform shotPoint;
        private Transform groundPoint;
        private BulletManager bulletManager;

        private bool isGround = true;

        private void Start()
        {
            rigidbodyManager = GetComponent<RigidbodyManager>();
            shotPoint = transform.GetChild(0);
            groundPoint = transform.GetChild(1);
            GameObject bullet = Instantiate(Resources.Load("Prefabs/Shot", typeof(GameObject))) as GameObject;
            bulletManager = bullet.GetComponentInChildren<BulletManager>();
        }

        private void Update()
        {
            isGround = Physics.Linecast(transform.position, groundPoint.position);

            if (!bulletManager.GetIsActive() && Input.GetButtonDown("Shot"))
            {
                StartCoroutine(bulletManager.Shot(shotPoint.position, transform.eulerAngles.y));
            }
        }

        private void FixedUpdate()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            if (THRESHOLD < Mathf.Abs(x) || THRESHOLD < Mathf.Abs(z))
			{
				var direction = new Vector3(x, 0, z);
				transform.localRotation = Quaternion.LookRotation(direction);
			}

            if (isGround)
            {
                Vector3 distance = new Vector3(x, 0, z);

                rigidbodyManager.Move(distance);
            }
            else
            {
                rigidbodyManager.Stop();
            }
        }
    }
}
