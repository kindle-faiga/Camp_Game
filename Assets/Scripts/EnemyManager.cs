using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CampGame
{
    [RequireComponent(typeof(RigidbodyManager))]
    public class EnemyManager : MonoBehaviour
    {
        private RigidbodyManager rigidbodyManager;
        [SerializeField]
        private BoxCollider boxCollider;
        [SerializeField]
        private float startTime = 1.0f;
		[SerializeField]
		private float size = 5.0f;
		[SerializeField]
        private Vector3 vectol = new Vector3(1,0,0);
        private bool isStart = false;

        void Start()
        {
            rigidbodyManager = GetComponent<RigidbodyManager>();
            StartCoroutine(WaitForStart());
        }

        private void FixedUpdate()
        {
            if(isStart)
            {
                /*
                float x = -Mathf.Sin(transform.eulerAngles.y * Mathf.Rad2Deg);
				float z = Mathf.Cos(transform.eulerAngles.y * Mathf.Rad2Deg);
                */

                Vector3 distance = vectol;

				rigidbodyManager.Move(distance);
            }
        }

        private IEnumerator WaitForStart()
        {
            yield return new WaitForSeconds(startTime);

            isStart = true;

            yield return new WaitForSeconds(2.0f);

            boxCollider.size = new Vector3(1,1,size);
        }
    }
}