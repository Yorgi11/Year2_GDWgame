using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueScript : MonoBehaviour
{
    public List<GameObject> clueSource;
    public List<bool> activeSource;
    public List<string> names;
    // Start is called before the first frame update
    void Start()
    {
        for(int b = 0; activeSource.Count < b; b++)
        {
            activeSource[b] = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; clueSource.Count > i; i++)
        {
            clueSource[i].SetActive(activeSource[i]);
        }
    }

    private void journalRefresh()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Clue")
        {
            for (int i = 0; names.Count > i; i++)
            {
                if(collision.transform.name == names[i])
                {
                    activeSource[i] = true;
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}
