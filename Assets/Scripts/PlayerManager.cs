﻿using System.Collections;
using UnityEngine;

namespace CampGame
{
    [RequireComponent(typeof(RigidbodyManager))]
    public class PlayerManager : MonoBehaviour
    {
        public static readonly float THRESHOLD = 0.001f;
        [SerializeField]
        private float shotTime = 1.0f;
        [SerializeField]
        private int id = 0;
        private RigidbodyManager rigidbodyManager;
		private Transform shotPoint;
        private Transform groundPoint;
        private BulletManager bulletManager;

        private bool isGround = true;
        private bool isShot = false;

        private void Start()
        {
            rigidbodyManager = GetComponent<RigidbodyManager>();
            shotPoint = transform.GetChild(0);
            groundPoint = transform.GetChild(1);
            GameObject bullet = Instantiate(Resources.Load("Prefabs/Shot", typeof(GameObject))) as GameObject;
            bulletManager = bullet.GetComponentInChildren<BulletManager>();
        }

        public int GetId() { return id; }

        private void Update()
        {
            isGround = Physics.Linecast(transform.position, groundPoint.position);

            if (!bulletManager.GetIsActive() && Input.GetButtonDown("Shot"+ id.ToString()))
            {
                StartCoroutine(bulletManager.Shot(shotPoint.position, transform.eulerAngles.y));
                StartCoroutine(WaitForShot());
            }
        }

        private void FixedUpdate()
        {
            float x = Input.GetAxis("Horizontal"+id.ToString());
            float z = Input.GetAxis("Vertical"+ id.ToString());

            if (!isShot && (THRESHOLD < Mathf.Abs(x) || THRESHOLD < Mathf.Abs(z)))
			{
                Vector3 direction = new Vector3(x, 0, z);
                transform.localRotation = Quaternion.LookRotation(direction);
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

		public IEnumerator WaitForShot()
		{
			isShot = true;
			yield return new WaitForSeconds(shotTime);
			isShot = false;
		}
    }
}
