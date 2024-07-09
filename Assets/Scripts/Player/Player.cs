using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    Rigidbody rb;
    public Transform body;
    [SerializeField] Vector3 orgPos;

    [SerializeField] GameObject block;

    public int blockCollected;
    [SerializeField] float blockHeight = .2f;

    public bool Moving
    {
        get { return moveDirection != Vector3.zero; }
        set { }
    }

    public bool finish;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GetInput();

        CheckDestination();

        Debug.DrawLine(transform.position, transform.position + moveDirection * .6f, Color.red);
    }

    private void FixedUpdate()
    {
        CollectBlock();
        DiscardBlock();

        Move();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("changeDir"))
        {
            GameObject purpleBlock = col.gameObject;
            purpleBlock.GetComponent<Animator>().ResetTrigger("trigger");
            purpleBlock.GetComponent<Animator>().SetTrigger("trigger");
            ChangeDir(purpleBlock);
        }
    }

    public override void OnInit()
    {
        base.OnInit();

        transform.position = orgPos;

        moveDirection = Vector3.zero;

        changeDir = false;
        finish = false;
        Moving = false;
        blockCollected = 0;
    }

    public override void OnDespawn()
    {
        base.OnDespawn();

        OnReset();
    }

    public void OnReset()
    {
        moveDirection = Vector3.zero;
        ResetInput();

        if (blockCollected > 0)
        {
            int childs = transform.childCount;
            for (int i = childs - 1; i > 1; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
                body.position -= new Vector3(0, blockHeight, 0);
            }
        }
    }

    #region Movement

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Vector3 moveDirection;

    [SerializeField] Vector2 startTouchPosition;
    [SerializeField] Vector2 endTouchPosition;
    [SerializeField] Vector3 direction;

    void GetInput()
    {
        if (!GameManager.IsState(GameState.GamePlay) || finish) { return; }

        if (!Moving)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startTouchPosition = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {

                endTouchPosition = Input.mousePosition;
                Moving = true;
                direction = endTouchPosition - startTouchPosition;
                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                {
                    direction.y = 0;
                    direction.Normalize();
                }
                else
                {
                    direction.x = 0;
                    direction.z = direction.y;
                    direction.y = 0;
                    direction.Normalize();
                }
            }
        }
        if (!changeDir)
        {
            moveDirection = direction;
        }
    }

    void ResetInput()
    {
        startTouchPosition = Vector2.zero;
        endTouchPosition = Vector2.zero;
        direction = Vector3.zero;
    }

    void Move()
    {
        if (Moving)
        {
            rb.MovePosition(transform.position + moveSpeed * Time.fixedDeltaTime * moveDirection);
        }
    }

    #endregion

    #region RayCast

    [SerializeField] LayerMask wallLayer;
    [SerializeField] LayerMask blockLayer;
    [SerializeField] LayerMask bridgeLayer;
    [SerializeField] LayerMask changeDirLayer;

    bool changeDir;

    void CheckDestination()
    {
        Physics.Raycast(transform.position, moveDirection, out RaycastHit hit, .6f, wallLayer);

        if (hit.collider != null)
        {
            moveDirection = Vector3.zero;
            changeDir = false;
            FixPlayerPosition();
        }
    }

    void CollectBlock()
    {
        Physics.Raycast(transform.position, moveDirection, out RaycastHit hit, .6f, blockLayer);

        if (hit.collider != null)
        {
            GameObject obj = hit.collider.gameObject;

            blockCollected++;

            body.position = Vector3.MoveTowards(body.position, body.position + new Vector3(0, blockHeight, 0), 20);

            Instantiate(block, body.position, block.transform.rotation).transform.SetParent(transform);

            obj.GetComponent<BoxCollider>().enabled = false;
            obj.GetComponent<MeshRenderer>().enabled = false;

            ChangeAnim(Constants.ANIM_JUMP);
            float length = anim.runtimeAnimatorController.animationClips.First(a => a.name == "Take 04").length;
            Invoke(nameof(ResetAnim), length);
        }
    }

    void DiscardBlock()
    {
        Physics.Raycast(transform.position, moveDirection, out RaycastHit hit, .6f, bridgeLayer);

        if (hit.collider != null)
        {
            GameObject obj = hit.collider.gameObject;

            if (blockCollected > 0)
            {
                int childs = transform.childCount;
                Destroy(transform.GetChild(childs - 1).gameObject);

                body.position = Vector3.MoveTowards(body.position, body.position - new Vector3(0, blockHeight, 0), 20);

                obj.GetComponent<BoxCollider>().enabled = false;
                obj.GetComponent<MeshRenderer>().enabled = true;

                blockCollected--;
            }
            else if (blockCollected == 0)
            {
                moveDirection = Vector3.zero;
                FixPlayerPosition();
            }
        }
    }

    void ChangeDir(GameObject obj)
    {
        changeDir = true;

        List<Vector3> avaiDir = obj.GetComponent<ChangeDir>().availableDir;
        Vector3 dir = moveDirection;

        for (int i = 0; i < avaiDir.Count; i++)
        {
            if (avaiDir[i] == -dir)
            {
                continue;
            }
            else
            {
                FixPlayerPosition();
                moveDirection = avaiDir[i];
            }
        }

        ResetInput();
    }

    void FixPlayerPosition()
    {
        Vector3 translation = new(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
        transform.position = Vector3.Lerp(transform.position, translation, 50);
    }

    //void SetDirection(Vector3 direction)
    //{
    //    if (!Occupied(direction))
    //    {
    //        this.moveDirection = direction;
    //    }
    //}

    //bool Occupied(Vector3 direction)
    //{
    //    bool hit = Physics.BoxCast(transform.position, Vector3.one * 0.4f, direction, Quaternion.identity, .6f, wallLayer);
    //    return hit;
    //}

    #endregion

}
