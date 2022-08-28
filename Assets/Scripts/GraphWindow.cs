using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphWindow : MonoBehaviour
{
    private RectTransform graphContainer;

    private void Awake()
    {
        graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();
    }

    private void CreateBar(Vector2 pos, float barWidth)
    {
        GameObject gameObject = new GameObject("Bar", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.tag = "Bar";

        // Set position
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(pos.x, pos.y/2);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);

        // Set shape
        rectTransform.sizeDelta = new Vector2(barWidth, pos.y);

        // Set bar colour
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
    }

    private void DestroyBar()
    {

    }

    public void ShowBarChart(List<int> values)
    {
        float yMaximum = 100f;
        float graphHeight = graphContainer.sizeDelta.y;
        float graphWidth = graphContainer.sizeDelta.x;
        float xSize = graphWidth / values.Count;
        for (int i = 0; i < values.Count; i++)
        {
            float xPosition = 0.5f * xSize + i * xSize;
            float yPosition = (values[i] / yMaximum) * graphHeight;
            CreateBar(new Vector2(xPosition, yPosition), xSize);
        }
    }

    public void ClearBarChart()
    {
        GameObject[] bars = GameObject.FindGameObjectsWithTag("Bar");
        for (int i = 0; i < bars.Length; i++)
        {
            Destroy(bars[i]);
        }
    }

}
