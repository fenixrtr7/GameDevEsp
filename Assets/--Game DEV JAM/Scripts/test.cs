using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class test : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Test_1();
        }
    }

    public void Test_1()
    {
        Sequence newSequence = DOTween.Sequence();
        newSequence.SetDelay(0.5f);
        newSequence.AppendCallback(() =>
        {
            Debug.Log("1");
        });
        newSequence.AppendInterval(3f);
        newSequence.Append(this.transform.DOScale(0.5f, 2f));
        newSequence.Join(this.transform.DORotate(new Vector3(0, 0, 180), 2f));
        newSequence.AppendInterval(3f);
        newSequence.AppendCallback(() =>
        {
            Debug.Log("2");
            DOTween.Sequence()
            .AppendInterval(2f)
            .AppendCallback(() =>
            {
                Debug.Log("4");
            })
            .AppendInterval(2f)
            .AppendCallback(() =>
            {
                Debug.Log("5");
            });
        });
        newSequence.AppendInterval(1f);
        newSequence.AppendCallback(() =>
        {
            Debug.Log("3");
        });
        
    }
}
