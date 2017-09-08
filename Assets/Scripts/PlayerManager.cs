using System.Collections;
using UnityEngine;

namespace CampGame
{
    [RequireComponent(typeof(RigidbodyManager))]
    public class PlayerManager : MonoBehaviour
    {
        public static readonly float THRESHOLD = 0.1f;
        [SerializeField]
        private float shotTime = 1.0f;
		[SerializeField]
		private float deadTime = 2.0f;
        private float respornTime = 0.5f;
        [SerializeField]
        private int id = 0;
        private HealthManager healthManager;
        private RigidbodyManager rigidbodyManager;
		private Transform shotPoint;
        private Transform groundPoint;
        private BulletManager bulletManager;

        private Vector3 defaultPosition;
        private bool isStart = false;
		private bool isGround = true;
        private bool isShot = false;
        private bool isDead = false;
        private bool isResporn = false;

        private void Start()
        {
            rigidbodyManager = GetComponent<RigidbodyManager>();
            defaultPosition = transform.position;
            shotPoint = transform.GetChild(0);
            groundPoint = transform.GetChild(1);
            GameObject bullet = Instantiate(Resources.Load("Prefabs/Shot", typeof(GameObject))) as GameObject;
            bulletManager = bullet.GetComponentInChildren<BulletManager>();
            bulletManager.SetId(id);

            healthManager = GameObject.Find("UI/Panel/Player" + id).GetComponent<HealthManager>();
        }

        public void SetStart() { isStart = true; }
        public int GetId() { return id; }
        public bool GetISDead() { return isDead; }
        public void SetIsDead()
        { 
            StartCoroutine(WaitForDead());
        }

        private void Update()
        {
            if (isResporn)
            {
                isGround = true;
            }
            else
            {
                isGround = Physics.Linecast(transform.position, groundPoint.position, LayerMask.GetMask("Ground"));
            }

            if (isStart && !isDead && !bulletManager.GetIsActive() && Input.GetButtonDown("Shot"+ id.ToString()))
            {
                StartCoroutine(bulletManager.Shot(shotPoint.position, transform.eulerAngles.y));
                StartCoroutine(WaitForShot());
            }
        }

        private void FixedUpdate()
        {
            float x = Input.GetAxis("Horizontal"+id.ToString());
            float z = Input.GetAxis("Vertical"+ id.ToString());

            /*
            if (!isShot && (THRESHOLD < Mathf.Abs(x) || THRESHOLD < Mathf.Abs(z)))
			{
                Vector3 direction = new Vector3(x, 0, z);
                transform.localRotation = Quaternion.LookRotation(direction);
			}
			*/
            if (isDead)
            {
				Vector3 distance = new Vector3(0, -1.0f, 0);

				rigidbodyManager.Move(distance);
            }
            else
            {
                if (!isShot)
                {
                    if (Mathf.Abs(x) > Mathf.Abs(z))
                    {
                        if (x < -THRESHOLD)
                        {
                            transform.localEulerAngles = new Vector3(0, -90.0f, 0);
                        }
                        else if (THRESHOLD < x)
                        {
                            transform.localEulerAngles = new Vector3(0, 90.0f, 0);
                        }
                    }
                    else
                    {
                        if (z < -THRESHOLD)
                        {
                            transform.localEulerAngles = new Vector3(0, 180.0f, 0);
                        }
                        else if (THRESHOLD < z)
                        {
                            transform.localEulerAngles = new Vector3(0, 0.0f, 0);
                        }
                    }
                }


                if (isGround && !isShot)
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

		public IEnumerator WaitForShot()
		{
			isShot = true;
			yield return new WaitForSeconds(shotTime);
			isShot = false;
		}

        public IEnumerator WaitForDead()
        {
			isDead = true;
            if(0 < healthManager.GetHp())
            {
                healthManager.Damage();
            }
            yield return new WaitForSeconds(deadTime);
            if (0 < healthManager.GetHp())
            {
                transform.position = defaultPosition;
                isDead = false;
                isResporn = true;
                StartCoroutine(WaitForResporn());
            }
        }

        private IEnumerator WaitForResporn()
        {
            yield return new WaitForSeconds(respornTime);

            isResporn = false;
        }
    }
}
