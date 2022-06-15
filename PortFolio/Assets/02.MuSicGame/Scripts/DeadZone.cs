using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
 

    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //GetComponentInParent ºÎ¸ð °´Ã¼ÀÇ Æ¯Á¤ ÄÄÆ÷³ÍÆ®¸¦ °¡Á®¿È /22.03.26 by½ÂÁÖ
            other.GetComponentInParent<PlayerController>().ResetFalling();

            
            
        }
    }
}
