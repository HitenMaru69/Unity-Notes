using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public class TestStoreData // class that store the data
{
    public int a;
    public int b;
    public int c;

   public List<int> nu ;
}


public class TestScript : MonoBehaviour // Save manager Class
{
    public List<Test> allTests; // List Of Scriptable Object

    private void Start()
    {
        LoadData();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){

            allTests[0].a = 10;
            allTests[0].b = 20;
            allTests[0].c = 30;

            allTests[0].nu.Clear();
        }


        if (Input.GetKeyDown(KeyCode.P)) {


            allTests[3].a = 30;
            allTests[3].b = 30;
            allTests[3].c = 30;

            allTests[3].nu.Clear();
        }

        saveData();
    }

    public void saveData() // To Save the data
    {
        for (int i = 0; i < allTests.Count; i++)
        {
            Test t = allTests[i];
            TestStoreData data = new TestStoreData
            {
                a = t.a,
                b = t.b,
                c = t.c,
                nu = new List<int>(t.nu)
            };

            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString("test_" + i, json);
        }

        PlayerPrefs.Save();
    }


    public void LoadData() // To load the data
    {
        for (int i = 0; i < allTests.Count; i++)
        {
            string key = "test_" + i;
            if (PlayerPrefs.HasKey(key))
            {
                string json = PlayerPrefs.GetString(key);
                TestStoreData data = JsonUtility.FromJson<TestStoreData>(json);

                Test t = allTests[i];
                t.a = data.a;
                t.b = data.b;
                t.c = data.c;
                t.nu = new List<int>(data.nu);
            }
        }
    }

}
