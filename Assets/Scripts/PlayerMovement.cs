using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float anglePerSecond;
    [SerializeField] private float swimForce;
    private Animator anim;
    private Rigidbody2D rb;
    public GameObject gameOverMusic;
    public bool isDead = false;
    public GameObject playerDeath;
    public GameObject finalScoreScreen;
    public bool playerHitRoutine = false;
    public int health;
    public int Score = 0;
    public Text finalScore;
    public Text livesText;
    public Text scoreText;
    SoundManager soundManager;
    private ShakeManager shake;
    Renderer rend;
    Color c;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rend = GetComponent<Renderer>();
        //finalScoreScreen = GameObject.FindGameObjectWithTag("FinalScore");
        soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
        c = rend.material.color;
        shake = GameObject.FindGameObjectWithTag("Shake").GetComponent<ShakeManager>();
    }

    void Update()
    {
        Vector3 rotation = transform.localEulerAngles;
        if (health <= 0)
        {
            Physics2D.IgnoreLayerCollision(6, 8, false);
            isDead = true;
            soundManager.PlaySFX(soundManager.death);
            soundManager.PlaySFX(soundManager.endMusic);
            finalScoreScreen.SetActive(true);
            shake.CamShake();
            transform.rotation = Quaternion.Euler(0, 0, 0);
            Instantiate(gameOverMusic, transform.position, transform.rotation);
            Instantiate(playerDeath, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rotation.z += Time.deltaTime * anglePerSecond;
            transform.localEulerAngles = rotation;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rotation.z -= Time.deltaTime * anglePerSecond;
            transform.localEulerAngles = rotation;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isDead == false)
        {
            soundManager.PlaySFX(soundManager.swim);
            StartCoroutine(Swim());
            //anim.Play("playerswim");
            rb.AddForce(transform.up * swimForce, ForceMode2D.Impulse);
        }
        livesText.text = health.ToString();
        scoreText.text = Score.ToString();
        finalScore.text = Score.ToString();
        

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy") && health > 0)
        {
            /*Vector2 direction = (transform.position - col.transform.position).normalized;
            Vector2 knockback = direction * knockBackForce;
            rb.AddForce(knockback, ForceMode2D.Impulse);
            StartCoroutine(hit());*/
            StartCoroutine("playerHit");
            soundManager.PlaySFX(soundManager.bulletHit);
            shake.CamShake();
            health--;
            
        }
    }

    private IEnumerator playerHit() 
    {
        playerHitRoutine = true;
        Physics2D.IgnoreLayerCollision(6, 8, true);
        c.a = 0f;
        rend.material.color = c;
        yield return new WaitForSeconds(.2f);
        c.a = 1f;
        rend.material.color = c;
        yield return new WaitForSeconds(.2f);
        c.a = 0f;
        rend.material.color = c;
        yield return new WaitForSeconds(.2f);
        c.a = 1f;
        rend.material.color = c;
        yield return new WaitForSeconds(.2f);
        c.a = 0f;
        rend.material.color = c;
        yield return new WaitForSeconds(.2f);
        c.a = 1f;
        rend.material.color = c;
        yield return new WaitForSeconds(.2f);
        c.a = 0f;
        rend.material.color = c;
        yield return new WaitForSeconds(.2f);
        c.a = 1f;
        rend.material.color = c;
        yield return new WaitForSeconds(.2f);
        c.a = 0f;
        rend.material.color = c;
        yield return new WaitForSeconds(.2f);
        c.a = 1f;
        rend.material.color = c;
        Physics2D.IgnoreLayerCollision(6, 8, false);
        anim.Play("playerIdle");
        playerHitRoutine = false;
    }

    private IEnumerator Swim() 
    {
        anim.Play("playerswim");
        yield return new WaitForSeconds(.35f);
        anim.Play("playerIdle");
    }
}
