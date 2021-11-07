using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour {

  public int size;

  private const int LEFT = 1, UP = 2, RIGHT = 4, DOWN = 8;
  private const int ALL = LEFT | UP | RIGHT | DOWN;

  public GameObject wallPrefab;
  public GameObject floor;

  private int[,] cells;
  private RandomQueue<int[]> queueWays;
  private GameObject[] walls;

  void Start() {
    if (size % 2 == 0) {
      size++;
    }
    generate();
    addWalls();
    cells = null;
    queueWays = null;
  }

  private void generate() {
    cells = new int[size, size];
    queueWays = new RandomQueue<int[]>(size);
    for (int i = 0; i < size; i++) {
      for (int j = 0; j < size; j++) {
        cells[i, j] = ALL;
      }
    }
    addToQueue(new int[] {size / 2, size / 2});
    while (queueWays.getSize() > 0) {
      int[] wall = queueWays.getNext();
      int[] cellToGo = getNeighbor(wall, wall[2]);
      if (!isVisited(cellToGo)) {
        cells[wall[0], wall[1]] -= wall[2];
        cells[cellToGo[0], cellToGo[1]] -= oppositeDirection(wall[2]);
        addToQueue(cellToGo);
      }
    }
    for (int i = 0; i < size; i++) {
      cells[0, i] -= LEFT;
      cells[i, 0] -= UP;
      cells[size - 1, i] -= RIGHT;
      cells[i, size - 1] -= DOWN;
    }
  }

  private void addWalls() {
    float cellSize = wallPrefab.transform.localScale.x;
    float elevation = wallPrefab.transform.localPosition.y;
    for (int i = 0; i < size; i++) {
      for (int j = 0; j < size; j++) {
        int x = j - size / 2;
        int z = i - size / 2;
        if ((cells[i, j] & LEFT) != 0) {
          Vector3 position = new Vector3(x * cellSize, elevation, (z - 0.5f) * cellSize);
          Quaternion rotation = Quaternion.Euler(0, 0, 0);
          Instantiate(wallPrefab, position, rotation);
        }
        if ((cells[i, j] & UP) != 0) {
          Vector3 position = new Vector3((x - 0.5f) * cellSize, elevation, z * cellSize);
          Quaternion rotation = Quaternion.Euler(0, 90, 0);
          Instantiate(wallPrefab, position, rotation);
        }
      }
    }
  }

  private int[] getNeighbor(int[] cell, int direction) {
    if (direction == LEFT) {
      return new int[] {cell[0] - 1, cell[1]};
    } else if (direction == UP) {
      return new int[] {cell[0], cell[1] - 1};
    } else if (direction == RIGHT) {
      return new int[] {cell[0] + 1, cell[1]};
    } else if (direction == DOWN) {
      return new int[] {cell[0], cell[1] + 1};
    }
    Debug.Log("getNeighbor invalid");
    return cell;
  }

  private int oppositeDirection(int direction) {
    if (direction == LEFT) {
      return RIGHT;
    } else if (direction == UP) {
      return DOWN;
    } else if (direction == RIGHT) {
      return LEFT;
    } else if (direction == DOWN) {
      return UP;
    }
    Debug.Log("oppositeDirection invalid");
    return 0;
  }

  private bool isVisited(int[] cell) {
    return cells[cell[0], cell[1]] != ALL;
  }

  private void addToQueue(int[] cell) {
    if (cell[0] > 0 && !isVisited(getNeighbor(cell, LEFT))) {
      queueWays.add(new int[] {cell[0], cell[1], LEFT});
    }
    if (cell[1] > 0 && !isVisited(getNeighbor(cell, UP))) {
      queueWays.add(new int[] {cell[0], cell[1], UP});
    }
    if (cell[0] < size - 1 && !isVisited(getNeighbor(cell, RIGHT))) {
      queueWays.add(new int[] {cell[0], cell[1], RIGHT});
    }
    if (cell[1] < size - 1 && !isVisited(getNeighbor(cell, DOWN))) {
      queueWays.add(new int[] {cell[0], cell[1], DOWN});
    }
  }

  private void printMaze() {
    Debug.Log("---");
    for (int i = 0; i < size; i++) {
      for (int j = 0; j < size; j++) {
        Debug.Log(i + "," + j + ":" + cells[i, j]);
      }
    }
  }

}
