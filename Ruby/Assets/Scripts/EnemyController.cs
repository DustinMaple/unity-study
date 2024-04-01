using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //最大修复次数
    public int maxFixTime = 1;
    public AudioClip fixAudio;
    private float _exitTime;

    private bool _fixFinish = false;
    private int _fixTime = 0;

    public bool FixFinish
    {
        get => _fixFinish;
    }

    // Update is called once per frame
    void Update()
    {
        if (_exitTime > 0)
        {
            _exitTime -= Time.deltaTime;
            if (_exitTime < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Fix()
    {
        _fixTime++;

        if (_fixTime >= Mathf.Max(1, maxFixTime))
        {
            _fixFinish = true;
            _exitTime = 2f;
            Animator animator = GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("Fixed");
            }

            BotMove botMove = GetComponent<BotMove>();
            if (botMove != null)
            {
                botMove.enabled = false;
            }

            Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
            if (rigidbody2D != null)
            {
                rigidbody2D.simulated = false;
            }

            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(fixAudio);
            }
        }
    }
}