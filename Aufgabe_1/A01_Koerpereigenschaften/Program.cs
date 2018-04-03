using System;
using System.Linq;

namespace A01_Koerpereigenschaften
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 2)
            {
                string type = args[0];
                if(type == "w" || type == "k" || type == "o")
                {
                    try
                    {
                        double diameter = double.Parse(args[1]);

                        switch(type)
                        {
                            case "w":
                                Console.WriteLine(GetCubeInfo(diameter));
                                break;
                            case "k":
                                Console.WriteLine(GetSphereInfo(diameter));
                                break;
                            case "o":
                                Console.WriteLine(GetOctahedronInfo(diameter));
                                break;
                        }
                    }
                    catch(FormatException e)
                    {
                        Console.WriteLine("Keine gültige Kantenlänge/kein gültiger Durchmesser.");
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Kein gültiger Körper.");
                }
            }
            else
            {
                Console.WriteLine("Die Zahl der Argumente ist ungültig.");
            }
        }

        static double GetCubeSurface(double diameter)
        {
            return 6 * Math.Pow(diameter, 2);
        }
        static double GetCubeVolume(double diameter)
        {
            return Math.Pow(diameter, 3);
        }
        static string GetCubeInfo(double diameter)
        {
            return "Würfel: A=" + Math.Round(GetCubeSurface(diameter), 2) + " | V=" + Math.Round(GetCubeVolume(diameter), 2);
        }

        static double GetSphereSurface(double diameter)
        {
            return Math.PI * Math.Pow(diameter, 2);
        }
        static double GetSphereVolume(double diameter)
        {
            return Math.PI * Math.Pow(diameter, 3) / 6;
        }
        static string GetSphereInfo(double diameter)
        {
            return "Kugel: A=" + Math.Round(GetSphereSurface(diameter), 2) + " | V=" + Math.Round(GetSphereVolume(diameter), 2);
        }

        static double GetOctahedronSurface(double diameter)
        {
            return 2 * Math.Sqrt(3) * Math.Pow(diameter, 2);
        }
        static double GetOctahedronVolume(double diameter)
        {
            return Math.Sqrt(2) * Math.Pow(diameter, 3) / 3;
        }
        static string GetOctahedronInfo(double diameter)
        {
            return "Oktaeder: A=" + Math.Round(GetOctahedronSurface(diameter), 2) + " | V=" + Math.Round(GetOctahedronVolume(diameter), 2);
        }
    }
}
