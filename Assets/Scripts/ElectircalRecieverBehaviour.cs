using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class ElectircalRecieverBehaviour : MonoBehaviour
{
    bool isUnlocked = false;

    public UnityEvent Event;

    [SerializeField]
    GameObject[] boxes;

    BOOL[] bools;
    public class BOOL
    {
        public BOOL(bool b)
        {
            _bool = b;
        }
        public bool _bool;
    }
    void Start()
    {
        bools = new BOOL[boxes.Length];
        int i = 0;
        foreach(GameObject g in boxes)
        {
            ElectricalBoxBehaviour x = g.GetComponent<ElectricalBoxBehaviour>();
            if (x == null)
            {
                Debug.Log(gameObject.name + " one or more assigned electrical box did not have a electrical box behaviour script on them, are you sure you assigned this reciever electrical boxes?");
            }
            
            
            if(g.GetComponent<ElectricalBoxBehaviour>().boolIsSet)
            {
                BOOL b = g.GetComponent<ElectricalBoxBehaviour>().GetBOOL();
                bools[i] = b;
                i++;
            }
            else
            {
                BOOL b = new BOOL(false);
                bools[i] = b;
                i++;
                g.GetComponent<ElectricalBoxBehaviour>().SetBool(b);
            }
            
            
        }
        
    }

    void Update()
    {
        if (isUnlocked) { return; }
        if(AllBoolsAreTrue())
        {
            Unlock();
        }
    }

    bool AllBoolsAreTrue()
    {
        foreach(BOOL B in bools)
        {
            if(B._bool == false)
            {
                return false;
            }
        }
        return true;
    }

    void Unlock()
    {
        isUnlocked = true;
        //Debug.Log("UNLOCKED!");
        if(Event != null)
        {
            Event.Invoke();
        }
    }
}
