using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float timeToDestroy = 2f;

    public int damageAmount = 1;
    public float speed = 50f;

    public List<string> tagsToHit;

    public SFXType hitAudio = SFXType.Type_04;

    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * (Time.deltaTime * speed));
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach(var t in tagsToHit)
        {
            if(collision.transform.tag == t)
            {
                var damageable = collision.transform.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    SFXPool.Instance.Play(hitAudio);

                    Vector3 dir = collision.transform.position - transform.position;

                    dir = -dir.normalized;

                    dir.y = 0;

                    damageable.Damage(damageAmount, dir);
                    Destroy(gameObject);
                }

                break;
            }
        }
    }
}
