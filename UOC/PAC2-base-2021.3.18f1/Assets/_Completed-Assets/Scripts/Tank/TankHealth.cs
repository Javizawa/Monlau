using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Complete
{
    public class TankHealth : NetworkBehaviour
    {
        public const float m_StartingHealth = 100f;
        public Slider m_Slider;
        public Image m_FillImage;
        public Color m_FullHealthColor = Color.green;
        public Color m_ZeroHealthColor = Color.red;
        public GameObject m_ExplosionPrefab;
        public bool m_reset_health = false;

        [SyncVar(hook = "OnChangeHealth")]
        public float currentHealth = m_StartingHealth;


        [SyncVar(hook = "OnSetName")]
        public string currentName;


        private AudioSource m_ExplosionAudio;
        private ParticleSystem m_ExplosionParticles;
        private float m_CurrentHealth;
        private bool m_Dead;




        private void Awake()
        {
            m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
            m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();
            m_ExplosionParticles.gameObject.SetActive(false);
            if(this.gameObject.tag != "NPC") {
              //  OnSetName(currentName, "Player");
            }
        }



        private void Update()
        {
            if (m_reset_health) { 
                resetHealth();
                m_reset_health = false;
            }
        }


        private void OnSetName(string oldName, string newName)
        {
            currentName = PlayerPrefs.GetString("Nombre");
            this.gameObject.GetComponentInChildren<TextMeshPro>().SetText(currentName);

        }




        private void OnEnable()
        {
            m_Dead = false;
            SetHealthUI();   
        }


        public void TakeDamage(float amount, int tankNumber)
        {
            if (!isServer)
            {
                return;
            }
            currentHealth -= amount;
            OnChangeHealth(currentHealth, 3);
            if (currentHealth <= 0f && !m_Dead)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().SetTankDeaths(tankNumber);
                Debug.Log("Te ha matado el tanke " + tankNumber);
                if(gameObject.tag != "NPC") {
                    CmdOnDeath();
                    RpcRespawn();
                    currentHealth = m_StartingHealth;
                    OnChangeHealth(currentHealth, 3);
                }
                else
                {
                    RpcOnDeath();
                }
            }
        }


        public void resetHealth()
        {
            currentHealth = m_StartingHealth;
            OnChangeHealth(currentHealth, 3);
        }


        private void SetHealthUI()
        {
            m_Slider.value = currentHealth;
            m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, currentHealth / m_StartingHealth);
        }
        
        private void OnChangeHealth(float oldHealth, float newHealth)
        {
            m_Slider.value = currentHealth;
            m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, currentHealth / m_StartingHealth);
        }


        [ClientRpc]
        private void RpcOnDeath()
        {
            m_Dead = true;
            m_ExplosionParticles.transform.position = transform.position;
            m_ExplosionParticles.gameObject.SetActive(true);
            m_ExplosionParticles.Play();
            m_ExplosionAudio.Play();
            gameObject.SetActive(false);
        }


        [Command]
        private void CmdOnDeath()
        {
            m_Dead = true;
            m_ExplosionParticles.transform.position = transform.position;
            m_ExplosionParticles.gameObject.SetActive(true);
            m_ExplosionParticles.Play();
            m_ExplosionAudio.Play();
            if (gameObject.tag == "NPC")
            {
                gameObject.SetActive(false);
            }
            //gameObject.SetActive(false);
        }


        [TargetRpc]
        void RpcRespawn()
        {
            m_Dead = false;
            if (isLocalPlayer)
            {
                // transform.position = new Vector3(0,0,0);
                GameObject[] sPoints = GameObject.FindGameObjectsWithTag("Spawn");
                transform.position = sPoints[gameObject.GetComponent<TankMovement>().m_PlayerNumber - 1].transform.position;
                gameObject.GetComponentInParent<Rigidbody>().velocity = new Vector3(0,0,0);
                m_ExplosionParticles.gameObject.SetActive(false);
               //gameObject.SetActive(true);
            }
        }

    }
}
