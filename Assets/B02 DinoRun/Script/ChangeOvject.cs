using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOvject : MonoBehaviour
{
    
    public GameObject[] objs;



    // Start is called before the first frame update
    void Awake()
    {
   
    }

    public void Change()
    {
        int ran = Random.Range(0 , objs.Length);

        for(int index = 0; index < objs.Length; index++)
        {
            transform.GetChild(index).gameObject.SetActive(ran == index);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
