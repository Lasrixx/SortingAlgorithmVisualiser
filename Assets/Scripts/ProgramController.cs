using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramController : MonoBehaviour
{
    private GraphWindow graphWindow;
    private int length;
    private float sortingDelay;
    private List<int> values;

    private void Awake()
    {
        graphWindow = GameObject.FindObjectOfType(typeof(GraphWindow)) as GraphWindow;
    }

    private void Start()
    {
        this.length = 100;
        this.sortingDelay = 0.10f;
        RandomiseValues();
    }

    public int GetLength()
    {
        return this.length;
    }

    public void SetLength(int length)
    {
        this.length = length;
    }

    public float GetDelay()
    {
        return this.sortingDelay;
    }

    public void SetDelay(float delay)
    {
        this.sortingDelay = delay;
    }

    public void RandomiseValues()
    {
        List<int> values = new List<int>();
        for (int i = 0; i < length; i++)
        {
            values.Add((int)Random.Range(0, 100));
        }

        this.values = values;
        graphWindow.ClearBarChart();
        graphWindow.ShowBarChart(this.values);
    }

    public void CallAlgorithm(string algorithm)
    {
        algorithm = algorithm.Replace(" ", string.Empty);
        Debug.Log(algorithm);
        //SortAlgorithm sortAlgorithm = new SortAlgorithm(this.values, this.sortingDelay);
        SortAlgorithm sortAlgorithm = gameObject.AddComponent<SortAlgorithm>();
        sortAlgorithm.SetValues(this.values);
        sortAlgorithm.SetDelay(this.sortingDelay);
        sortAlgorithm.SetGraphContainer();
        switch (algorithm)
        {
            case "InsertionSort":
                StartCoroutine(sortAlgorithm.InsertionSort());
                break;
            case "BubbleSort":
                StartCoroutine(sortAlgorithm.BubbleSort());
                break;
            case "SelectionSort":
                StartCoroutine(sortAlgorithm.SelectionSort());
                break;
            case "MergeSort":
                StartCoroutine(sortAlgorithm.MergeSort());
                break;
            case "QuickSort":
                StartCoroutine(sortAlgorithm.QuickSort());
                break;
        }
        
    }
}
