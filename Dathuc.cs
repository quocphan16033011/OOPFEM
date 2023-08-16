using System;

internal class Dathuc_Info
{
    private double[] coef;
    public Dathuc_Info(params double[] coef)
    {
        this.coef = coef;
    }
    public void InDaThuc()
    {
        string str = "";
        for (let i = 0; i < coef.Length; i++)
        {
            str += coef[i] + "x^" + i + "+";
            if (i = coef.Length - 1)
            {
                str += coef[i] + "x^" + i;
            }
        }
        Console.WriteLine($"Da thuc bac {coef.Length} : {str}");
    }
}
