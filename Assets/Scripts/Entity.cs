using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

//[System.Serializable]
//public class MyIntEvent : UnityEvent<float>
//{
//}

public class Entity : MonoBehaviour
{
    //public MyIntEvent m_MyEvent;
    [SerializeField] private float startingHealth;
    [SerializeField] private float health;
    public bool isEnemy;
    public float Health
    {
        get { return health; }
        set
        {
            health = value;
            if (isEnemy)
            {
                //Debug.Log("Enemy health: " +health);
                if (health <= 0f) Destroy(gameObject);
            }
            else { 
                //Debug.Log("Player health: " + health);
            }
            
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        health = startingHealth;
        //if (m_MyEvent == null)
        //    m_MyEvent = new MyIntEvent();

        //m_MyEvent.AddListener(PlayerController.UpdateHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
