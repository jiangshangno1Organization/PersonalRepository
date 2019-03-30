using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
public class Player1
{

    public static int HH;
    public static int WW;
    static int zx ;
    static int yx ;
    static int sy ;
    static int xy ;
    static int i ;
    static bool XF ;
    static bool YF;

    static void Main(string[] args)
    {

        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        int W = int.Parse(inputs[0]); // width of the building.
        int H = int.Parse(inputs[1]); // height of the building.
        int N = int.Parse(Console.ReadLine()); // maximum number of turns before game over.
        inputs = Console.ReadLine().Split(' ');
        int X0 = int.Parse(inputs[0]);
        int Y0 = int.Parse(inputs[1]);

        XF = false;
        YF = false;

        HH = H;
        WW = W;
        int rX = X0;
        int rY = Y0;

         zx = 0;
         yx = W - 1;
         sy = 0;
         xy = H - 1;


        // game loop
        while (true)
        {
            string X = "X";
            string Y = "Y";
            if (H - 1 == 0)
            {
                YF = true;
            }
            if (W - 1 == 0)
            {
                XF= true;
            }
            string bombDir = Console.ReadLine();

            // the direction of the bombs from batman's current location (U, UR, R, DR, D, DL, L or UL)
            // the location of the next window Batman should jump to.
            switch (bombDir)
            {
                //在上方
                case "U":
                    rX = GetValues(-1, rX, X);
                    rY = GetValues(0, rY, Y);
                    break;
                //在
                case "UR":
                    rX = GetValues(1, rX, X);
                    rY = GetValues(0, rY, Y);
                    break;
                case "R":
                    rX = GetValues(1, rX, X);
                    rY = GetValues(-1, rY, Y);
                    break;
                case "DR":
                    rX = GetValues(1, rX, X);
                    rY = GetValues(1, rY, Y);
                    break;
                case "D":
                    rX = GetValues(-1, rX, X);
                    rY = GetValues(1, rY, Y);
                    break;
                case "DL":
                    rX = GetValues(0, rX, X);
                    rY = GetValues(1, rY, Y);
                    break;
                case "L":
                    rX = GetValues(0, rX, X);
                    rY = GetValues(-1, rY, Y);
                    break;
                case "UL":
                    rX = GetValues(0, rX, X);
                    rY = GetValues(0, rY, Y);
                    break;
                default:
                    break;
            }

            string res = "{0} {1}";
            Console.WriteLine(string.Format(res, rX, rY));
        }

    }

    /// <summary>
    ///  0 - 1 - -1  0小 ， 1大
    /// </summary>
    /// <param name="T"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    private static int GetValues(int T, int values, string type)
    {
      
        int max = 0;
        int min = 0;
        if (type.Equals("X"))
        {
            {
                min = zx;
                max = yx;
            }
            if (XF)
            {
                return 0;
            }

        }
        if (type.Equals("Y"))
        {

            if (YF)
            {
                return 0;
            }

            {
                min = sy;
                max = xy;
            }
        }


        if (T == -1)
        {
            return values;
        }

        // 0 - 9
        int res = 0;
        // 小
        if (T == 0)
        {
            if (values == 1)
            {
                return 0;
            }
            res = (values - min) / 2 + min;
            if (type.Equals("X"))
            {
                //res += 1;
                yx = res;
            }
            else
            {
                //res += 1;
                xy = res;
                //sy = res;
            }

        }

        if (T == 1)
        {
            //values += 1;
            int r = (max - values) / 2;
            if (r == 0)
            {
                r = 1;
            }
            res = values + r ;
            if (res > HH-1)
            {
                res = HH - 1;
            }
            if (type.Equals("X"))
            {
                zx = res;
            }
            else
            {
                xy = res;
                //xy = res;
            }
        }
        return res;
    }

}