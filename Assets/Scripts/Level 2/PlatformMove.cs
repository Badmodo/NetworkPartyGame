using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platforms.Manager;

namespace Platforms.Movement
{
    public class PlatformMove : PlatformManager
    {
        Transform endPos;
        
        void OnEnable()
        {
            FindObjectOfType<PlatformManager>();
           // endPos = end;
            StartCoroutine(MovePlatform());
        }

        IEnumerator MovePlatform()
        {       
            while (this.transform.position != endPos.position)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, endPos.position, platformSpeed * 10);

                yield return null;
            }
        }
    }
}
