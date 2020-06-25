using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
   public Transform cam;
   public float shakeAmt;
   public float shakeDuration;

   private void Awake()
   {
      if (cam == null)
      {
         cam = Camera.main.transform;
      }
   }

   public void Shake()
   {
      StartCoroutine(CamShake(shakeAmt, shakeDuration));
   }

   // private void Update()
   // {
   //    if (Input.GetKeyDown(KeyCode.T))
   //    {
   //       StartCoroutine(CamShake(shakeAmt, shakeDuration));
   //    }
   // }

   public IEnumerator CamShake(float magnitude, float duration)
   {
      float elapsed = 0;
      Vector3 originalPos = cam.position;

      while (elapsed <= duration)
      {
         float offsetX = Random.Range(-1f, 1f) * magnitude;
         float offsetY = Random.Range(-1f, 1f) * magnitude;
         
         cam.position = new Vector3(originalPos.x + offsetX, originalPos.y + offsetY, originalPos.z);
         elapsed += Time.deltaTime;
         
         yield return new WaitForSeconds(0.01f);
      }
      
      cam.localPosition = Vector3.zero;
   }
}
