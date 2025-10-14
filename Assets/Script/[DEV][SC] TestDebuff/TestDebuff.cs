using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDebuff : MonoBehaviour
{
    public B_and_DB_Manager manager;
    public string buffId;

    public void testDubuff()
    {
        manager.FindDBB(buffId);
    }
    
}
