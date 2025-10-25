using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GroundAttackAnimationEvent : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject groundSmashingPrefab;
    [SerializeField] private float projectileSpeed = 10f;
    private Vector2[] normalDi = { new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1) }; //ซ้าย ขวา บน ล่าง
    private Vector2[] diagonalDi = { new Vector2(1, 1), new Vector2(-1, 1), new Vector2(1, -1), new Vector2(-1, -1) }; //มุมเฉียง



    public void randomAttackVariant()
    {
        int random = UnityEngine.Random.Range(0, 3);
        switch(random)
        {
            case 0:
                ProjectileSmash(normalDi);
                break;
            case 1:
                ProjectileSmash(diagonalDi);
                break;
            default:
                GroundSmash();
                break;
        }
    }

    public void ProjectileSmash(Vector2[] Direction)
    {
        for (int i = 0; i < Direction.Length; i++)
        {

            GameObject projectile = Instantiate(projectilePrefab,
            new Vector2(transform.position.x + Direction[i].x,
            transform.position.y + Direction[i].y),
            quaternion.identity);

            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(Direction[i] * projectileSpeed, ForceMode2D.Impulse);
        }
    }
    
    public void GroundSmash()
    {
        Instantiate(groundSmashingPrefab, transform.position, quaternion.identity);   
    }
}
