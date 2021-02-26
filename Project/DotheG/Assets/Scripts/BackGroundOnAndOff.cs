using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundOnAndOff : MonoBehaviour
{
    GenerateMonster gm;
    Animator moveText;
    public GameObject backGround;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        backGround.SetActive(false);
        gm = GameObject.Find("GameManager").GetComponent<GenerateMonster>();
        moveText = this.gameObject.GetComponent<Animator>();
        time = 0;
        moveText.Rebind();
        moveText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gm.isFeverTime)
        {
            moveText.enabled = true;
            
            if (time<=18f)
            {
                backGround.SetActive(true);
            }
            else
            {
                backGround.SetActive(false);
            }
            time += 0.2f;
        }
        else
        {
            time = 0;
            moveText.Rebind();
            moveText.enabled = false;
        }
        
    }
}
