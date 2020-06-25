using UnityEngine;

public class Tiling : MonoBehaviour
{
    private Camera m_Cam;
    private float m_SpriteWidth = 0f;
    public bool hasLeftClone = false;
    public bool hasRightClone = false;
    public bool needsMirror = false;
    public float offset = 2f;
    private Transform myTransform;


    private void Awake()
    {
        m_Cam = Camera.main;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        m_SpriteWidth = sr.sprite.bounds.size.x;
        myTransform = transform;
    }

    void Update()
    {
        float cameraHalfExtendX = m_Cam.orthographicSize * m_Cam.aspect;
        if (m_Cam.transform.position.x + cameraHalfExtendX > myTransform.position.x + m_SpriteWidth / 2 - offset && !hasRightClone)
        {
            GenClone(1);
            hasRightClone = true;
        }
        else if (m_Cam.transform.position.x - cameraHalfExtendX < myTransform.position.x - m_SpriteWidth / 2 + offset &&
                 !hasLeftClone)
        {
            GenClone(-1);
            hasLeftClone = true;
        }
    }

    void GenClone(int rightOrLeft)
    {
        Vector3 newPos = new Vector3(myTransform.position.x + m_SpriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
        Transform clone = Instantiate(myTransform, newPos, myTransform.rotation);
        if (needsMirror)
        {
            clone.localScale = new Vector3(clone.localScale.x * -1, clone.localScale.y, clone.localScale.z);
        }
        if (rightOrLeft == 1)
        {
            clone.GetComponent<Tiling>().hasLeftClone = true;
        }

        else
        {
            clone.GetComponent<Tiling>().hasRightClone = true;
            
        }
        clone.parent = myTransform.parent;
    }
}
