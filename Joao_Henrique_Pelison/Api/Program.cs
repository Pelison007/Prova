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
    var existe = ctx.TabelaFuncionario.Any(c => c.cpf == funcionario.cpf);
    if (existe){
        return Results.BadRequest("Cpf ja cadastrado");
    }
    if (funcionario.mes < 1 || funcionario.mes > 12){
        return Results.BadRequest("O mês deve estar entre 1 e 12");
    }
    if (funcionario.ano < 2000){
        return Results.BadRequest("O ano deve ser maior ou igual a 2000.");
    }
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
    double cpf,
    int mes,
    int ano,
    [FromBody] Funcionarios funcionario,
    [FromServices] AppDbContext ctx) =>
{
    var existe = ctx.TabelaFuncionario.Find(funcionario.cpf);
    if (existe != null){
        var existe1 = ctx.TabelaFuncionario.Find(funcionario.mes);
        if (existe1 != null){
            var existe2 = ctx.TabelaFuncionario.Find(funcionario.ano);
            if(existe2 != null)
            {
                return Results.Ok(funcionario);
            }    
        }
        return Results.NotFound("Esse Mes não existe");
    }
    return Results.NotFound("Esse Cpf nao Existe");
});

// -- Funcionalidade 3
app.MapDelete("/api/folha/remover/{cpf}/{mes}/{ano}", (
    double cpf,
    int mes,
    int ano,
    [FromBody] Funcionarios funcionario,
    [FromServices] AppDbContext ctx) =>
{
    var existe = ctx.TabelaFuncionario.Find(funcionario.cpf);
    if (existe != null){
        var existe1 = ctx.TabelaFuncionario.Find(funcionario.mes);
        if (existe1 != null){
            var existe2 = ctx.TabelaFuncionario.Find(funcionario.ano);
            if(existe2 != null)
            {
                ctx.TabelaFuncionario.Remove(funcionario);
                ctx.SaveChanges();
                return Results.Ok("Funcionario Excluido");
            }    
        }
        return Results.NotFound("Esse Mes não existe");
    }
    return Results.NotFound("Esse Cpf nao Existe");
});

app.Run();
