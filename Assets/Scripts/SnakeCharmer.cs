using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SnakeCharmer : MonoBehaviour
{
    public Flute flute;
    IKControl ikControl;
    public StarterAssetsInputs starterInputs;
    public bool _isplayng = false;
    float _maxHealth = 100;
    float _maxStamina = 10;
    public float CurrentHealth;
    public float CurrentStamina;
    public Image HealthFill;
    public Image StaminaFill;
    bool CanPlayFlute = true;
    public Color StaminaPlayColor, StaminaStopColor;
    public bool _dead = false;
    Animator _animator;
    public List<Enemy> enemies;
    public PlayerInput playerInput;
    public AudioSource CompleteSound;
    private void Awake()
    {
        ikControl = GetComponent<IKControl>();
        starterInputs = GetComponent<StarterAssetsInputs>();
        _animator = GetComponent<Animator>();
        CurrentHealth = _maxHealth;
        CurrentStamina = _maxStamina;
    }

    public void UpdateStats()
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, CurrentHealth);
        CurrentStamina = Mathf.Clamp(CurrentStamina, 0, _maxStamina);
        HealthFill.fillAmount = CurrentHealth / _maxHealth;
        StaminaFill.fillAmount = CurrentStamina / _maxStamina;
    }

    private void Update()
    {
        if(!_dead)
        {
            CheckFlute();
        }
    }
    public void TakeDamage(float damage)
    {
        CurrentHealth-= damage;
        if (CurrentHealth<=0)
        {
            Dead();
            UpdateStats();
        }
        else
        {
            _animator.SetTrigger("Hit");
        }
    }
    public void Dead()
    {
        playerInput.enabled = false;
        _animator.SetBool("Dead", true);
        _dead = true;
        StartCoroutine(Death_Delay());
    }

    IEnumerator Death_Delay()
    {
        yield return new WaitForSeconds(1f);
        FindObjectOfType<GameManager>().LevelFailed();
    }
    public void PlayFlute()
    {
        ikControl.ikActive = true;
        flute.PlayFlute();
        //CompleteSound.Play();
    }
    void CheckFlute()
    {
        if (starterInputs.Flute&& CanPlayFlute)
        {
            CurrentStamina -= Time.deltaTime;
            if (!_isplayng)
            {
                ikControl.ikActive = true;
                flute.PlayFlute();
                _isplayng = true;
                Debug.LogError("Play");
                MakeSnakeSleep();
            }
            if (CurrentStamina <= 0f)
            {
                CanPlayFlute = false;
            }
        }
        else
        {
            CurrentStamina += Time.deltaTime;
            if (_isplayng)
            {
                ikControl.ikActive = false;
                flute.StopFlute();
                _isplayng = false;
                Debug.LogError("Stop");
                WakeSnakesUp();
            }
            if (CurrentStamina >= 3f)
            {
                CanPlayFlute = true;
                StaminaFill.color = StaminaPlayColor;
            }
            else
            {
                StaminaFill.color = StaminaStopColor;
                CanPlayFlute = false;
            }
        }
        UpdateStats();
    }

    public void MakeSnakeSleep()
    {
        if(enemies.Count > 0)
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.Sleep();
            }
        }
    }

    public void WakeSnakesUp()
    {
        if (enemies.Count > 0)
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.WakeUp();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Enemy>())
        {
            enemies.Add(other.GetComponent<Enemy>());
            if(_isplayng)
            {
                other.GetComponent<Enemy>().Sleep();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            other.GetComponent<Enemy>().WakeUp();
            enemies.Remove(other.GetComponent<Enemy>());
        }
    }
}
