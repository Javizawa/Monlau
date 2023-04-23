using System;
using System.Collections;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//FALTAN RESITUAR A LOS JUGADORES

namespace Complete
{
    public class GameManager : NetworkBehaviour
    {
        public int m_NumRoundsToWin = 5;            // The number of rounds a single player has to win to win the game
        public float m_StartDelay = 3f;             // The delay between the start of RoundStarting and RoundPlaying phases
        public float m_EndDelay = 3f;               // The delay between the end of RoundPlaying and RoundEnding phases
        public CameraControl m_CameraControl;       // Reference to the CameraControl script for control during different phases
        public TMP_Text m_MessageText;              // Reference to the overlay Text to display winning text, etc
        public GameObject m_TankPrefab;             // Reference to the prefab the players will control
        public TankManager[] m_Tanks;               // A collection of managers for enabling and disabling different aspects of the tanks
        public float m_targetTime = 60.0f;
        public TMP_Text m_timer;
        [SerializeField] public TMP_Text m_message;
        [SerializeField] private string messageText;
        public int m_total_rounds = 4;

        private float m_waitTime = 3f;
        private float m_waitTimeNextRound = 3f;

        //private NetworkManager manager;
        int tankNumber = 0;
        private GameObject[] players;
        private bool m_check_start = false;
        private bool m_countdownStarted = false;
        private bool m_countRound = false;
        private int m_rounds = 1;
        private bool m_gameOver = true;

        private void Update()
        {
            if (isServer)
            {
                if (!m_check_start)
                {
                    CheckIfCanStart();
                }
                else
                {
                    if (m_rounds < m_total_rounds) { 
                        if (WaitSeconds(m_rounds)) {
                            GameStart();
                        }
                    }
                    else
                    {
                        if (!m_gameOver) { 
                            CheckWhoWinsGame();
                            m_gameOver = true;
                        }
                    }
                }
            }
        }

        private void GameStart()
        {
            if (GameObject.Find("Gameplay").GetComponent<Gameplay>().m_end_countdown && !m_countRound)
            {
                //COMPRUEBA QUIEN HA GANADO
                CheckWhoWinsRound();
            }
            if (m_countRound)
            {
                // Reinicio round
                WaitSecondsNextRound();
            }
        }

        private bool WaitSeconds(int round)
        {
            if (m_waitTime > 0) {
                messageText = "COMIENZA LA RONDA " + round;
                RpcUpdateMessage(messageText);
                m_waitTime -= Time.deltaTime;
                return false;
            }
            else
            {
                if (!m_countdownStarted)
                {
                    messageText = "";
                    RpcUpdateMessage(messageText);
                    m_countdownStarted = true;
                    RpcEnableTank();
                    StartCountdown();
                    m_waitTimeNextRound = 3f;
                    m_countRound = false;
                }
                return true;
            }
        }

        private void WaitSecondsNextRound()
        {
            if (m_waitTimeNextRound > 0)
            {
              //  Debug.Log("WaitSecondsNextRound" + m_waitTimeNextRound);
                m_waitTimeNextRound -= Time.deltaTime;
            }
            else
            {
                m_countdownStarted = false;
                m_waitTime = 3f;
                //GameObject.Find("Gameplay").GetComponent<Gameplay>().ResetCountdown();
                m_countRound = false;
                m_rounds++;
                if (m_rounds >= m_total_rounds)
                {
                    m_gameOver = false;
                }
            }
        }


        private void CheckWhoWinsRound()
        {
            int lv_player_points = 0;
            int lv_best_points = 0;
            string lv_winners = "";
            bool tie = false;
            for (int i = 0; i < tankNumber; i++)
            {
                if (m_Tanks[i].m_Instance != null)
                {
                    lv_player_points = m_Tanks[i].m_deaths;
                    if (lv_player_points == lv_best_points)
                    {
                        if (i == 0)
                        {
                            lv_winners = m_Tanks[i].m_PlayerNumber.ToString();
                        }
                        else
                        {
                            lv_winners = lv_winners + " y " + m_Tanks[i].m_PlayerNumber;
                            tie = true;
                        }
                    }
                    else if (lv_player_points > lv_best_points)
                    {
                        lv_best_points = lv_player_points;
                        tie = false;
                        lv_winners = m_Tanks[i].m_PlayerNumber.ToString();
                    }
                }
            }
            if (tie)
            {
                messageText = "Han empatado la ronda los jugadores " + lv_winners;
            }
            else
            {
                messageText = "Ha ganado la ronda el jugador " + lv_winners;
            }
            RpcUpdateMessage(messageText);
            //AÑADE EL PUNTO AL JUGADOR/ES QUE GANA/N
            for (int i = 0; i < tankNumber; i++)
            {
                if (m_Tanks[i].m_Instance != null && m_Tanks[i].m_deaths == lv_best_points)
                {
                    m_Tanks[i].m_Wins++;
                }
            }
            m_countRound = true;
            RpcDisableTank();
            ResetTankDeaths();
            //HAS PUESTO ESTO
            RpcResetTanksPossition();
        }




        private void CheckWhoWinsGame()
        {
            int lv_player_points = 0;
            int lv_best_points = 0;
            string lv_winners = "";
            bool tie = false;
            for (int i = 0; i < tankNumber; i++)
            {
                if (m_Tanks[i].m_Instance != null)
                {
                    lv_player_points = m_Tanks[i].m_Wins;
                    if (lv_player_points == lv_best_points)
                    {
                        if (i == 0)
                        {
                            lv_winners = m_Tanks[i].m_PlayerNumber.ToString();
                        }
                        else
                        {
                            lv_winners = lv_winners + " y " + m_Tanks[i].m_PlayerNumber;
                            tie = true;
                        }
                    }
                    else if (lv_player_points > lv_best_points)
                    {
                        lv_best_points = lv_player_points;
                        tie = false;
                        lv_winners = m_Tanks[i].m_PlayerNumber.ToString();
                    }
                }
            }
            if (tie)
            {
                messageText = "Han empatado el juego los jugadores " + lv_winners;
            }
            else
            {
                messageText = "Ha ganado el juego el jugador " + lv_winners;
            }
            RpcUpdateMessage(messageText);
            RpcDisableTank();
            ResetTankDeaths();
            
        }

        [Server]
        private void CheckIfCanStart()
        {
            RpcDisableTank();
            players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length > 1)
            {
                //PUEDE COMENZAR
                m_check_start = true;
                messageText = "";
                RpcUpdateMessage(messageText);

            }
            else
            {
                messageText = "ESPERANDO JUGADORES";
                RpcUpdateMessage(messageText);
            }
        }

        [ClientRpc]
        private void RpcUpdateMessage(string message)
        {
            m_message.text = message;
        }


        [ClientRpc]
        private void RpcEnableTank()
        {
            EnableTankControl();
        }


        [ClientRpc]
        private void RpcDisableTank()
        {
            DisableTankControl();
        }

            private void StartCountdown()
        {
            GameObject.Find("Gameplay").GetComponent<Gameplay>().ResetCountdown();
        }

        public void AddTank(GameObject go)
        {

            //COMPROBAR SI QUEDA ALGÚN HUECO DE ALGUIEN QUE SE HAYA IDO DE LA PARTIDA
            for (int i = 0; i < tankNumber; i++)
            {
                if (m_Tanks[i].m_Instance == null)
                {
                    m_Tanks[i].m_Instance = go;
                    m_Tanks[i].m_deaths = 0;
                    m_Tanks[i].m_PlayerNumber = i + 1;
                    m_Tanks[i].m_PlayerName = PlayerPrefs.GetString("Nombre");
                    m_Tanks[i].Setup();
                    SetCameraTargets();
                    return;
                }
            }

            m_Tanks[tankNumber].m_Instance = go;
            m_Tanks[tankNumber].m_PlayerNumber = tankNumber + 1;
            m_Tanks[tankNumber].m_PlayerName = PlayerPrefs.GetString("Nombre");
            m_Tanks[tankNumber].Setup();
            tankNumber++;
            SetCameraTargets();
        }

        public void SetTankDeaths(int tankNumber)
        {
            m_Tanks[tankNumber - 1].m_deaths++;
        }


        public void ResetTankDeaths()
        {
            for (int i = 0; i < tankNumber; i++)
            {
                if (m_Tanks[i].m_Instance != null)
                {
                    m_Tanks[i].m_deaths = 0;
                }
            }
        }

        private void SetCameraTargets()
        {
            // Create a collection of transforms the same size as the number of tanks
            //Transform[] targets = new Transform[m_Tanks.Length];
            Transform[] targets = new Transform[tankNumber];

            // For each of these transforms...
            //  for (int i = 0; i < targets.Length; i++)
            for (int i = 0; i < tankNumber; i++)
            {
                // ... set it to the appropriate tank transform

                if (m_Tanks[i].m_Instance != null) { 
                    targets[i] = m_Tanks[i].m_Instance.transform;
                }
            }

            // These are the targets the camera should follow
            m_CameraControl.m_Targets = targets;
        }


        // This function is used to turn all the tanks back on and reset their positions and properties
        private void ResetAllTanks()
        {
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                if (m_Tanks[i].m_Instance != null)
                {
                    m_Tanks[i].Reset();
                }
            }
        }

        [ClientRpc]
        private void RpcResetTanksPossition()
        {
            GameObject[] sPoints = GameObject.FindGameObjectsWithTag("Spawn");
          //  System.Random random = new System.Random();
          //  Array.Sort(sPoints, (x, y) => random.Next(-1, 2));
            GameObject[] sTanks = GameObject.FindGameObjectsWithTag("Player");
            int lV_tankID = 0;
            foreach (GameObject sPoint in sPoints)
            {
                if(m_Tanks[lV_tankID].m_Instance != null)
                {
                    sTanks[lV_tankID].transform.position = sPoint.transform.position;
                    sTanks[lV_tankID].GetComponent<TankHealth>().m_reset_health = true;
                }
                lV_tankID++;
            }
        }

        private void EnableTankControl()
        {
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                if (m_Tanks[i].m_Instance != null)
                {
                    m_Tanks[i].EnableControl();
                }
            }
        }


        private void DisableTankControl()
        {
            for (int i = 0; i < m_Tanks.Length; i++)
            {
                if (m_Tanks[i].m_Instance != null)
                {
                    m_Tanks[i].DisableControl();
                }
            }
        }
    }
}