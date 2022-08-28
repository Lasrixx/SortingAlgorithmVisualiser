using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputController : MonoBehaviour
{
    private GraphWindow graphWindow;
    private ProgramController programController;
    [SerializeField]
    private GameObject lengthText;
    [SerializeField]
    private GameObject algorithmText;
    [SerializeField]
    private GameObject speedText;
    [SerializeField] private int maxLength;
    [SerializeField] private int minLength;
    List<string> algorithms = new List<string>() { "Insertion Sort", "Bubble Sort", "Selection Sort", "Merge Sort", "Quick Sort" };
    private int algoIndex = 0;

    private void Awake()
    {
        graphWindow = GameObject.FindObjectOfType(typeof(GraphWindow)) as GraphWindow;
        programController = GameObject.FindObjectOfType(typeof(ProgramController)) as ProgramController;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Generate new values
            programController.RandomiseValues();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Decrease values array length
            int length = programController.GetLength();
            length -= 5;
            if (length < minLength)
            {
                length = minLength;
            }
            lengthText.GetComponent<TextMeshProUGUI>().text = length.ToString();
            programController.SetLength(length);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Increase values array length
            int length = programController.GetLength();
            length += 5;
            if (length > maxLength)
            {
                length = maxLength;
            }
            lengthText.GetComponent<TextMeshProUGUI>().text = length.ToString();
            programController.SetLength(length);
        }
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            // Decrease sorting speed
            float delay = programController.GetDelay();
            delay += 0.05f;
            if (delay > 0.15f)
            {
                delay = 0.15f;
            }
            string speed = "Mid";
            if (delay == 0.15f){
                speed = "Slow";
            }
            speedText.GetComponent<TextMeshProUGUI>().text = speed.ToString();
            programController.SetDelay(delay);
        }
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            // Increase sorting speed
            float delay = programController.GetDelay();
            delay -= 0.05f;
            if (delay < 0.05f)
            {
                delay = 0.05f;
            }
            string speed = "Mid";
            if (delay <= 0.051f)
            {
                speed = "Fast";
            }
            speedText.GetComponent<TextMeshProUGUI>().text = speed.ToString();
            programController.SetDelay(delay);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Change sorting algorithm
            algoIndex += 1;
            if (algoIndex >= algorithms.Count)
            {
                algoIndex = 0;
            }
            algorithmText.GetComponent<TextMeshProUGUI>().text = algorithms[algoIndex];
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Change sorting algorithm
            algoIndex -= 1;
            if (algoIndex < 0)
            {
                algoIndex = algorithms.Count - 1;
            }
            algorithmText.GetComponent<TextMeshProUGUI>().text = algorithms[algoIndex];
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Run sorting algorithm
            programController.CallAlgorithm(algorithmText.GetComponent<TextMeshProUGUI>().text);
        }
    }
}
