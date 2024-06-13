using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            print("Hit " +  collision.gameObject.name + " !");
            CreateBulletImpactEffect(collision);
            Destroy(gameObject);

        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            print("Hit Wall");
            CreateBulletImpactEffect(collision);
            Destroy(gameObject);

        }
    }

    void CreateBulletImpactEffect(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];

        GameObject hole = Instantiate(GlobalReferences.Instance.bulletImpactEffectPrefab, contact.point, Quaternion.LookRotation(contact.normal));

        hole.transform.SetParent(collision.gameObject.transform);
    }
}
