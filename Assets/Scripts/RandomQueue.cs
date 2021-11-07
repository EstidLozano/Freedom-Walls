using System;
public class RandomQueue<T> {

  private T[] queue;
  private int size;
  private Random rand;
  
  public RandomQueue(int capacity) {
    this.size = 0;
    queue = new T[capacity];
    rand = new Random();
  }

  public T getNext() {
    int randInt = rand.Next(size);
    T temp = queue[randInt];
    queue[randInt] = queue[size - 1];
    size--;
    return temp;
  }

  public void add(T item) {
    if (size == queue.Length) {
      setQueueLength(size * 2);
    }
    queue[size] = item;
    size++;
  }

  public void add(T[] items) {
    foreach (T item in items) {
      add(item);
    }
  }

  public int getSize() {
    return size;
  }

  public void setQueueLength(int size) {
    T[] temp = new T[size];
    Array.Copy(queue, 0, temp, 0, Math.Min(queue.Length, size));
    queue = temp;
  }

}