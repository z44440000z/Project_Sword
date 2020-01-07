using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    [Header("Game Value")]
    public int life = 3;
    public int combo = 0;
    private float score;
    public int bestCombo;
    [Header("Value Setting")]
    public float baseScore = 10f;
    public Animator anim;
    private Vector2 lastTapPos;
    public float jumpSpeed = 5f;
    public float gravitySpeed = 3f;
    private Rigidbody rb;
    public OnGroundSensor sensor;
    [Header("State Setting")]
    public bool isjump;
    public bool isdead;
    [Header("Attack Setting")]
    public Atkzone atkzone;
    public ObstacleType atkType = ObstacleType.none;
    [Header("UI Setting")]
    public Transform comboCanvas;
    public Text scoreText;
    public Image[] lifesImg;
    public GameObject ResultPanel;
    public Text bestComboText;
    public Text resultScoreText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ResultPanel.SetActive(false);
        scoreText.text = Mathf.Round(score).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //死亡
        if (isdead)
        { return; }
        if (life <= 0)
        {
            isdead = true;
            anim.SetTrigger("die");
            ResultPanel.SetActive(true);
            bestComboText.text = bestCombo.ToString();
            resultScoreText.text = scoreText.text;
            scoreText.gameObject.SetActive(false);
            GameManager.Instance.audiosource.Stop();
            SaveSystem.Save(new SaveData(score, bestCombo));
        }

        //落下動作
        anim.SetFloat("height", sensor.height);
        if (rb.velocity.y < 0)
            anim.SetBool("isFall", true);
        else if (sensor.height <= 0.1f)
            anim.SetBool("isFall", true);
        else
            anim.SetBool("isFall", false);
        //左右走
        if (Input.GetMouseButton(0) && !isjump)
        {
            Vector2 curTapPos = Input.mousePosition;

            if (lastTapPos == Vector2.zero)
                lastTapPos = curTapPos;

            float delta = lastTapPos.x - curTapPos.x;
            lastTapPos = curTapPos;

            rb.AddForce(Vector3.left * delta);
            anim.SetFloat("left", Mathf.Clamp(delta, -1f, 1f));
        }
        //停止走動
        if (Input.GetMouseButtonUp(0))
        {
            lastTapPos = Vector2.zero;
            anim.SetFloat("left", 0f);
        }
        //跳躍
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Player" && !isjump)
            {
                Debug.DrawLine(Camera.main.transform.position, hit.transform.position, Color.red, 0.1f, true);
                Debug.Log(hit.transform.name);
                anim.SetTrigger("jump");
            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        //落地
        if (other.transform.tag == "Ground")
        {
            isjump = false;
            rb.velocity = Vector3.zero; // Remove velocity to not make the ball jump higher after falling done a greater distance
            //Combo被斷
            if (GameManager.Instance.IfCubeExist())
            { combo = 0; }
        }
        //損失生命
        if (other.transform.tag == "A" || other.transform.tag == "B")
        {
            if (!isjump)
            {
                if (life > 0)
                {
                    life--;
                    combo = 0;
                    lifesImg[life].enabled = false;
                }
                else
                { }
            }
        }
    }
    //動畫事件-跳躍
    public void Jump()
    {
        isjump = true;
        //Vector3 newV = Vector3.up * Mathf.Sqrt(height);
        //rb.velocity = newV * Mathf.Abs(speed);
        rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
    }
    //延長滯空的跳躍
    public void Jump(float speed)
    {
        isjump = true;
        rb.AddForce(Vector3.up * speed, ForceMode.Impulse);
    }
    //左邊的攻擊按鈕
    public void LeftAttackButton()
    {
        if (isdead)
            return;
        anim.SetBool("isLeft", true);
        anim.SetTrigger("attack");
        atkType = ObstacleType.Left;
    }
    //右邊的攻擊按鈕
    public void RightAttackButton()
    {
        if (isdead)
            return;
        anim.SetBool("isLeft", false);
        anim.SetTrigger("attack");
        atkType = ObstacleType.Right;

    }
    public void OnIdleUpdate()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("attack"), Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("attack")), 0f, .2f));
    }
    public void OnAttackUpdate()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("attack"), Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("attack")), 1f, 1f));
    }
    //動畫事件-攻擊
    public void OnAttack()
    {
        if (atkType == atkzone.type && atkType != ObstacleType.none)
        {
            //Combo增加
            combo++;
            //顯示Combo數
            FloatingTextController.CreateFloatingText(combo.ToString(), comboCanvas);
            //比較BestCombo，大於則進行替代
            if (combo > bestCombo)
            { bestCombo = combo; }
            //加分數
            score += baseScore * (1 + (combo * 0.1f));
            scoreText.text = Mathf.Round(score).ToString();
            //確認場上尚存的方塊數
            GameManager.Instance.brokeCount++;
            //滯空
            if (isjump)
            { Jump(10); }
            //破壞方塊
            Destroy(atkzone.Obstacle.gameObject);
        }
        else
        { }
    }
}
