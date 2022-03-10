using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSystem : MonoBehaviour
{


    public GameObject[] ButtonStatic;
    GameObject ParentButton;
    GameObject[] ChildArray;
    int Indexnum;
    int MaxIndex;

    GameObject TempObject;
    // Start is called before the first frame update
    void Start()
    { 
        Indexnum = 0;
        ParentButton = GameObject.Find("Switch");
        MaxIndex = ParentButton.transform.childCount;
        ButtonStatic = new GameObject[MaxIndex];
        
        
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void PushIndex(GameObject gameobject)
    {
        ButtonStatic[Indexnum] = gameobject;
        Indexnum++;
    }

    public void PopIndex()
    {
        ButtonStatic[Indexnum-1] = null;
        Indexnum--;
    }   

    public void ResetIndex()
    {
        for (int i = 0; i <= Indexnum; Indexnum-- )
        {
            ButtonStatic[Indexnum] = null;
        }
        Indexnum = 0;
    }

    public bool CheckIndex(GameObject gameObject)
    {
        Debug.Log(gameObject.name);
        TempObject = gameObject;
        if (Indexnum == 0 && ButtonStatic[Indexnum] == TempObject)
            return true;

        else if (Indexnum != 0 && ButtonStatic[Indexnum-1] == TempObject)
        {
            return true;
        }

        else
        {
            return false;
        }
    }


}
