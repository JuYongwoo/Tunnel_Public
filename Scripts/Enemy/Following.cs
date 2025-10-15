using UnityEngine;
using UnityEngine.AI;

public class Following : MonoBehaviour
{
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = Util.AddOrGetComponent<NavMeshAgent>(gameObject);
        agent.speed = 1.5f;

        GetComponent<Zombie>().setAnimStat(Zombie.MoveStat.Run);

    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = ManagerObject.instance.actionManager.ThisScenePlayer.transform.position;
    }
}
