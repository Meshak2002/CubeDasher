using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerScript : MonoBehaviour
{
    //private bool no;
    public GameController script;
    public int TutorialFirstRun;
    public GameObject Enemy;
    public Rigidbody rb;
    public float force = 1000f;
    public float speed = 10,accsidespeed=15;
    public float sideSpeed = 10;
    public float maxX;
    public float minX;
    private bool left;
    private bool right;
    public GameObject RightText;
    public GameObject LeftText;
    public int zspeed;
    public int zOffset;

    public int CurrentLane = 1;
    public float landDistance = 5;
    [Range(1, 5)]
    public float glideControl = 5;
    private Vector2 startTouchPosition, endTouchPosition;
    private float jumpForce = 2f;
    private bool jumpAllowed = false,onground;
    private Vector3 initialPos,final;
    public bool gyroEnabled=true;
    public Vector3 movement;

    //public GameObject menuPanel;
    //private bool isFreezed;
    // Start is called before the first frame update
    void Start()
    {
        glideControl = PlayerPrefs.GetFloat("GlideValue", 5);
        rb = GetComponent<Rigidbody>();
        //no =false;
        TutorialFirstRun = PlayerPrefs.GetInt("TutorialFirstRun");
        if (TutorialFirstRun == 0)
        {
            // FirstRunCheck();
            //skipObject.SetActive(false);
            Debug.Log("FirstRun");
            PlayerPrefs.SetInt("TutorialFirstRun", 1);
        }
        else
        {

            //skipObject.SetActive(true);
            Debug.Log("Welcome Again");

        }
        left = false;
        right = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (gyroEnabled)
        {
            // Get the input from the gyroscope
            float sidewaysMovement = Input.acceleration.x; // Use the appropriate axis depending on your desired movement

            // Apply the movement to your object
             movement = new Vector3(sidewaysMovement, 0f, 0f);
            if (movement.x < 0)
            {
                left=true;
                right=false;
            }
            if (movement.x > 0.3f)
            {
                    left = false;
                    right = true;
            }
        }
        rb.AddRelativeForce(Vector3.down * 90);
        if (Input.GetButtonDown("Jump") && onground)
        {
            rb.AddForce(new Vector3(0, 40, 0), ForceMode.Impulse);
            onground = false;
        }
        if (zspeed > 10000)
        {
            Application.Quit();
        }
        SwipeCheck();
        zOffset = (int)transform.position.z;
        if (zOffset % 100 == 0)
        {
            zspeed += 5;
        }
        #region tiltControl
        float dirX = Input.acceleration.x * speed;
        GetComponent<Rigidbody>().velocity = new Vector3(dirX, 0, 0);
        #endregion

        // Invoke("Movement", 2f);
        Invoke("Parent", 2f);
        //Time.timeScale=0;
        PLayerMovement();

    }
    public void OnButtonDownRight()
    {
        right = true;
    }
    public void OnButtonDownLeft()
    {
        left = true;
        DeactivateText();
    }
    public void OnButtonUpRight()
    {
        right = false;
    }
    public void OnButtonupLeft()
    {
        left = false;
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onground = true;
        }
    }
    private void Movement()
    {
        Vector3 playerPos = transform.position;
        playerPos.x = Mathf.Clamp(playerPos.x, minX, maxX);
        transform.position = playerPos;
        transform.position = playerPos;
        if (left)
        {
            //LeftMovement();
            UnfreezGame();
            transform.Translate(Vector3.left * sideSpeed * Time.deltaTime);
        }
        if (right)
        {
            //RightMovement();
            UnfreezGame();
            transform.Translate(Vector3.right * sideSpeed * Time.deltaTime);
        }
    }
    void FixedUpdate()
    {
        //rigidbody.AddForce(0, 0, 350 * Time.deltaTime); 
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        JumpIfAllowed();
    }
    private void Parent()
    {
        Enemy.transform.parent = gameObject.transform;
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void FirstRunCheck()
    {
        FreezTimer1();
        FreezTimer2();
    }

    void FreezTimer1()
    {
        Invoke("FreezeGame", 3f);
        Invoke("RightTextActive", 3f);
    }
    void FreezTimer2()
    {
        Invoke("FreezeGame", 5f);
        Invoke("LeftTextActive", 5f);
    }
    void FreezeGame()
    {
        Time.timeScale = 0;
        //isFreezed = true;
    }
    void UnfreezGame()
    {
        Time.timeScale = 1;
        //isFreezed = false;
    }
    void LeftTextActive()
    {
        LeftText.SetActive(true);
    }
    void RightTextActive()
    {
        RightText.SetActive(true);
    }
    void DeactivateText()
    {
        LeftText.SetActive(false);
        RightText.SetActive(false);
    }
    public void PLayerMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            initialPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Calculate(Input.mousePosition);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || right)
        {
            right = false;
            UnfreezGame();
            CurrentLane++;
            if (CurrentLane == 3)
            {
                CurrentLane = 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || left)
        {
            left = false;
            UnfreezGame();
            CurrentLane--;
            if (CurrentLane == -1)
            {
                CurrentLane = 0;
            }
        }
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (CurrentLane == 0)
        {
            targetPosition += Vector3.left * landDistance;
        }
        else if (CurrentLane == 2)
        {
            targetPosition += Vector3.right * landDistance;
        }
        transform.position = Vector3.Lerp(transform.position, targetPosition, glideControl * Time.deltaTime);

    }
    void SwipeCheck()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;
        }
        if (endTouchPosition.y > startTouchPosition.y && rb.velocity.y == 0)
        {
            jumpAllowed = true;
        }
    }
    void JumpIfAllowed()
    {
        if (jumpAllowed)
        {
            rb.AddForce(Vector3.up * jumpForce);
            jumpAllowed = false;
        }
    }
    void Calculate(Vector3 finalPos)
    {
        float disX = Mathf.Abs(initialPos.x - finalPos.x);
        float disY = Mathf.Abs(initialPos.y - finalPos.y);
        if (disX > 0 || disY > 0)
        {
            if (disX > disY)
            {
                if (initialPos.x > finalPos.x)
                {
                    Debug.Log("Left");
                    left = true;
                }
                else
                {
                    Debug.Log("Right");
                    right = true;
                }
            }
            else
            {
                if (initialPos.y > finalPos.y)
                {
                    Debug.Log("Down");
                }
                else
                {
                    if (onground == true)
                    {
                        rb.AddForce(new Vector3(0, 40, 0), ForceMode.Impulse);
                        onground = false;
                    }
                }
            }
        }
    }
    public void jump()
    {
        if (onground == true)
        {
            rb.AddForce(new Vector3(0, 40, 0), ForceMode.Impulse);
            onground = false;
        }
    }
}















