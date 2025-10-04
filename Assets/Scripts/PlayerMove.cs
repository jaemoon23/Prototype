using UnityEngine;
using UnityEngine.UI;
public class PlayerMove : MonoBehaviour
{   
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;

    [Header("Buttons")]
    [SerializeField] private Button leftButton; 
    [SerializeField] private Button rightButton;
    [SerializeField] private Button jumpButton;

    [Header("Player Move Settings")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Player Jump Settings")]
    [SerializeField] private float jumpForce = 300f;
    

    [Header("Variables")]
    private bool m_IsLeftButtonDown;
    private bool m_IsRightButtonDown;
    private bool isJump = false;
    private bool isGrounded = true;
    private float previousYVelocity = 0f;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    private void Start()
    {
        jumpButton.onClick.AddListener(OnClickJumpButton);
    }

    private void Update() 
    {
        
        HandleMovement();
        Jump();

        if (previousYVelocity > 0 && rb.linearVelocity.y < 0)
        {
            animator.SetBool("isFalling", true);
            animator.SetBool("isJumping", false);
        }
        previousYVelocity = rb.linearVelocity.y;
    }


    private void HandleMovement()
    {
        // 키 입력 체크
        bool leftKey = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || m_IsLeftButtonDown;
        bool rightKey = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || m_IsRightButtonDown;
 
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
    private void OnClickJumpButton()
    {
        if (isGrounded)
        {
            isJump = true;
        }
    }
    private void Jump()
    {
        if (isJump)
        {
            rb.AddForce(Vector2.up * jumpForce);
            animator.SetBool("isJumping", true);
            isJump = false;
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = false;
            isGrounded = true; // 지면에 닿았을 때 지면 상태 활성화
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
            animator.SetBool("isIdle", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // 지면에서 벗어날 때 지면 상태 비활성화
        }
    }

    public void LeftButtonDown() { m_IsLeftButtonDown = true; }
    public void LeftButtonUp() { m_IsLeftButtonDown = false; }
    public void RightButtonDown() { m_IsRightButtonDown = true; }
    public void RightButtonUp() { m_IsRightButtonDown = false; }
}
