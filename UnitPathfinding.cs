using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BehaviourType
{
    Gather,
    Patrol,
    Fight,
    Escape
}
public class UnitPathfinding : MonoBehaviour
{
    public GameObject destination;
    public Unit self_stats;
    public Coroutine ActiveCR;
    // Start is called before the first frame update
    private void Start()
    {
        self_stats = this.gameObject.GetComponent<UnitDataHolder>().unit_info;
        switch(self_stats.unit)
        {
            case Eunit.worker:
                ActiveCR = StartCoroutine(WorkerAI());
                break;
            case Eunit.warrior:
                ActiveCR = StartCoroutine(SoliderAI());
                break;
            case Eunit.archer:
                ActiveCR = StartCoroutine(ArcherAI());
                break;
        }
    }
    private IEnumerator WorkerAI()
    {
        while(true)
        {
            Debug.Log("Rolnik szuka ¿ony");
            
            yield return new WaitForSeconds(1f);
        }
    }
    private IEnumerator SoliderAI()
    {
        while (true)
        {
            Debug.Log("Rolnik szuka ¿ony");
            yield return new WaitForSeconds(1f);
        }
    }
    private IEnumerator ArcherAI()
    {
        while (true)
        {
            Debug.Log("Rolnik szuka ¿ony");
            yield return new WaitForSeconds(1f);
        }
    }
}
