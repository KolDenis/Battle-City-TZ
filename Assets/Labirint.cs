using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        /*for (int i = 0; i < size_lab; i++)
        {
            for (int j = 0; j < size_lab; j++)
            {
                mas[i, j] = 0;
            }
        }*/
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
    }
}
