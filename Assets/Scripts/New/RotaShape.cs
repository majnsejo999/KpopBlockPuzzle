using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaShape : MonoBehaviour
{
    public bool CanRota;
	public Vector3 Rotate;
	public int z;
	
    private void OnEnable()
    {
        CanRota = true;
        Rotate = new Vector3(0, 0, 0);
		z = 0;
    }
}
