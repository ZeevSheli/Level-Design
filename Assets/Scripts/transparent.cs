using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transparent : MonoBehaviour
{
    public Material transM;

    string transparent_layers = "0,10,21";/*string transparent_layers = "0,9,10,12,13,";*/

    public GameObject head_game_object;
    BoxCollider boxCollider;

    struct name_material
    {
        //public List<Material> mats;
        public Material[] mats;
        public string name;
    }

    List<name_material> storage;

    // Start is called before the first frame update
    void Start()
    {
        storage = new List<name_material>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        ColliderUpdate();
    }

    void AddToStorage(GameObject gameObject)
    {

        Material[] mats;

        mats = new Material[gameObject.GetComponent<Renderer>().materials.Length];
        mats = gameObject.GetComponent<Renderer>().materials;
        
        string name = gameObject.name;

        name_material n; n.mats = mats; n.name = name;
        bool is_stored = false;
        foreach(name_material nm in storage)
        {
            if (nm.name == name)
            {
                is_stored = true;
                break;
            }
        }
        if (is_stored == false)
        {
            storage.Add(n);
        }

        Material[] matArray = new Material[mats.Length];
        for(int i = 0; i < matArray.Length; i++)
        {
            matArray[i] = transM;
        }
        gameObject.GetComponent<Renderer>().materials = matArray;
    }


    /*this was on trigger enter before but I changed it because it was returning to normal
    on trigger exit even if it was colliding with other colliders of the same object*/
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.GetComponent<Renderer>() == null && other.gameObject.layer != head_game_object.layer) { return; }

        if (transparent_layers.Contains(other.gameObject.layer.ToString()))
        {
            if(other.gameObject.layer == head_game_object.layer)
            {
                AddToStorage(other.gameObject.transform.GetChild(0).GetChild(0).GetChild(1).gameObject);
            }
            else
            {
                AddToStorage(other.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Renderer>() == null && other.gameObject.layer != head_game_object.layer) { return; }

        if (transparent_layers.Contains(other.gameObject.layer.ToString()))
        {
            string name = other.gameObject.name;
            foreach (name_material nm in storage)
            {
                if (name == nm.name)
                {
                    if(other.gameObject.layer == head_game_object.layer)
                    {
                        other.gameObject.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Renderer>().materials = nm.mats;
                    }
                    else
                    {
                        other.gameObject.GetComponent<Renderer>().materials = nm.mats;
                    }
                }
            }
        }
    } 
    
    void ColliderUpdate()
    {
        //Update position to be half way between camera and the player
        float distance = Vector3.Distance(head_game_object.transform.position, gameObject.transform.position);
        if(distance < 0.9f)
        {
            distance = 0.9f;
        }
        boxCollider.center = new Vector3(boxCollider.center.x, boxCollider.center.y, distance * 0.5f - 0.55f);

        //Adjust size to fit space between camera and the player
        boxCollider.size = new Vector3(boxCollider.size.x, boxCollider.size.y, distance * 1.0f + 0.0f); //added 0.3f z to make things closer to the head also dissapear 
    }

}
