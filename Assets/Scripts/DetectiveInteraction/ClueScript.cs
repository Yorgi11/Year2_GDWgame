using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueScript : MonoBehaviour
{
    // you should use arrays not lists
    // more efficient
    // bc you arent really utilizing the lists features
    [SerializeField] private List<GameObject> clueSource;
    [SerializeField] private List<bool> activeSource;
    [SerializeField] private List<string> names;
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
        // this shouldn't be in any update or fixedupdate
        // rn its looping through the list every physics update
        // only need tosetactive once
        // can setactive by default so no need to set active unless you set them to false before

        // if all clues are activated in start then there is no need to check if the clue at the array is activated

        // to deactivate the clues a simple deactivate method being called when you want then to deactivate is better than
        // constantly looping through the list every physics update
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
            // if u put the activate source variables in a separate script for each clue then all u need here is
            // collision.gameobject.Getcomponent<CubeClueScript>().activeSource(true)
            // Destroy(collision.gameObject);

            // saves having to loop through the list every time
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
