// #nullable disable
using AutoMapper;
using bahmapi.Controllers;
using bahmapi.Dtos;
using bahmapi.Entities;
using bahmapi.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using UnitTestsFixed;

namespace bahmapi.Tests;

public class UnitTest1
{
    [Fact]
    public async Task Get_OnSucess_ReturnsDetails()
    {



        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "test_db")
            .Options;
        var dbContext = new DatabaseContext(options);


        UsuarioService usuarioService = new UsuarioService(dbContext);





        usuarioService.Novo(AutoMapperFix.Mapper.Map<Usuario>(new UsuarioDto
        {
            EmailUsuario = "teste123",
            SenhaUsuario = "1231321"
        }));


        usuarioService.Novo(AutoMapperFix.Mapper.Map<Usuario>(new UsuarioDto
        {
            EmailUsuario = "novo usuario",
            SenhaUsuario = "1231321"
        }));

        var sut = new UsuarioController(
            AutoMapperFix.Mapper,
            new TokenService(),
            new UsuarioService(dbContext),
            new EmailService()
        );

        var result = await sut.ListaTodos();
        var objectResult = (OkObjectResult)result;
        var resultUsuario = (List<UsuarioDto>)objectResult.Value;
        foreach (var i in resultUsuario)
        {
            Console.WriteLine(i.EmailUsuario);
        }
    }


    // [Fact]
    // public async Task Get_OnSucess_ReturnsListOfUsers()
    // {
    //     var mockMapperService = new Mock<IMapper>();
    //     var mockUserService = new Mock<IUsuarioService>();
    //     var mockTkenService = new Mock<TokenService>();
    //     var mockEmailService = new Mock<EmailService>();

    //     mockUserService
    //     .Setup(service => service.ListaTodos())
    //     .ReturnsAsync(new List<Usuario>());

    //     var sut = new UsuarioController(
    //         AutoMapperFix.Mapper,
    //         mockTkenService.Object,
    //         mockUserService.Object,
    //         mockEmailService.Object
    //     );

    //     var result = await sut.ListaTodos();

    //     result.Should().BeOfType<OkObjectResult>();
    //     var objectResult = (OkObjectResult)result;
    //     objectResult.Value.Should().BeOfType<List<UsuarioDto>>();

    // }

    // [Fact]
    // public async Task Get_OnSucess_PaginaReturnsDetails()
    // {

    //     var mockDbContext = new Mock<DatabaseContext>();
    //     var mockPaginaService = new Mock<IPaginaService>();
    //     // var mockAuthenticatedUserUSer = new Mock<AuthenticatedUser>();

    //     mockPaginaService
    //     .Setup(service => service.Pagina(1))
    //     .ReturnsAsync(new PaginaDto());

    //     var sut = new PaginaController(
    //         AutoMapperFix.Mapper

    //         // mockDbContext.Object,
    //         // mockPaginaService.Object
    //     );

    //     var result = await sut.Pagina(1);

    //     // Console.WriteLine(result.ToString());

    //     result.Should().BeOfType<OkObjectResult>();
    //     var objectResult = (OkObjectResult)result;
    //     objectResult.Value.Should().BeOfType<PaginaDto>();

    // }



}