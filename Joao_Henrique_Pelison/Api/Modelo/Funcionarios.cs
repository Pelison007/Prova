namespace Api.Modelo;

public class Funcionarios
{
    public int Id {get; set;}
    public string Nome {get; set;} = string.Empty;
    public double cpf {get; set;}
    public int mes {get; set;}
    public int ano {get; set;}
    public int horasTrabalhadadas {get; set;}
    public int ValorHora {get; set;}

    public float SalarioBruto {get; set;} = horasTrabalhadadas * ValorHora

    public float ImpostoRenda {get; set;} = {
        if (SalarioBruto <= 1903.98){
            ImpostoRenda = 0;
        } 
        if (SalarioBruto >= 1903.99 && SalarioBruto <= 2826.65){
            ImpostoRenda = (SalarioBruto * 0.075) - 142.80;

        }if (SalarioBruto >= 2826.66 && SalarioBruto <= 3751,05){
            ImpostoRenda = (SalarioBruto * 0.15) - 354.80;
        }
        if (SalarioBruto >= 3751,06 && SalarioBruto <= 4664,68){
            ImpostoRenda = (SalarioBruto * 0.225) - 636.13
        }
        if (SalarioBruto > 4664.68){
            ImpostoRenda = (SalarioBruto * 0.275) - 869.36
        }
    }
    public float Inss {get; set;} = {
        if (SalarioBruto <= 1693,72){
            Inss = SalarioBruto * 0.8;
        }
        if (SalarioBruto >= 1693,73 && SalarioBruto <= 2822.90){
            Inss = SalarioBruto * 0.9;
        }
        if (SalarioBruto >= 2822.91 && SalarioBruto <= 5645.80){
            Inss = SalarioBruto * 0.11;
        }
        if (SalarioBruto > 5645.81){
            Inss = SalarioBruto + 621.03
        }
    }
    public float Fgts {get; set;}
    public float SalarioLiquido {get; set;}
}