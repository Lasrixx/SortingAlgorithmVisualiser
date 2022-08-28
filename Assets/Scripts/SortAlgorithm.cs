using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SortAlgorithm : MonoBehaviour
{
    private GraphWindow graphWindow;
    private List<int> values;
    private float delay;

    public void SetValues(List<int> values)
    {
        this.values = values;
    }

    public void SetDelay(float delay)
    {
        this.delay = delay;
    }

    public void SetGraphContainer()
    {
        graphWindow = GameObject.FindObjectOfType(typeof(GraphWindow)) as GraphWindow;
    }

    private void UpdateGraph()
    {
        graphWindow.ClearBarChart();
        graphWindow.ShowBarChart(this.values);
    }

    public IEnumerator InsertionSort()
    {
        // Do insertion sort
        int length = this.values.Count;
        // for loop means we can ignore first i elements once they
        // are inserted in the right place
        for (int i = 0; i < length - 1; i++)
        {
            // find minimum element in the remainder of the array
            int minIdx = i;
            for (int j = i + 1; j < length; j++)
            {
                if (this.values[j] < this.values[minIdx])
                {
                    minIdx = j;
                }
            }
            // swap ith smallest element with element in ith position
            int temp = this.values[i];
            this.values[i] = this.values[minIdx];
            this.values[minIdx] = temp;
            UpdateGraph();
            yield return new WaitForSeconds(this.delay);
        }
    }

    public IEnumerator BubbleSort()
    {
        // This time, track if there are any changes made between this pass
        // and the previous pass. If there isn't, we can end the algorithm early
        bool changed = false;
        int length = this.values.Count;
        // Require len-2 passes of the array to know it is sorted
        // len-2 comes from 2 things: initial value of i is 0,
        // and if every element in an array but 1 is known to be sorted,
        // then the unknown element must also be in the right position
        for (int i = 0; i < length - 2; i++)
        {
            //Debug.Log("sorting");
            // In this pass, compare pairs of adjacent values
            // Now can also reduce second loop by i as every pass will
            // put the last i elements in the correct position
            // therefore we do not need to consider them again 
            for (int j = 0; j < length - i - 1; j++)
            {
                if (this.values[j] > this.values[j + 1])
                {
                    //Debug.Log("comparing");
                    int temp = this.values[j + 1];
                    this.values[j + 1] = this.values[j];
                    this.values[j] = temp;
                    changed = true;
                    yield return new WaitForSeconds(this.delay);
                    UpdateGraph();
                }
            if (changed == false)
            {
                break;
            }
            }
        }
    }

    public IEnumerator SelectionSort()
    {
        // Once every element but last is in correct place
        // The last element must also be in the correct place
        // Therefore, no need to check it
        int length = this.values.Count;
        for (int i = 0; i < length - 1; i++)
        {
            // Need to find the smallest element remaining in the unsorted array
            int min_index = i;
            for (int j = i + 1; j < length; j++)
            {
                if (this.values[j] < this.values[min_index])
                {
                    min_index = j;
                }
            }
            // Then swap minimum element with element at ith position
            int temp = this.values[i];
            this.values[i] = this.values[min_index];
            this.values[min_index] = temp;
            UpdateGraph();
            yield return new WaitForSeconds(this.delay);
        }
    }
    
    public IEnumerator QuickSort()
    {
        // Create a stack which will store the starting and ending indices 
        // of each quicksort partition
        int left = 0;
        int right = this.values.Count - 1;
        int[] stack = new int[right - left + 1];
        int top_ptr = -1;

        // Push left and right onto the stack as initial values
        stack[++top_ptr] = left;
        stack[++top_ptr] = right;

        Debug.Log("sorting");

        // While stack is not empty, pop it
        while (top_ptr >= 0)
        {
            right = stack[top_ptr--];
            left = stack[top_ptr--];

            // Partition array
            // Set pivot element to rightmost element
            int i = left - 1;
            int pivot = this.values[right];

            // Traverse through all elements
            // compare each element with pivot
            for (int j = left; j < right; j++)
            {
                if (this.values[j] <= pivot)
                {
                    // If element is smaller than pivot,
                    // swap it with the greater element pointed to by i
                    i++;
                    int temp1 = this.values[i];
                    this.values[i] = this.values[j];
                    this.values[j] = temp1;
                    UpdateGraph();
                    yield return new WaitForSeconds(this.delay);
                }
            }
            // Swap pivot element with i
            i++;
            int temp2 = this.values[i];
            this.values[i] = this.values[right];
            this.values[right] = temp2;
            UpdateGraph();
            yield return new WaitForSeconds(this.delay);

            int p = i;

            //Any elements to the left of the pivot get pushed to left side of stack
            if (p - 1 > left)
            {
                stack[++top_ptr] = left;
                stack[++top_ptr] = p - 1;
            }
            // Any elements to the right are put on the right side of the stack
            if (p + 1 < right)
            {
                stack[++top_ptr] = p + 1;
                stack[++top_ptr] = right;
            }
        }
    }

    public IEnumerator MergeSort()
    {
        // Iterative merge sort uses a bottom up approach
        // We start with sorted lists of length 1 and build up
        int width = 1;
        int length = this.values.Count;

        while (width < length)
        {
            int left = 0;
            while (left < length)
            {
                int right = Math.Min(left + (width * 2 - 1), length - 1);
                int mid = Math.Min(left + width - 1, length - 1);

                // Unmerged sublists occur if this.values is not length of power of 2
                // In this case, merge the sublists last
                int leftLength = mid - left + 1;
                int[] leftHalf = new int[leftLength];
                int rightLength = right - mid;
                int[] rightHalf = new int[rightLength];
                // Populate leftHalf and rightHalf
                for (int a = 0; a < leftLength; a++)
                {
                    leftHalf[a] = this.values[left + a];
                }
                for (int a = 0; a < rightLength; a++)
                {
                    rightHalf[a] = this.values[mid + a + 1];
                }

                // Merge sublists
                int i = 0;
                int j = 0;
                int k = left;
                while (i < leftLength && j < rightLength)
                {
                    if (leftHalf[i] <= rightHalf[j])
                    {
                        // If left element is smaller than right,
                        // it needs to be placed in the merged array first
                        this.values[k++] = leftHalf[i++];
                        UpdateGraph();
                        yield return new WaitForSeconds(this.delay);
                    }
                    else
                    {
                        this.values[k++] = rightHalf[j++];
                        UpdateGraph();
                        yield return new WaitForSeconds(this.delay);
                    }
                } 
                // If right list is empty, append rest of left to array
                while (i < leftLength)
                {
                    this.values[k++] = leftHalf[i++];
                    UpdateGraph();
                    yield return new WaitForSeconds(this.delay);
                }
                // If left list is empty, append rest of right to array
                while (j < rightLength)
                {
                    this.values[k++] = rightHalf[j++];
                    UpdateGraph();
                    yield return new WaitForSeconds(this.delay);
                }
                // Move left pointer along for next iteration
                left += width * 2;
            }
            // Increase sub array size by power of 2
            // Each merge squares the size of the sub arrays
            width *= 2;
        }
    }

}
