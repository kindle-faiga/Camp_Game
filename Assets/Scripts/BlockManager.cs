﻿using System.Collections;
using UnityEngine;

namespace CampGame
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public class BlockManager : MonoBehaviour
    {
        private Renderer render;
        private Material defaultMaterial;
        private Rigidbody rigidBody;
        private float waitTime = 0.5f;
        private float resetTime = 6f;
        private bool isDead = false;
        private Vector3 defaultPosition;
        [SerializeField]
        private PlayerManager playerManager;
        private AudioSource[] sources;

        private void Start()
        {
            sources = gameObject.GetComponents<AudioSource>();
            render = GetComponent<Renderer>();
            rigidBody = GetComponent<Rigidbody>();
            rigidBody.isKinematic = true;
            defaultPosition = transform.localPosition;
            defaultMaterial = render.material;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag.Equals("TimeOver"))
            {
                isDead = true;
                StartCoroutine(MoveUp());
				Material material = Resources.Load("Materials/Block_Dead") as Material;
				render.material = material;
            }

            if (other.tag.Equals("Bullet") && !isDead)
            {
                BulletManager b = other.GetComponent<BulletManager>();

                if (b.GetIsActive())
                {
                    StartCoroutine(MoveUp());
                    int id = b.GetId();
                    Material material = Resources.Load("Materials/Block_Player"+id) as Material;
                    render.material = material;
                }
            }

            if(other.tag.Equals("Dead"))
            {
                playerManager = other.GetComponentInParent<PlayerManager>();
            }
        }

        private void OnTriggerExit(Collider other)
        {
			if (other.tag.Equals("Dead"))
			{
				playerManager = null;
			}
        }

		private IEnumerator MoveUp()
		{
            sources[0].Play();
            yield return new WaitForSeconds(waitTime);
            sources[1].Play();
            rigidBody.isKinematic = false;
            if(playerManager != null)
            {
                playerManager.SetIsDead();
                playerManager.transform.position = new Vector3(transform.position.x, playerManager.transform.position.y, transform.position.z);
            }
            StartCoroutine(ResetPos());
		}

		private IEnumerator ResetPos()
		{
            yield return new WaitForSeconds(resetTime);
            if (!isDead)
            {
                rigidBody.isKinematic = true;
                transform.localPosition = defaultPosition;
                render.material = defaultMaterial;
            }
		}
    }
}
