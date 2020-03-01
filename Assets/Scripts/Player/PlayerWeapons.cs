using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    public Transform _handBow;
    public Transform _unequippedBow;
    public Transform _sword;
    public float _bowCoolDown = 5.0f;

    private float _bowAnimTime = 3.0f;
    private bool _bowCoolDownCheck = false;

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
            StartCoroutine(BowCoolDown());
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
        yield return new WaitForSeconds((_bowAnimTime / 2) + 0.5f);
        var arrow = Instantiate(GameAssets.i.PlayerArrowProjectile, transform.position + transform.forward, transform.rotation);
        arrow.GetComponent<Rigidbody>().AddForce(transform.forward * arrow.GetComponent<Projectile>()._speed);
    }

    private IEnumerator BowCoolDown()
    {
        yield return new WaitForSeconds(_bowCoolDown);
        _bowCoolDownCheck = false;
    }

    public void AttackNormal(GameObject target)
    {
        target.GetComponent<Health>().TakeDamage(Random.Range(2, 10));
    }
}
