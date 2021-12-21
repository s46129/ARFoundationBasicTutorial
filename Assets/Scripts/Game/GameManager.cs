using System;

namespace ARTutorial.ShootGame
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.XR.ARFoundation.Samples;

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        [SerializeField] private PlaceOnPlane _placeOnPlane;
        private SpouterController spouterController;
        [SerializeField] private GameObject StartGameUI;

        private void Awake()
        {
            instance = this;
            StartGameUI.SetActive(false);
        }

        public void startGame()
        {
            _placeOnPlane.enabled = false;
            setStartGameUI(false);
            spouterController.StartPout();
        }

        public void GameOver()
        {
            _placeOnPlane.enabled = true;
        }

        public void OnSpouterEnable(SpouterController controller)
        {
            spouterController = controller;
            setStartGameUI(true);
        }

        void setStartGameUI(bool acive)
        {
            StartGameUI.SetActive(acive);
        }
    }
}