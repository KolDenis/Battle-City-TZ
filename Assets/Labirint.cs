using System;
using System.Collections.Generic;
using System.Linq;

public class Labirint
{
    public int size_lab = 30;
    List<int[]> way = new List<int[]>();
    int[,] mas;

    public int[,,] lab;

    public Labirint(int size)
    {
        size_lab = size;
    }

    public int[] where_can_go(int x, int y)
    {
        int[] ways = { 0, 0, 0, 0 };
        if (x != 0)
        {
            if (lab[y, x - 1, 0] == 0)
            {
                ways[0] = 1;
            }
        }

        if (y != 0)
        {
            if (lab[y - 1, x, 1] == 0)
            {
                ways[1] = 1;
            }
        }

        if (x != size_lab - 1)
        {
            if (lab[y, x, 0] == 0)
            {
                ways[2] = 1;
            }
        }

        if (y != size_lab - 1)
        {
            if (lab[y, x, 1] == 0)
            {
                ways[3] = 1;
            }
        }
        return ways;
    }

    int[] how_much_way(int x, int y)
    {
        int[] count = { 0, 0, 0, 0 };
        if (x != 0)
        {
            if (mas[x - 1, y] == 0)
            {
                count[0] = 1;
            }
        }

        if (y != 0)
        {
            if (mas[x, y - 1] == 0)
            {
                count[1] = 1;
            }
        }

        if (x != size_lab - 1)
        {
            if (mas[x + 1, y] == 0)
            {
                count[2] = 1;
            }
        }

        if (y != size_lab - 1)
        {
            if (mas[x, y + 1] == 0)
            {
                count[3] = 1;
            }
        }
        return count;
    }

    int go_backward(ref int x, ref int y)
    {
        int step = mas[x, y];
        while (!how_much_way(way[step - 1][0], way[step - 1][1]).Contains(1))
        {
            step--;
            if (step == 0)
            {
                break;
            }
        }
        if (step != 0)
        {
            x = way[step - 1][0];
            y = way[step - 1][1];
        }
        return step;
    }

    public void generate_new_labirint()
    {
        mas = new int[size_lab, size_lab];
        lab = new int[size_lab, size_lab, 2];

        for (int i = 0; i < size_lab; i++)
        {
            for (int j = 0; j < size_lab; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    lab[i, j, k] = 1;
                }
                mas[i, j] = 0;
            }
        }

        Random rand = new Random();

        bool go = true;
        int[] list_way = new int[4];
        int number_way;
        int changed_way = 0;
        int direct = 0;
        int count = 1;
        int x = size_lab - 1, y = size_lab - 1;
        mas[x, y] = 1;
        way.Clear();
        way.Add(new int[] { x, y });

        while (go == true)
        {
            if (size_lab * size_lab == count)
            {
                break;
            }
            list_way = how_much_way(x, y);
            if (!list_way.Contains(1) || (x == 0 && y == 0))
            {
                int step = go_backward(ref x, ref y);
                if (step == 0)
                {
                    break;
                }
                //list_way = how_much_way(x, y);
                //count--;
            }
            else
            {
                number_way = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (list_way[i] == 1)
                    {
                        number_way++;
                    }
                }
                direct = rand.Next(number_way);

                for (int i = 0; i < 4; i++)
                {
                    if (list_way[i] == 1)
                    {
                        direct--;
                    }
                    if (direct == -1)
                    {
                        changed_way = i;
                        i = 4;
                    }
                }

                if (changed_way == 0)
                {
                    x -= 1;
                    lab[y, x, 0] = 0;
                }
                else if (changed_way == 1)
                {
                    y -= 1;
                    lab[y, x, 1] = 0;
                }
                else if (changed_way == 2)
                {
                    lab[y, x, 0] = 0;
                    x += 1;
                }
                else if (changed_way == 3)
                {
                    lab[y, x, 1] = 0;
                    y += 1;
                }
                mas[x, y] = count;
                way.Add(new int[] { x, y });
                count++;
            }

        }

        Random r = new Random();

        for (int i = 0; i < size_lab; i++)
        {
            int a = r.Next(size_lab);
            int b = r.Next(size_lab);
            int c = r.Next(2);
            if (lab[a, b, c] == 1)
            {
                lab[a, b, c] = 0;
            }
            else
            {
                i--;
            }
        }
    }

    int go_backwardFindingWay(ref int x, ref int y)
    {
        int step = mas[x, y];
        int count = 0;

        while (true)
        {
            int[] massive = where_can_go(way[step - 1][0], way[step - 1][1]);
            count = 0;
            if (massive[0] == 1 && mas[way[step - 1][0] - 1, way[step - 1][1]] == 0)
            {
                count++;
            }
            if (massive[1] == 1 && mas[way[step - 1][0], way[step - 1][1] - 1] == 0)
            {
                count++;
            }
            if (massive[2] == 1 && mas[way[step - 1][0] + 1, way[step - 1][1]] == 0)
            {
                count++;
            }
            if (massive[3] == 1 && mas[way[step - 1][0], way[step - 1][1] + 1] == 0)
            {
                count++;
            }
            if (count > 0)
            {
                break;
            }
            else
            {
                step--;
                way.RemoveAt(way.Count - 1);
            }
        }
        x = way[step - 1][0];
        y = way[step - 1][1];
        return step;
    }

    public int[][] find_way(int[] from, int[] to)
    {
        size_lab = lab.GetLength(0);

        mas = new int[size_lab, size_lab];

        int[] list_way = new int[4];
        for (int i = 0; i < size_lab; i++)
        {
            for (int j = 0; j < size_lab; j++)
            {
                mas[i, j] = 0;
            }
        }

        int count = 2;

        way.Clear();
        way.Add(from);
        int x = from[0], y = from[1];

        while (true)
        {
            list_way = where_can_go(x, y);
            if (list_way[0] == 1 && mas[x - 1, y] == 0)
            {
                x--;
            }
            else if (list_way[1] == 1 && mas[x, y - 1] == 0)
            {
                y--;
            }
            else if (list_way[2] == 1 && mas[x + 1, y] == 0)
            {
                x++;
            }
            else if (list_way[3] == 1 && mas[x, y + 1] == 0)
            {
                y++;
            }
            else
            {
                count = go_backwardFindingWay(ref x, ref y);
                count++;
                continue;
            }

            mas[x, y] = count;
            way.Add(new int[] { x, y });

            if (x == to[0] && y == to[1])
            {
                break;
            }

            count++;
        }
        return way.ToArray();
    }
}
