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
    private GameObject currentTextBox;
    private GameObject currentHint;
    private bool getText = false;
    
    // Update is called once per frame
    void Update()
    {
        if (DetectObject())
        {
            if (currentTextBox != null)
            {
                if (currentTextBox.activeInHierarchy)
                {
                    DisableName();
                }
                else
                {
                    EnableName();
                }
            }
            EnableBackLight();    
            EnableHint();
            if (InteractInput())
            {
                if (currentInteractable.CompareTag("NextLevel"))
                {
                    levelLoader = currentInteractable.transform.GetComponent<LevelLoader>();
                            
                    levelLoader.EnterRoom();
                }

                if (currentTextBox != null)
                {
                    if (!currentTextBox.activeInHierarchy)
                    {
                        EnableTextBox();
                    }
                    else
                    {
                        DisableTextBox();
                    }
                }
            }
            
        }
        else
        {   
            if (currentTextBox != null)
            {
                if (currentTextBox.activeInHierarchy)
                {
                    DisableTextBox();
                }
            }
            DisableBackLight();
            DisableName();
            DisableHint();
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
                   currentBackLight = currentInteractable.transform.Find("BackLight")?.gameObject;
                   currentName = currentInteractable.transform.Find("Name").gameObject;
                   currentTextBox = currentInteractable.transform.Find("TextBox").gameObject;
                   currentHint  = currentInteractable.transform.Find("Hint").gameObject;
               }  
            }

            if (obj.CompareTag("NextLevel"))
            {
                levelLoader =  obj.transform.GetComponent<LevelLoader>();
                
                currentInteractable = obj.transform.gameObject;
                currentBackLight = currentInteractable.transform.Find("BackLight")?.gameObject;
                currentHint = currentInteractable.transform.Find("Hint").gameObject;
            }
           
            
            return true;
        }
        else
        {
            currentInteractable = null;
            return false;
        }
        
        
    }
    void EnableBackLight()
    {
        if (currentBackLight != null && !currentBackLight.activeSelf)
            currentBackLight.SetActive(true);
        
    }

    void DisableBackLight()
    {
        if (currentBackLight != null)
            currentBackLight.SetActive(false);
        
        currentInteractable = null;
        currentBackLight = null;
        
    }

    void EnableName()
    {
        if (currentName != null && !currentName.activeSelf)
        {
            currentName.SetActive(true);
        }
    }
    
    void DisableName()
    {
        if (currentName != null){
            currentName.SetActive(false);
            
        }
        currentName = null;
    }
    void EnableHint()
    {
        if (currentHint != null && !currentHint.activeSelf)
        {
            currentHint.SetActive(true);
        }
    }
    
    private void DisableHint()
    {
        if (currentHint != null)
        {
            currentHint.SetActive(false);
        }
    }

    void EnableTextBox()
    {
        if (currentTextBox != null && !currentTextBox.activeSelf)
        {
            currentTextBox.SetActive(true);
        }
    }
    
    void DisableTextBox()
    {
        if (currentTextBox != null)
        {
            currentTextBox.SetActive(false);
        }
    }
    
    
}
