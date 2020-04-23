using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CadenaController : MonoBehaviour
{
    public float rotationSpeed;
    public float rotationMangetRadius;
    public List<Transform> roulettes;
    public int password;
    public UnityEvent callbacks;

    private int currentRoulette = 0;
    private int[] number;
    private Coroutine rotation;

    private void Start()
    {
        number = new int[roulettes.Count];
        for (int i = 0; i < number.Length; i++)
        {
            number[i] = 9;
        }
    }

    void Update()
    {
        if(Input.GetButtonDown("Horizontal") && rotation == null)
        {
            number[currentRoulette] = (number[currentRoulette] + 1 * (int)Input.GetAxisRaw("Horizontal")) % 10;

            if (number[currentRoulette] == 0 && (int)Input.GetAxisRaw("Horizontal") > 0)
                number[currentRoulette]++;
            else if (number[currentRoulette] < 1)
                number[currentRoulette] += 9;

            rotation = StartCoroutine( Rotate(roulettes[currentRoulette], roulettes[currentRoulette].eulerAngles + new Vector3(0, 40 * (int)Input.GetAxisRaw("Horizontal"), 0)) );

            if(Check())
            {
                callbacks.Invoke();
                enabled = false;
            }

        } else if (Input.GetButtonDown("Vertical"))
        {
            currentRoulette = (currentRoulette + (int)Input.GetAxisRaw("Vertical") * -1) % roulettes.Count;
            if (currentRoulette < 0)
            {
                currentRoulette += roulettes.Count;
            }
        }

        if(Input.GetButtonDown("Cancel"))
        {
            enabled = false;
        }
    }

    IEnumerator Rotate(Transform obj, Vector3 targetRot)
    {
        //targetRot = new Vector3(targetRot.x % 360f, targetRot.y % 360f, targetRot.z % 360f);

        while(obj.eulerAngles != targetRot)
        {
            yield return new WaitForSeconds(0.01f);

            Vector3 lerp = Vector3.Lerp(obj.eulerAngles, targetRot, rotationSpeed * Time.deltaTime);

            if (VecSup(lerp, 360f) && VecSup(targetRot, 360f))
            {
                targetRot = VecMod(targetRot, 360f);
            }
            else if (VecInf(lerp, 0) && VecInf(targetRot, 0))
            {
                targetRot = RotMod(targetRot, 0f);
            }

            obj.eulerAngles = lerp;

            
            if((obj.eulerAngles - targetRot).magnitude < rotationMangetRadius)
            {
                obj.eulerAngles = targetRot;
            }
        }

        rotation = null;
    }

    private bool VecSup(Vector3 vec, float sup)
    {
        if(vec.x >= sup || vec.y >= sup || vec.z >= sup)
        {
            return true;
        }
        return false;
    }

    private bool VecInf(Vector3 vec, float inf)
    {
        if (vec.x < inf || vec.y < inf || vec.z < inf)
        {
            return true;
        }
        return false;
    }

    private Vector3 VecMod(Vector3 vec, float mod)
    {
        return new Vector3(vec.x % mod, vec.y % mod, vec.z % mod);
    }

    private Vector3 RotMod(Vector3 rot, float mod)
    {
        if(rot.x < 0)
            rot.x += 360;

        if (rot.y < 0)
            rot.y += 360;

        if (rot.z < 0)
            rot.z += 360;

        rot = VecMod(rot, 360);
        return rot;
    }

    private bool Check()
    {
        for (int i = 0; i < number.Length; i++)
            if (number[i] != int.Parse(password.ToString()[i].ToString()))
                return false;

        return true;
    }
}
