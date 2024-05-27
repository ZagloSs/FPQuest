using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManosProperties : MonoBehaviour
{
    private Vector3 iPos;
    private Vector3 targetPos;
    private bool isAtt = false;
    
    void Start()
    {
        iPos = transform.position;
    }

    private void Update()
    {
        if (isAtt)
        {
            Vector2.MoveTowards(iPos, targetPos, 5f * Time.deltaTime);
        }
    }

    public void StartAttack()
    {

    }
}
