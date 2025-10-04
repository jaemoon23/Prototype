using UnityEngine;
using UnityEngine.UI;
public class PlayerMove : MonoBehaviour
{   
    [Header("Animator")]
    [SerializeField] private Animator animator;

    [Header("Button")]
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;

    [Header("Player Move Settings")]
    [SerializeField] private float moveSpeed = 5f;

    private bool m_IsLeftButtonDown;
    private bool m_IsRightButtonDown;

    private void Start()
    {

    }

    private void Update() 
    {
        // 왼쪽과 오른쪽 키 입력 동시 체크
        bool leftKey = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || m_IsLeftButtonDown;
        bool rightKey = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || m_IsRightButtonDown;
        
        // 이동
        if (leftKey || rightKey)
        {
            if (leftKey && !rightKey) // 왼쪽만 눌렸을 때
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                if (transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
                animator.SetBool("isRunning", true);
                animator.SetBool("isIdle", false);
            }
            else if (rightKey && !leftKey) // 오른쪽만 눌렸을 때
            {
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                if (transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
                animator.SetBool("isRunning", true);
                animator.SetBool("isIdle", false);
            }
        }
        else if (!leftKey && !rightKey || leftKey && rightKey) // 아무 키도 안 눌렸거나 양쪽 다 눌렸을 때
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", true);
        }
    }

    public void LeftButtonDown() { m_IsLeftButtonDown = true; }
    public void LeftButtonUp() { m_IsLeftButtonDown = false; }
    public void RightButtonDown() { m_IsRightButtonDown = true; }
    public void RightButtonUp() { m_IsRightButtonDown = false; }
}
