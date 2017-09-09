using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CampGame
{
    public class HealthManager : MonoBehaviour
    {
        [SerializeField]
        private int id;
        private List<Image> images = new List<Image>();
        private int hp;
        private GameManager gameManager;
        private Text text;

        void Start()
        {
            foreach (Image image in GetComponentsInChildren<Image>())
            {
                if (image.name.Equals("Hp")) images.Add(image);
            }

            hp = images.Count;

            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            text = transform.FindChild("Rank").GetComponent<Text>();
            text.enabled = false;
        }

        public int GetHp() { return hp; }

        public void Damage()
        {
            --hp;

            images[hp].enabled = false;

            if(hp == 0)
            {
                text.enabled = true;
                text.text = gameManager.GetDeadCount(id).ToString();
            }
        }
    }
}