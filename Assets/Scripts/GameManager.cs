using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace CampGame
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerManager[] playerManager;
        [SerializeField]
        private Image[] images;
		[SerializeField]
		private Image[] rankImages;
		[SerializeField]
		private Text[] ranks;
        private AudioSource[] sources;

        private int deadCount = 5;
        private int deadNum = 3;
        private int rankCount = 9;

        public int GetDeadCount(int id)
        {
            --deadCount;

            --deadNum;
            rankCount -= id;

            if(deadNum <= 0)
            {
                ranks[rankCount].enabled = true;
                ranks[rankCount].text = "1";
                StartCoroutine(WaitForEnd());
            }

            return deadCount;
        }

        IEnumerator Start()
        {
            sources = gameObject.GetComponents<AudioSource>();

            foreach (Image i in images)
			{
                i.enabled = false;
			}

			foreach (Image ri in rankImages)
			{
				ri.enabled = false;
			}

            yield return new WaitForSeconds(3.0f);

			sources[2].Play();
            images[0].enabled = true;

			yield return new WaitForSeconds(1.0f);

            sources[2].Play();
            images[0].enabled = false;
			images[1].enabled = true;

			yield return new WaitForSeconds(1.0f);

            sources[2].Play();
			images[1].enabled = false;
			images[2].enabled = true;

			yield return new WaitForSeconds(1.0f);

            sources[2].Play();
			images[2].enabled = false;
			images[3].enabled = true;

            sources[0].Play();

            foreach(PlayerManager p in playerManager)
            {
                p.SetStart();
            }

			yield return new WaitForSeconds(0.5f);

			images[3].enabled = false;
		}

        IEnumerator WaitForEnd()
        { 
            yield return new WaitForSeconds(0.5f);
			
            sources[1].Play();

            rankImages[rankCount].enabled = true;

            yield return new WaitForSeconds(5.0f);

            SceneManager.LoadScene("Title");
        }
    }
}