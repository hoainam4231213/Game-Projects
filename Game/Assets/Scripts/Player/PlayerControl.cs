using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerControl : MonoBehaviour
{
    [NonSerialized]
    public Transform trans;
    public CharacterController characterController;
    public PlayerDatabinding databinding;
    public WeaponControl weaponControl;
    private Vector3 moveDir;
    public float speed;
    private bool isGround;
    public bool isAim;
    private int maxHP = 200;
    private int hp;
    public float shootRange = 9;
    public List<Transform> enemy_list = new List<Transform>();
    public int HP
    {
        get
        {
            return hp;
        }
    }

    public int MaxHP
    {
        get
        {
            return maxHP;
        }
    }
    public event Action<int,int> OnHPChange;
    // Start is called before the first frame update
    private void Awake()
    {
        trans = transform;
        weaponControl = gameObject.GetComponent<WeaponControl>();
    }
    void Start()
    {
        hp = maxHP;
        OnHPChange?.Invoke(hp,maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = InputControlPlayer.move;
        moveDir = new Vector3(move.x, 0, move.y);
        // Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hitInfo;
        //   if (Physics.Raycast(r, out hitInfo, 100, 1 << 7, QueryTriggerInteraction.Ignore)) ;
        //  Vector3 mousePos = hitInfo.point - trans.position;
        //  mousePos.Normalize();
        //   mousePos.y = trans.position.y;

        if (isAim)
        {
            Vector3 aimDir = Quaternion.Euler(0, 45f - trans.eulerAngles.y, 0) * moveDir;
            moveDir = Quaternion.Euler(0, 45f, 0) * moveDir;
            databinding.MoveDir = aimDir;
        }
        else
        {
            if (moveDir.magnitude > 0)
            {
                moveDir = Quaternion.Euler(0, 45f, 0) * moveDir;
                Quaternion q = Quaternion.LookRotation(moveDir, Vector3.up);
                trans.rotation = q;

            }
            databinding.MoveDir = new Vector3(0, 0, moveDir.magnitude);
        }
        isGround = characterController.isGrounded;
        Vector3 pos = moveDir;
        pos.y = isGround ? 0 : -1;

        characterController.Move(pos * Time.deltaTime * speed);

        DectectEnemy();
    }

    public void OnDamge(EnemyDamageData enemyDamageData)
    {
        hp -= enemyDamageData.damage;
        if (hp <= 0)
        {
            hp = 0;
            MissionControl.instance.OnPlayerDead();
        }
        OnHPChange?.Invoke(hp,maxHP);
    }

    public void DectectEnemy()
    {
        if (enemy_list.Count > 0)
        {
            Vector3 dir = enemy_list[0].position - trans.position;
            dir.Normalize();
            Quaternion q = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z), Vector3.up);
            trans.rotation = Quaternion.Slerp(trans.rotation, q, Time.deltaTime * 360);
            isAim = true;
            weaponControl.currentGun.OnFire(true);
        }
        else
        {
            isAim = false;
            weaponControl.currentGun.OnFire(false);
        }
    }

    public void AddEnemyToList(Transform trans)
    {
        if (!enemy_list.Contains(trans))
            enemy_list.Add(trans);
    }

    public void RemoveEnemyFromList(Transform trans)
    {
        enemy_list.Remove(trans);
    }
}
