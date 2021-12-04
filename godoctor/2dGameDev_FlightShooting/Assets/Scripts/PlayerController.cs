using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName;
    [SerializeField]
    private StageData stageData;
    [SerializeField]
    private KeyCode keyCodeAttack = KeyCode.Space;
    [SerializeField]
    private KeyCode keyCodeBoom = KeyCode.Z;

    Movement2D movement2D;
    private Weapon weapon;
    private bool isDie = false;
    private Animator animator;

    private int score;
    public int Score
    {
        set => score = Mathf.Max(0, value); // score값이 음수일경우에는 0ㄴ
        get => score;
    }

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        weapon = GetComponent<Weapon>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDie == true) return;

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        movement2D.MoveTo(new Vector3(x, y, 0));


        if(Input.GetKeyDown(keyCodeAttack))
        {
            weapon.StartFiring();
        }
        else if (Input.GetKeyUp(keyCodeAttack))
        {
            weapon.StopFiring();
        }

        if(Input.GetKeyDown(keyCodeBoom))
        {
            weapon.StartBoom();
        }
    }

    private void LateUpdate()
    {
       transform.position = new Vector3(Mathf.Clamp(transform.position.x, stageData.LimitMin.x, stageData.LimitMax.x),
                                       Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y));
    }

    public void OnDie()
    {
        // 이동방향 초기화
        movement2D.MoveTo(Vector3.zero);
        animator.SetTrigger("onDie");
        // 콜라이더를 삭제하여 더이상 충돌이벤트가 발생하지 않도록 함
        Destroy(GetComponent<CircleCollider2D>());
        isDie = true;            
    }

    public void OnDieEvent()
    {
        // Save Score
        PlayerPrefs.SetInt("Score", score);
        // nextScene
        SceneManager.LoadScene(nextSceneName);

    }
}
