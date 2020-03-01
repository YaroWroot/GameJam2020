using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    public Transform _handBow;
    public Transform _unequippedBow;
    public Transform _sword;
    public float _bowCoolDown = 1.0f;
    public float _bowMultiCoolDown = 5.0f;

    private float _bowAnimTime = 3.0f;
    private bool _bowCoolDownCheck = false;
    private bool _bowMultiCoolDownCheck = false;
    private int _multiCount = 0;

    private bool _uiBow = false;

    private void Awake()
    {
    }

    private void FixedUpdate()
    {
        if (_uiBow)
        {
            UIBow();
        }
    }

    float lerp = 0.0f;
    float startSize = 0.5f;
    float endSize = 1;
    private void UIBow()
    {
        lerp += Time.deltaTime / _bowMultiCoolDown;
        FindObjectOfType<UI_MultiArrow>().slider.value = lerp;
    }

    public void FireBow(Animator animator)
    {
        if (!_bowCoolDownCheck)
        {
            var clips = animator.runtimeAnimatorController.animationClips;
            _bowAnimTime = clips.First(e => e.name == "BowShoot").length;
            _bowCoolDownCheck = true;
            _handBow.gameObject.SetActive(true);
            _unequippedBow.gameObject.SetActive(false);
            _sword.gameObject.SetActive(false);
            animator.SetTrigger("BowShoot");
            StartCoroutine(SwapBowBack());
            StartCoroutine(ShootArrow());
            //EventManager.TriggerEvent("BowCoolDown");
            StartCoroutine(BowCoolDown());
        }
    }

    public void FireMultipleBow(Animator animator)
    {
        if (!_bowMultiCoolDownCheck)
        {
            var clips = animator.runtimeAnimatorController.animationClips;
            _bowAnimTime = clips.First(e => e.name == "BowShoot").length;
            _bowMultiCoolDownCheck = true;
            _handBow.gameObject.SetActive(true);
            _unequippedBow.gameObject.SetActive(false);
            _sword.gameObject.SetActive(false);
            animator.SetTrigger("BowShoot");
            StartCoroutine(SwapBowBack());
            StartCoroutine(ShootArrowMultiple());
            //EventManager.TriggerEvent("BowCoolDown");
            StartCoroutine(BowMultiCoolDown());
            _uiBow = true;
            FindObjectOfType<UI_MultiArrow>().slider.value = startSize;
        }
    }

    private IEnumerator SwapBowBack()
    {
        yield return new WaitForSeconds(_bowAnimTime);

        _unequippedBow.gameObject.SetActive(true);
        _sword.gameObject.SetActive(true);
        _handBow.gameObject.SetActive(false);
    }

    private IEnumerator ShootArrow()
    {
        yield return new WaitForSeconds((_bowAnimTime / 2));
        var arrow = Instantiate(GameAssets.i.PlayerArrowProjectile, transform.position + transform.forward, transform.rotation);
        arrow.GetComponent<Rigidbody>().AddForce(transform.forward * arrow.GetComponent<Projectile>()._speed);
    }

    private IEnumerator ShootArrowMultiple()
    {
        yield return new WaitForSeconds(0.5f);

        var arrow = Instantiate(GameAssets.i.PlayerArrowProjectile, transform.position + transform.forward, transform.rotation);
        arrow.GetComponent<Rigidbody>().AddForce(transform.forward * arrow.GetComponent<Projectile>()._speed);
        _multiCount++;

        if(_multiCount != 3)
        {
            StartCoroutine(ShootArrowMultiple());
        }
        else
        {
            _multiCount = 0;
        }
    }

    private IEnumerator BowCoolDown()
    {
        yield return new WaitForSeconds(_bowCoolDown);
        _bowCoolDownCheck = false;
    }

    private IEnumerator BowMultiCoolDown()
    {
        yield return new WaitForSeconds(_bowCoolDown);
        _bowMultiCoolDownCheck = false;
    }

    public void AttackNormal(GameObject target)
    {
        target.GetComponent<Health>().TakeDamage(Random.Range(2, 10));
    }
}
