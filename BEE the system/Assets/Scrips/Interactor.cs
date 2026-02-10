using Unity.VisualScripting;
using UnityEngine;

public class Interactor : MonoBehaviour
{
  
    public Transform detectionPoint;
    private const float detectionRadius = 0.2f;
    public LayerMask LayerMask;
    private LevelLoader levelLoader;
    
    private GameObject currentInteractable;
    private GameObject currentBackLight;
    private GameObject currentName;

    
    // Update is called once per frame
    void Update()
    {
        if (DetectObject())
        {
            EnableBackLightAndName();

            if (InteractInput())
            {
                if (currentInteractable.CompareTag("NextLevel"))
                {
                    levelLoader = currentInteractable.transform.GetComponent<LevelLoader>();
                            
                    levelLoader.EnterRoom();
                }
                Debug.Log("Interact");
            }
        }
        else
        {
            DisableBackLightAndName();
        }
    }


    bool InteractInput()
    {
        

        return Input.GetButtonDown("Interact");
    }

    bool DetectObject()
    {
        Collider2D obj = Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, LayerMask);
        if (obj != null)
        {
            if (obj.CompareTag("Interactable"))
            {
               if (currentInteractable != obj.gameObject)
               {
                   currentInteractable = obj.transform.parent.gameObject;
                   //Debug.Log("currentInteractable: " + currentInteractable.name);
                   currentBackLight = currentInteractable.transform.Find("BackLight")?.gameObject;
                   //Debug.Log("currentBackLight: " + currentBackLight.name);
                   currentName = currentInteractable.transform.Find("Name").gameObject;
               }  
            }

            if (obj.CompareTag("NextLevel"))
            {
                levelLoader =  obj.transform.GetComponent<LevelLoader>();
                
                currentInteractable = obj.transform.gameObject;
                currentBackLight = currentInteractable.transform.Find("BackLight")?.gameObject;
            }
           
            
            return true;
        }
        else
        {
            currentInteractable = null;
            return false;
        }
        
        
    }
    void EnableBackLightAndName()
    {
        if (currentBackLight != null && !currentBackLight.activeSelf)
            currentBackLight.SetActive(true);
        if (currentName != null && !currentName.activeSelf)
            currentName.SetActive(true);
    }

    void DisableBackLightAndName()
    {
        if (currentBackLight != null)
            currentBackLight.SetActive(false);
        if (currentName != null)
            currentName.SetActive(false);
        currentInteractable = null;
        currentBackLight = null;
        currentName = null;
    }

   
    
    
}
