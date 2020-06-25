using System;
using UnityEngine;


public class tile : MonoBehaviour
{
    public Transform[] backgroundContainers;
    public Transform cam;
    public float[] parallaxScale;
    public float smoothing = 1f;
    private Vector3 m_PreviousCamPos;

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    void Start()
    {
        m_PreviousCamPos = cam.position;

        parallaxScale = new float[backgroundContainers.Length];
        for (int i = 0; i < backgroundContainers.Length; i++)
        {
            parallaxScale[i] = backgroundContainers[i].position.z * -1;
        }
    }

    private void Update()
    {
        for (int i = 0; i < backgroundContainers.Length; i++)
        {
            float parallax = (cam.position.x - m_PreviousCamPos.x) * parallaxScale[i];
            float parallay = (cam.position.y - m_PreviousCamPos.y) * parallaxScale[i];
            float backGroundTargetX = cam.position.x + parallax;
            float backGroundTargetY = cam.position.y + parallay;
            Vector3 targetPos = new Vector3(backGroundTargetX, backGroundTargetY, backgroundContainers[i].position.z);

            backgroundContainers[i].position = Vector3.Lerp(backgroundContainers[i].position, targetPos, Time.deltaTime);
        }

        m_PreviousCamPos = cam.position;
    }
}