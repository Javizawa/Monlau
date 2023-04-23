using Mirror;
using UnityEngine;

namespace Complete
{
    public class CameraControl : NetworkBehaviour
    {
        public float m_DampTime = 0.2f;                 // Approximate time for the camera to refocus
        public float m_ScreenEdgeBuffer = 4f;           // Space between the top/bottom most target and the screen edge
        public float m_MinSize = 6.5f;                  // The smallest orthographic size the camera can be
        [HideInInspector] public Transform[] m_Targets; // All the targets the camera needs to encompass


        private Camera m_Camera;
        private float m_ZoomSpeed;
        private Vector3 m_MoveVelocity;
        private Vector3 m_DesiredPosition;
        private GameObject[] enemies;
        private GameObject[] players;


        private void Awake()
        {
            m_Camera = GetComponentInChildren<Camera>();
        }

        private void FixedUpdate()
        {
            enemies = GameObject.FindGameObjectsWithTag("NPC");
            players = GameObject.FindGameObjectsWithTag("Player");
            Move();
            Zoom();

        }

        [ClientRpc]
        public void RpcObjectRemoved()
        {
            if (!isServer)
            {
                SetStartPositionAndSize();
            }
        }

        private void Move()
        {
            FindAveragePosition();
            transform.position = Vector3.SmoothDamp(transform.position, m_DesiredPosition, ref m_MoveVelocity, m_DampTime);
        }

        private void FindAveragePosition()
        {
            Vector3 averagePos = new Vector3();
            int numTargets = 0;

            //for (int i = 0; i < m_Targets.Length; i++)
            foreach (GameObject player in players)
            {
                //if (!m_Targets[i].gameObject.activeSelf)
                if (!player.gameObject.activeSelf)
                {
                    continue;
                }

                //averagePos += m_Targets[i].position;
                averagePos += player.transform.position;
                numTargets++;
            }

            //BUSCAMOS LOS QUE SON NPCS PARA AÑADIRLOS AL CÁLCULO

            foreach (GameObject enemy in enemies)
            {
                if (!enemy.gameObject.activeSelf)
                {
                    continue;
                }

                averagePos += enemy.transform.position;
                numTargets++;
            }


            //FIN MODIFICACIÓN NPCs

            if (numTargets > 0)
            {
                averagePos /= numTargets;
            }

            averagePos.y = transform.position.y;
            m_DesiredPosition = averagePos;
        }

        private void Zoom()
        {
            float requiredSize = FindRequiredSize();
            m_Camera.orthographicSize = Mathf.SmoothDamp(m_Camera.orthographicSize, requiredSize, ref m_ZoomSpeed, m_DampTime);
        }

        private float FindRequiredSize()
        {
            Vector3 desiredLocalPos = transform.InverseTransformPoint(m_DesiredPosition);
            float size = 0f;

            //for (int i = 0; i < m_Targets.Length; i++)
            foreach (GameObject player in players)
            {
                //  if (!m_Targets.gameObject.activeSelf)
                if (!player.gameObject.activeSelf)
                {
                    continue;
                }

                //Vector3 targetLocalPos = transform.InverseTransformPoint(m_Targets[i].position);
                Vector3 targetLocalPos = transform.InverseTransformPoint(player.transform.position);
                Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;
                size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));
                size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / m_Camera.aspect);
            }



            //BUSCAMOS LOS QUE SON NPCS PARA AÑADIRLOS AL CÁLCULO
            foreach (GameObject enemy in enemies)
            {
                if (!enemy.gameObject.activeSelf)
                {
                    continue;
                }

                Vector3 targetLocalPos = transform.InverseTransformPoint(enemy.transform.position);
                Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;
                size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));
                size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / m_Camera.aspect);
            }

            //FIN DEL CÁLCULO DE LOS NPCs
            size += m_ScreenEdgeBuffer;
            size = Mathf.Max(size, m_MinSize);

            return size;
        }

        public void SetStartPositionAndSize()
        {
            FindAveragePosition();
            transform.position = m_DesiredPosition;
            m_Camera.orthographicSize = FindRequiredSize();
        }
    }

}