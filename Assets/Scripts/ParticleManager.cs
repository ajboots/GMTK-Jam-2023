using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private ParticleSystem dirt;

    [SerializeField]
    private ParticleSystem blood;

    [SerializeField]
    private ParticleSystem blood_pool;

    public void playDirt(Vector3 pos)
    {
        ParticleSystem p = ParticleSystem.Instantiate(dirt, pos, Quaternion.identity);
        Destroy(p.gameObject, 1.0f);
    }

    public void playBlood(Vector3 pos, Quaternion angle, GameObject parent)
    {
        ParticleSystem p = ParticleSystem.Instantiate(blood, pos, angle);
        ParticleSystem pool = ParticleSystem.Instantiate(
            blood_pool,
            pos + Vector3.down * 0.1f,
            Quaternion.Euler(-90, 0, 0)
        );
        Destroy(p.gameObject, 2.0f);
        Destroy(pool.gameObject, 100.0f);
    }

    void Start() { }

    // Update is called once per frame
    void Update() { }
}
