using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Modelo;
using Api.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();
var app = builder.Build();

app.MapGet("/", () =>
{
    return Results.Ok(" ==== PROVA ====");
});

// -- Funcionalidade 1
app.MapPost("/api/folha/cadastrar", (
    [FromBody] Funcionarios funcionario,
    [FromServices] AppDbContext ctx) =>
{
    
    funcionario.cpf = funcionario.cpf.Replace(".", "").Replace("-", "");

    var existe = ctx.TabelaFuncionario.Any(c =>
        c.cpf == funcionario.cpf &&
        c.mes == funcionario.mes &&
        c.ano == funcionario.ano);

    if (existe){
        return Results.BadRequest("Já existe uma folha cadastrada para este CPF, mês e ano.");
    }
    if (funcionario.mes < 1 || funcionario.mes > 12){
        return Results.BadRequest("O mês deve estar entre 1 e 12");
    }
    if (funcionario.ano < 2000){
        return Results.BadRequest("O ano deve ser maior ou igual a 2000.");
    }

    funcionario.CalcularValores();
    ctx.TabelaFuncionario.Add(funcionario);
    ctx.SaveChanges();
    return Results.Ok("Folha Cadastrada com sucesso.");
});

// -- Funcionalidade 2
app.MapGet("/api/folha/listar", (
    [FromServices] AppDbContext ctx) =>
{
    if (ctx.TabelaFuncionario.Count() == 0)
    {
        return Results.NotFound("A Lista está vazia!");
    }
    return Results.Ok(ctx.TabelaFuncionario.ToList());
});

// -- Funcionalidade 3
app.MapGet("/api/folha/buscar/{cpf}/{mes}/{ano}", (
    string cpf,
    int mes,
    int ano,
    [FromServices] AppDbContext ctx) =>
{
    cpf = cpf.Replace(".", "").Replace("-", "");

    var existe = ctx.TabelaFuncionario.FirstOrDefault(c =>
        c.cpf == cpf &&
        c.mes == mes &&
        c.ano == ano);

    if (existe == null){
        return Results.NotFound("Folha de pagamento não encontrada para este CPF, mês e ano.");
    }
        return Results.Ok(existe);
});

// -- Funcionalidade 3
app.MapDelete("/api/folha/remover/{cpf}/{mes}/{ano}", (
    string cpf,
    int mes,
    int ano,
    [FromServices] AppDbContext ctx) =>
{
    cpf = cpf.Replace(".", "").Replace("-", "");
    var existe = ctx.TabelaFuncionario.FirstOrDefault(c =>
        c.cpf == cpf &&
        c.mes == mes &&
        c.ano == ano);

    if (existe == null){
        return Results.NotFound("Folha de pagamento não encontrada para este CPF, mês e ano.");
    }
        ctx.TabelaFuncionario.Remove(existe);
        ctx.SaveChanges();
        return Results.Ok("Folha removida com sucesso!");
});

// -- Funcionalidade 4
app.MapGet("/api/folha/total-liquido", (
    [FromServices] AppDbContext ctx) =>
{
    if (ctx.TabelaFuncionario.Count() == 0)
    {
        return Results.NotFound("Não existe nenhum funcionario com folhas de pagamentos");
    }

    var total = ctx.TabelaFuncionario.Sum(c => c.SalarioLiquido);

    return Results.Ok(new
    {
        totalLiquido = MathF.Round(total, 2).ToString("C2")
    });
});

app.Run();
