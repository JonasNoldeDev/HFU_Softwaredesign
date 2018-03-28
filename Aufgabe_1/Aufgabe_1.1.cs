/*
    "dotnet run [Objekttyp] [Durchmesser]"

    Objekttyp (char):
        w: Würfel
        k: Kugel
        o: Oktaeder

    Durchmesser (double):
        Wert des Durchmessers
 */

public void main(args) 
{
    char type = char.Parse(args[0]);
    double diameter = double.Parse(args[1]);

    switch (type)
    {
        case 'w':
            Console.WriteLine("Würfel: A=" + surfaceCube(diameter) + " | V=" + volumeCube(diameter));
            break;
        case 'k':
            Console.WriteLine("Sphere: A=" + surfaceSphere(diameter) + " | V=" + volumeSphere(diameter));
            break;
        case 'o':
            Console.WriteLine("Oktaeder: A=" + surfaceOctahedron(diameter) + " | V=" + volumeOctahedron(diameter));
            break;
        default:
            Console.WriteLine("Objekttyp nicht valide");
            break;
    }
}

private double surfaceCube(double d)
{
    return 6*d^2;
}

private double volumeCube(double d)
{
    return d^3;
}

private double surfaceSphere(double d)
{
    return π * d^2;
}

private double volumeSphere(double d)
{
    return math.PI * d^3 / 6;
}

private double surfaceOctahedron(double d)
{
    return 2 * math.Sqrt(3) * d^2;
}

private double volumeOctahedron(double d)
{
    return math.Sqrt(2) * d^3 / 3;
}