using System;
using UnityEngine;
public class MazeGenerator : MonoBehaviour {

  public static int size = 3;

  private const int LEFT = 1, UP = 2, RIGHT = 4, DOWN = 8, FRONT = 16, BACK = 32;
  private const int ALL = LEFT | UP | RIGHT | DOWN | FRONT | BACK;

  public GameObject wallPrefab;
  public GameObject rayPrefab;
  public GameObject lightPrefab;
  public GameObject goal;

  private int[,,] cells;
  private RandomQueue<int[]> queueWays;

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
    cells = new int[size, size, size];
    queueWays = new RandomQueue<int[]>(size);
    for (int i = 0; i < size; i++) {
      for (int j = 0; j < size; j++) {
        for (int k = 0; k < size; k++) {
          cells[i, j, k] = ALL;
        }
      }
    }
    addToQueue(new int[] {size / 2, size / 2, size / 2});
    while (queueWays.getSize() > 0) {
      int[] wall = queueWays.getNext();
      int[] cellToGo = getNeighbor(wall, wall[3]);
      if (!isVisited(cellToGo)) {
        cells[wall[0], wall[1], wall[2]] -= wall[3];
        cells[cellToGo[0], cellToGo[1], cellToGo[2]] -= oppositeDirection(wall[3]);
        addToQueue(cellToGo);
      }
    }
    for (int i = 0; i < size; i++) {
      for (int j = 0; j < size; j++) {
        cells[0, i, j] -= LEFT;
        cells[i, 0, j] -= DOWN;
        cells[i, j, 0] -= BACK;
        cells[size - 1, i, j] -= RIGHT;
        cells[i, size - 1, j] -= UP;
        cells[i, j, size - 1] -= FRONT;
      }
    }
  }

  private void addWalls() {
    float cellSize = wallPrefab.transform.localScale.x;
    Quaternion[] rots = {
      Quaternion.Euler(0, 90, 0),
      Quaternion.Euler(90, 0, 0),
      Quaternion.identity
    };
    int mid = size / 2;
    float rayProb = 0.1f, lightProb = 0.2f;
    System.Random rnd = new System.Random();
    // Inner walls, rays and lights
    for (int i = 0; i < size; i++) {
      for (int j = 0; j < size; j++) {
        for (int k = 0; k < size; k++) {
          int x = i - mid, y = j - mid, z = k - mid;
          if ((cells[i, j, k] & LEFT) != 0) {
            Vector3 pos = new Vector3(x - 0.5f, y, z) * cellSize;
            Instantiate(wallPrefab, pos, rots[0]);
            if (rnd.NextDouble() < lightProb) {
              Instantiate(lightPrefab, pos, rots[0]);
            }
          } else if (rnd.NextDouble() < rayProb) {
            Vector3 pos = new Vector3(x - 0.5f, y, z) * cellSize;
            Instantiate(rayPrefab, pos, rots[0]);
          }
          if ((cells[i, j, k] & DOWN) != 0) {
            Vector3 pos = new Vector3(x, y - 0.5f, z) * cellSize;
            Instantiate(wallPrefab, pos, rots[1]);
            if (rnd.NextDouble() < lightProb) {
              Instantiate(lightPrefab, pos, rots[1]);
            }
          } else if (rnd.NextDouble() < rayProb) {
            Vector3 pos = new Vector3(x, y - 0.5f, z) * cellSize;
            Instantiate(rayPrefab, pos, rots[1]);
          }
          if ((cells[i, j, k] & BACK) != 0) {
            Vector3 pos = new Vector3(x, y, z - 0.5f) * cellSize;
            Instantiate(wallPrefab, pos, rots[2]);
            if (rnd.NextDouble() < lightProb) {
              Instantiate(lightPrefab, pos, rots[2]);
            }
          } else if (rnd.NextDouble() < rayProb) {
            Vector3 pos = new Vector3(x, y, z - 0.5f) * cellSize;
            Instantiate(rayPrefab, pos, rots[2]);
          }
        }
      }
    }
    // Outer walls
    Vector3 scale = wallPrefab.transform.localScale;
    scale.x = size * cellSize + scale.z;
    scale.y = scale.x;
    float offset = size / 2f * cellSize;
    Vector3[] positions = {
      new Vector3(-offset, 0, 0),
      new Vector3(0, -offset, 0),
      new Vector3(0, 0, -offset),
      new Vector3(offset, 0, 0),
      new Vector3(0, offset, 0),
      new Vector3(0, 0, offset)
    };
    for(int i = 0; i < positions.Length; i++) {
      GameObject wall = Instantiate(wallPrefab, positions[i], rots[i % 3]);
      wall.transform.localScale = scale;
    }
    // Goal
    Vector3 posGoal = new Vector3(rnd.Next(0, 1), rnd.Next(0, 1), rnd.Next(0, 1)) * 2 - Vector3.one;
    goal.transform.position = posGoal * (size - 1) / 2f * cellSize;
  }

  private int[] getNeighbor(int[] cell, int direction) {
    int[] neighbor = (int[]) cell.Clone();
    if (direction == LEFT) {
      neighbor[0]--;
    } else if (direction == DOWN) {
      neighbor[1]--;
    } else if (direction == BACK) {
      neighbor[2]--;
    } else if (direction == RIGHT) {
      neighbor[0]++;
    } else if (direction == UP) {
      neighbor[1]++;
    } else if (direction == FRONT) {
      neighbor[2]++;
    } else {
      Debug.Log("Neighbor invalid");
    }
    return neighbor;
  }

  private int oppositeDirection(int direction) {
    if (direction == LEFT) {
      return RIGHT;
    } else if (direction == DOWN) {
      return UP;
    } else if (direction == BACK) {
      return FRONT;
    } else if (direction == RIGHT) {
      return LEFT;
    } else if (direction == UP) {
      return DOWN;
    } else if (direction == FRONT) {
      return BACK;
    } else {
      Debug.Log("Opposite direction invalid");
      return 0;
    }
  }

  private bool isVisited(int[] cell) {
    return cells[cell[0], cell[1], cell[2]] != ALL;
  }

  private void addToQueue(int[] cell) {
    int[] sample = new int[] {cell[0], cell[1], cell[2], 0};
    if (cell[0] > 0 && !isVisited(getNeighbor(cell, LEFT))) {
      sample[3] = LEFT;
      queueWays.add((int[]) sample.Clone());
    }
    if (cell[1] > 0 && !isVisited(getNeighbor(cell, DOWN))) {
      sample[3] = DOWN;
      queueWays.add((int[]) sample.Clone());
    }
    if (cell[2] > 0 && !isVisited(getNeighbor(cell, BACK))) {
      sample[3] = BACK;
      queueWays.add((int[]) sample.Clone());
    }
    if (cell[0] < size - 1 && !isVisited(getNeighbor(cell, RIGHT))) {
      sample[3] = RIGHT;
      queueWays.add((int[]) sample.Clone());
    }
    if (cell[1] < size - 1 && !isVisited(getNeighbor(cell, UP))) {
      sample[3] = UP;
      queueWays.add((int[]) sample.Clone());
    }
    if (cell[2] < size - 1 && !isVisited(getNeighbor(cell, FRONT))) {
      sample[3] = FRONT;
      queueWays.add((int[]) sample.Clone());
    }
  }
}
