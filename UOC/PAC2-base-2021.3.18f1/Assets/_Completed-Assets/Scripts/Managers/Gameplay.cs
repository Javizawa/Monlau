using Complete;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay : NetworkBehaviour
{
    [SerializeField] private float countdownTime = 60.0f;
    [SerializeField] public TMP_Text countdownText;
  //  private GameObject[] players;
  //  private TankManager[] m_Tanks;
    public bool m_start_countdown = false;
    public bool m_end_countdown = false;

    [Server]
    private void DecreaseTimer()
    {
        countdownTime -= Time.deltaTime;

        if (countdownTime <= 0f)
        {
            countdownTime = 0f;
            m_start_countdown = false;
            m_end_countdown = true;
          //  Debug.Log("Time's up!");

            // Aquí puedes llamar a otra función RPC o hacer cualquier otra cosa que necesites
        }

        RpcUpdateCountdownTime(countdownTime);
    }

    [ClientRpc]
    private void RpcUpdateCountdownTime(float time)
    {
        countdownTime = time;
    }

    private void Update()
    {
        if (isServer)
        {
            if (m_start_countdown) { 
            DecreaseTimer();
            }
          //  players = GameObject.FindGameObjectsWithTag("Player");
          //  GetTanks();
        }
        countdownText.text = "Time: " + countdownTime.ToString("0.0").Substring(0, 3);
    }

    public void ResetCountdown()
    {
        m_start_countdown = true;
        m_end_countdown = false;
        countdownTime = 60.0f;
    }

}
