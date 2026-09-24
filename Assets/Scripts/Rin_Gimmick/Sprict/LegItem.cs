using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LegItem : MonoBehaviour
{
    private bool m_playerTouch = false;

    public float ItemMoveSpeed = 2.0f;

    public float ItemMoveHeight = 0.5f;

    private Vector3 m_startPosition;

    private float m_time = 0.0f;

    public Image GetItemNotice;

    public float NoticeDisplayTime = 3.0f;


    private Animator animator;
    public GameObject LegItemImage;
    void Start()
    {
        m_startPosition = transform.position;

        if (GetItemNotice != null) { GetItemNotice.gameObject.SetActive(false); }
    }

    void Update()
    {
        if (m_playerTouch)
        {
            ItemGet();
        }
        else
        {
            ItemMoving();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_playerTouch = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_playerTouch = true;
        }
    }

    private void ItemMoving()
    {
        m_time += Time.deltaTime;

        float offsetY = Mathf.Sin(m_time * ItemMoveSpeed) * ItemMoveHeight;

        transform.position = m_startPosition + Vector3.up * offsetY;
    }

    private void ItemGet()
    {
        if (GetItemNotice != null)
        {
            animator.SetTrigger("Bubble Breaking");

            GetItemNotice.gameObject.SetActive(true);
            CancelInvoke(nameof(HideItemNotice)); Invoke(nameof(HideItemNotice), NoticeDisplayTime);
            Debug.Log("Get " + gameObject.name);
            Destroy(gameObject);

            LegItemImage.transform.localPosition -= Vector3.down;
        }
    }

    private void HideItemNotice() 
    {
        if (GetItemNotice != null) {
            GetItemNotice.gameObject.SetActive(false); 
        }
    }
}
