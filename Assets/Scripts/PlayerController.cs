using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public GameObject horizontalRay;
    public GameObject verticalRay;
    public float fireRate = .5f;
    private float nextFire = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public static class FireDirection
    {
        public static readonly Dictionary<Direction, string> Directions = new Dictionary<Direction, string>()
        {
            { Direction.Up, "[8]" },
            { Direction.Down, "[5]" },
            { Direction.Left, "[4]" },
            { Direction.Right, "[6]" },
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("[6]") && CanFire())
        {
            GameObject bullet = Instantiate(horizontalRay, transform.position, Quaternion.identity) as GameObject;
            Fire(bullet, transform.right, 10);
        }
        if (Input.GetKey("[4]") && CanFire())
        {
            GameObject bullet = Instantiate(horizontalRay, transform.position, Quaternion.identity) as GameObject;
            Fire(bullet, transform.right, -10);
        }
        if (Input.GetKey("[8]") && CanFire()) // Up
        {
            GameObject bullet = Instantiate(verticalRay, transform.position, Quaternion.identity) as GameObject;
            Fire(bullet, transform.up, 10);
        }
        if (Input.GetKey("[5]") && CanFire()) //Down
        {
            GameObject bullet = Instantiate(verticalRay, transform.position, Quaternion.identity) as GameObject;
            Fire(bullet, transform.up, -10);
        }

        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        if (Math.Abs(horizontal) > .5f)
        {
            transform.Translate(new Vector3(horizontal * moveSpeed * Time.deltaTime, 0f, 0f));
        }

        if (Math.Abs(vertical) > .5f)
        {
            transform.Translate(new Vector3(0f, vertical * moveSpeed * Time.deltaTime, 0f));
        }
    }

    public void UpdateNextFire() => nextFire = Time.time + fireRate;

    private bool CanFire() => Time.time > nextFire;

    private void Fire(GameObject projectile, Vector2 direction, float magnitude)
    {
        projectile.GetComponent<Rigidbody2D>().AddForce(direction * magnitude, ForceMode2D.Impulse);
        UpdateNextFire();
    }
}
