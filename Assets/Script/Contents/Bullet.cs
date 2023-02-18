using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Camera mainCam;
    [SerializeField]
    GameObject boomEft;
    [SerializeField]
    GameObject iceEft;
    [SerializeField]
    AudioClip iceSound;
    public int damage;
    RaycastHit hit;
    Vector3 dir;
    AudioSource soundSource;
    bool move = true;
    private void OnEnable()
    {
        soundSource = GetComponent<AudioSource>();
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();        
        int layerMask = ((1 << LayerMask.NameToLayer("Player")) | (1 << LayerMask.NameToLayer("UI")) | (1 << LayerMask.NameToLayer("Monster")));
        var ray = mainCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 1f));

        if (Physics.Raycast(ray, out hit, 1000, ~layerMask))
        {
            dir = hit.point;
        }
    }
    private void Update()
    {
        if(move == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, dir, Time.deltaTime * 15);
        }       
    }

    /*private void Hit()
    {
        if (transform.childCount < 6)
        {
            //if (Physics.SphereCast(transform.position, transform.lossyScale.x, transform.forward, out hitInfo, 0.5f))
            if (Physics.SphereCast(transform.position, 1, transform.forward, out hitInfo, 0.5f))
            {
                if (hitInfo.collider.CompareTag("Monster"))
                {
                    SkeletonController skeleton = hitInfo.collider.GetComponent<SkeletonController>();
                    skeleton.SetDamage(damage);
                    Boom();
                }
                if (hitInfo.collider.CompareTag("Boss"))
                {
                    BossController boss = hitInfo.collider.GetComponent<BossController>();
                    boss.SetDamage(damage);
                    Boom();
                }
                else
                {
                    Boom();
                }
            }
        }       
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (transform.childCount > 4) return;
        if (other.CompareTag("Monster"))
        {
            SkeletonController skeleton = other.GetComponent<SkeletonController>();
            skeleton.SetDamage(damage);
            Boom();
        }
        if (other.CompareTag("Boss"))
        {
            BossController boss = other.GetComponent<BossController>();
            boss.SetDamage(damage);
            Boom();
        }
        if (other.CompareTag("feature") || other.CompareTag("Portal"))
        {
            Boom();
        }
    }
    void Boom()
    {        
        GameObject go = Managers.Resource.Instantiate(boomEft, transform);
        StartCoroutine(Destroygo(go));
        StartCoroutine(Destroygo(gameObject));
        iceEft.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        soundSource.clip = iceSound;
        soundSource.Play();
        move = false;
    }

    IEnumerator Destroygo(GameObject go)
    {
        yield return new WaitForSeconds(1f);
        Destroy(go);
    }
}

