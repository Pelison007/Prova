namespace Api.Modelo;

public class Funcionarios
{
    public int Id {get; set;}
    public string Nome {get; set;} = string.Empty;
    public string cpf {get; set;} = string.Empty;
    public int mes {get; set;}
    public int ano {get; set;}
    public int horasTrabalhadas {get; set;}
    public int valorHora {get; set;}

    public float SalarioBruto {get; set;}
    public float ImpostoRenda {get; set;}
    public float Inss {get; set;}
    public float Fgts {get; set;}
    public float SalarioLiquido {get; set;}

    public void CalcularValores()
    {
        SalarioBruto = horasTrabalhadas * valorHora;

        if (SalarioBruto <= 1903.98f)
        {
            ImpostoRenda = (SalarioBruto * 0f) - 0f;
        }
        else if (SalarioBruto <= 2826.65f)
        {
            ImpostoRenda = (SalarioBruto * 0.075f) - 142.80f;
        }
        else if (SalarioBruto <= 3751.05f)
        {
            ImpostoRenda = (SalarioBruto * 0.15f) - 354.80f;
        }
        else if (SalarioBruto <= 4664.68f)
        {
            ImpostoRenda = (SalarioBruto * 0.225f) - 636.13f; 
        }
        else
        {
            ImpostoRenda = (SalarioBruto * 0.275f) - 869.36f;
        }

        // - Inss
        if (SalarioBruto <= 1693.72f)
        {
            Inss = SalarioBruto * 0.08f;
        }
        else if (SalarioBruto <= 2822.90f)
        {
            Inss = SalarioBruto * 0.09f;
        }
        else if (SalarioBruto <= 5645.80f)
        {
            Inss = SalarioBruto * 0.11f;
        }
        else
        {
            Inss = 621.03f;
        }

        // -- FGTS
        Fgts = SalarioBruto * 0.08f;

        // -- Salário Líquido
        SalarioLiquido = SalarioBruto - ImpostoRenda - Inss;

        //-- arredondamento
        SalarioBruto = MathF.Round(SalarioBruto, 2);
        ImpostoRenda = MathF.Round(ImpostoRenda, 2);
        Inss = MathF.Round(Inss, 2);
        Fgts = MathF.Round(Fgts, 2);
        SalarioLiquido = MathF.Round(SalarioLiquido, 2);
    }
}

