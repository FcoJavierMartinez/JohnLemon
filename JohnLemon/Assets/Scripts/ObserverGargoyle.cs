using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverGargoyle : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;
    public Timer gameTime;
    public GhostStateMachine[] ghostList;

    bool m_IsPlayerInRange;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_IsPlayerInRange = true;

            foreach (GhostStateMachine ghost in ghostList)
            {
                ghost.ChangeState(GhostStateMachine.state.ChaseIndiscriminate, player);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }

    void Update()
    {
        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    gameTime.time -= 15f;
                    m_IsPlayerInRange = false;
                }
            }
        }
    }
}
