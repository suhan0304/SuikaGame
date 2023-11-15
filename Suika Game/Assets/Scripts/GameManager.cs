using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Dongle lastDongle;
    public GameObject donglePrefab; //���� ������
    public Transform dongleGroup;   //������ ������ ��ġ

    private void Start()
    {
        NextDongle();
    }

    Dongle GetDongle()
    {
        //���� ������ �����ؼ� ������, �� �� �θ�� ���� �׷����� ����
        GameObject instant = Instantiate(donglePrefab, dongleGroup);
        Dongle instantDongle = instant.GetComponent<Dongle>();
        return instantDongle;
    }

    void NextDongle()
    {
        //������ ������ ������ new Dongle�� ����
        Dongle newDongle = GetDongle();
        lastDongle = newDongle;

        StartCoroutine(WaitNext()); //����� NextDongle�� �����ϴ� �ڷ�ƾ ����
    }

    IEnumerator WaitNext()
    {
        while(lastDongle != null)
        {
            yield return null; //�� �������� ����Ѵ�.
        }

        yield return new WaitForSeconds(2.5f); //2.5�ʸ� ����Ѵ�

        NextDongle();
    }

    public void TouchDown()
    {
        if (lastDongle == null) //lastDongle�� ������ �������� ����
            return;

        lastDongle.Drag();
    }

    public void TouchUp()
    {
        if (lastDongle == null) //lastDongle�� ������ �������� ����
            return;
        lastDongle.Drop();
        lastDongle = null; //����ϸ鼭 �����뵵�� �����ص� ������ null�� ����.
    }
}