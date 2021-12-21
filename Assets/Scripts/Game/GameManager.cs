using UnityEngine.UI;

namespace ARTutorial.ShootGame
{
    using UnityEngine;
    using UnityEngine.XR.ARFoundation.Samples;

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        [SerializeField] private PlaceOnPlane _placeOnPlane;
        private SpouterController spouterController;
        [SerializeField] private GameObject StartButton;
        [SerializeField] private float score = 0;
        [SerializeField] private Text ScoreUI;
        [SerializeField] private Text TimeUI;

        private bool isStartGame = false;
        [SerializeField] private float GameTime = 180;
        private float tempTime;

        private void Awake()
        {
            instance = this;
            StartButton.SetActive(false);
        }

        public void startGame()
        {
            _placeOnPlane.enabled = false;
            setStartGameUI(false);
            spouterController.StartPout();
            score = 0;
            setScoreText();
            tempTime = GameTime;
            isStartGame = true;
        }

        public void GameOver()
        {
            _placeOnPlane.enabled = true;
            isStartGame = false;
            setStartGameUI(true);
            spouterController.StopPout();
        }

        private void Update()
        {
            if (!isStartGame)
            {
                return;
            }

            tempTime -= Time.deltaTime;
            tempTime = Mathf.Clamp(tempTime, 0, GameTime);
            TimeUI.text = tempTime.ToString("F1");
            if (tempTime <= 0)
            {
                GameOver();
            }
        }

        public void OnSpouterEnable(SpouterController controller)
        {
            spouterController = controller;
            setStartGameUI(true);
        }

        void setStartGameUI(bool acive)
        {
            StartButton.SetActive(acive);
        }

        public void AddScore(float _score)
        {
            score += _score;
            setScoreText();
        }

        void setScoreText()
        {
            ScoreUI.text = $"分數： {score}";
        }
    }
}