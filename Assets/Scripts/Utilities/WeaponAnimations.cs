using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimations : MonoBehaviour
{
    [SerializeField] Transform Model;
    [SerializeField] Transform MagModel;
    [SerializeField] Transform FullModel;
    [SerializeField] Transform SwayTarget;

    private float t;

    private bool isIn;
    private bool isOut;

    private Vector3 currentRotation;
    private Vector3 currentPosition;
    private Vector3 targetRotation;
    private Vector3 targetPosition;

    private Vector3 currentMagRotation;
    private Vector3 currentMagPosition;
    private Vector3 targetMagRotation;
    private Vector3 targetMagPosition;

    private Vector3 currentImpulse;

    private Quaternion quat1;

    private Gun gun;
    private void Start()
    {
        gun = GetComponent<Gun>();
        SwayTarget.localPosition = new Vector3(0f, 0f, 2f);
    }
    public void Recoil(float recoilx, float recoily, float recoilz)
    {
        currentImpulse = new Vector3(-recoilx,recoily,recoilz);
        targetRotation += currentImpulse;
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, gun.GetSnappiness() * Time.deltaTime);
        currentPosition = Vector3.Slerp(currentPosition, targetPosition, gun.GetSnappiness() * Time.deltaTime);
        Model.localRotation = Quaternion.Euler(currentRotation) * quat1;
        Model.localPosition = currentPosition;
    }
    public void ADS(Vector3 newPos, Vector3 newRot)
    {
        targetRotation = Vector3.Lerp(targetRotation, newRot, gun.GetAdsSpeed() * Time.deltaTime);
        targetPosition = Vector3.Lerp(targetPosition, newPos, gun.GetAdsSpeed() * Time.deltaTime);
    }
    public IEnumerator Reload(Vector3 inpos, Quaternion inrot, float dur1, Vector3 outpos, Quaternion outrot, float dur2)
    {
        float time = 0;
        while (time <= dur1)
        {
            MagModel.localPosition = Vector3.Lerp(inpos, outpos, time / dur1);
            FullModel.localRotation = Quaternion.Slerp(inrot, outrot, time / dur1);
            time += Time.deltaTime;
            yield return null;
        }
        MagModel.localPosition = outpos;
        FullModel.localRotation = outrot;
        while (time <= dur2)
        {
            MagModel.localPosition = Vector3.Lerp(outpos, inpos, time / dur2);
            FullModel.localRotation = Quaternion.Slerp(outrot, inrot, time / dur2);
            time += Time.deltaTime;
            yield return null;
        }
        MagModel.localPosition = inpos;
        FullModel.localRotation = inrot;
    }
    public void IdleSway(float idlex, float idley, float idleSwaySpeed)
    {
        t += idleSwaySpeed * Time.deltaTime * 0.1f;
        transform.forward = (SwayTarget.transform.position - transform.position).normalized;
        SwayTarget.transform.localPosition = new Vector3(-Mathf.Sin(14 * Mathf.PI * t) * idlex, Mathf.Cos(21 * Mathf.PI * t)* idley, 1f);
    }
    public void LookSway(float smooth, float swayMultiplier)
    {
        quat1 = Quaternion.Slerp(quat1, (Quaternion.AngleAxis(-Input.GetAxisRaw("Mouse Y") * swayMultiplier, Vector3.right) * Quaternion.AngleAxis(Input.GetAxisRaw("Mouse X") * swayMultiplier, Vector3.up)), smooth * Time.deltaTime);
    }
    public Vector3 GetCurrentRotation()
    {
        return currentImpulse;
    }
}
