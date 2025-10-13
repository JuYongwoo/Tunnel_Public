using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Zombie : MonoBehaviour
{
    public enum MoveStat
    {
        Stand, //0
        Walk, //1
        Run, //2
        Attack //3
    }
    public MoveStat moveStat = MoveStat.Stand;

    protected Animator animator;

    virtual protected void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            SceneManager.LoadScene("GameOver");
    }

    public void setAnimStat(MoveStat statnum) {
        animator.SetInteger("MoveStat", (int)statnum);
    }

}
