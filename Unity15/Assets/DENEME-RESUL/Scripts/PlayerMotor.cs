using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))] // Unity; Biz her bu componenti çaðýrdýðýmýzda otomatik olarak NavMeshAgent'i ekleyecek.
public class PlayerMotor : MonoBehaviour
{
    
    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    public void Noktayailerle(Vector3 gidecegimizNokta)
    {
        agent.SetDestination(gidecegimizNokta);
    }
}
