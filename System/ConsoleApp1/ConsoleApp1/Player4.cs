using System;


public class Player4
{
    static bool s;
    static bool x;
    static bool z;
    static bool y;

    static string lastDir;
    static string Dir;

    //边框
    static int ixMAX;
    static int iyMIN;

    //变化界限
    static int bxMIN;
    static int bxMAX;
    static int byMIN;
    static int byMAX;

    static int ix;
    static int iy;
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


        bool s = true;
        bool x = true;
        bool z = true;
        bool y = true;


        int xF = 0;
        int HH = H;
        int WW = W;
        int max = W;

        int Xmin = 0;
        int Xmax = W - 1;
        int Ymin = 0;
        int Ymax = H - 1;
        int Xn = max - 1;
        int Xstand = Xmax;
        //RX 前一步的移动放心  -1：向左  1：向右 
        int RL = 1;


        int yYUAN = 0;
        bool X = false;
        //Console.WriteLine("0 "+ ( Xmax -1));
        //string bombDir = Console.ReadLine();
        string bombDir = "";
        int yF = 0;
        int i = 0;
        string wow = "";
        bombDir = Console.ReadLine();
        //Xmax 变
        while (true)
        {
            wow = "{0} {1}";

            if (Xmax == 0)
            {
                X = true;
                Xmin = 0;
                Xmax = Ymax;
                xF = 0;
            }
            //输出最小
            if (!X)
            {
                //if (i==1)
                //{
                //    bombDir = Console.ReadLine();
                //    i++;
                //}

                Console.WriteLine(string.Format(wow, Xmin, yYUAN));
                //输出最大
                bombDir = Console.ReadLine();
                Console.WriteLine(string.Format(wow, Xmax, yYUAN));
                bombDir = Console.ReadLine();
            }
            //if (bombDir.Equals("COLDER"))
            //{
            //    Console.WriteLine("COLDER");
            //}
            //if (bombDir.Equals("UNKNOWN"))
            //{
            //    Console.WriteLine("UNKNOWN");
            //}
            //if (bombDir.Equals("WARMER"))
            //{
            //    Console.WriteLine("WARMER");
            //}
            // do 1 
            string res = bombDir;


            if (res.Equals("COLDER"))
            {
                Xmin = Xmin;
                Xmax = Xmin + (Xmax - Xmin) / 2;
            }
            if (res.Equals("WARMER"))
            {
                Xmin = Xmin + (Xmax - Xmin) / 2;
                Xmax = Xmax;
            }

            if (!X && res == "SAME")
            {
                xF = (Xmax + Xmin) / 2;
                X = true;
                i = 0;
                // Console.WriteLine("1324655");
            }

            //else
            //{
            //    Console.WriteLine(string.Format(wow, xF, Xmin));
            //    Console.WriteLine(string.Format(wow, xF, Xmax));
            //}

            if (Xmax - Xmin == 1)
            {
                if (res.Equals("WARMER"))
                {
                    xF = Xmax;
                }
                else
                {
                    xF = Xmin;
                }
                X = true;
                Xmin = 0;
                Xmax = Ymax;
            }

            if (X)
            {
                Xmin = 0;
                Xmax = Ymax;

                if (Ymax == 0)
                {
                    Console.WriteLine(string.Format(wow, xF, 0));
                }
                wow = "{0} {1}";
                //Console.WriteLine(string.Format(wow, xF, 0));
                while (true)
                {

                    Console.WriteLine(string.Format(wow, xF, Xmin));
                    bombDir = Console.ReadLine();
                    Console.WriteLine(string.Format(wow, xF, Xmax));
                    bombDir = Console.ReadLine();

                    // do 1 
                    res = bombDir;
                    if (res.Equals("COLDER"))
                    {
                        Xmin = Xmin;
                        Xmax = Xmin + (Xmax - Xmin) / 2;
                    }
                    if (res.Equals("WARMER"))
                    {
                        Xmin = Xmin + (Xmax - Xmin) / 2;
                        Xmax = Xmax;
                    }
                    if (res == "SAME")
                    {
                        yF = (Xmax + Xmin) / 2;
                        Console.WriteLine(string.Format(wow, xF, yF));
                    }

                    if (Xmax - Xmin == 1)
                    {
                        if (res.Equals("WARMER"))
                        {
                            yF = Xmax;
                        }
                        else
                        {
                            yF = Xmin;
                        }
                        Console.WriteLine(string.Format(wow, xF, yF));
                    }
                    //else
                    //{
                    //    Console.WriteLine(string.Format(wow, xF, Xmin));
                    //    Console.WriteLine(string.Format(wow, xF, Xmax));
                    //}
                }
            }
        }
        //bombDir = Console.ReadLine();
        //Console.WriteLine( xF +" " + Ymax);

    }

    public static string GetDir()
    {
        if (s)
        {
            return "s";
        }
        if (x)
        {
            return "x";
        }

        if (z)
        {
            return "z";
        }
        if (y)
        {
            return "y";
        }
        s = true;
        x = true;
        z = true;
        y = true;
        return "s";
    }


    public static int GetValuse(string Dir)
    {
        int res = 0;
        if (Dir.Equals("s"))
        {
            res = iy - (iy - byMIN) / 2;
        }
        if (Dir.Equals("x"))
        {
            res = iy + (byMAX - iy) / 2;
        }
        if (Dir.Equals("z"))
        {
            res = ix - (ix - bxMIN) / 2;
        }
        if (Dir.Equals("y"))
        {
            res = ix + (bxMAX - ix) / 2;
        }
        return res;

    }

}