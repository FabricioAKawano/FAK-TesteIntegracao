using FAK.TesteIntegracao.Core.Commands;
using FAK.TesteIntegracao.Core.Models;
using FAK.TesteIntegracao.Infrastructure;
using FAK.TesteIntegracao.Services.Handlers;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FAK.TEsteIntegracao.Tests
{
    public class GerenciaPrazoDasTarefasHandlerExecute
    {
        [Fact]
        public void QuandoTarefasEstiveremAtrasadasDeveMudarSeuStatus()
        {
            //arrange
            var compCateg = new Categoria(1, "Compras");
            var casaCateg = new Categoria(2, "Casa");
            var trabCateg = new Categoria(3, "Trabalho");
            var saudCateg = new Categoria(4, "Saúde");
            var higiCateg = new Categoria(5, "Higiene");

            var tarefas = new List<Tarefa>
            {
                //atrasadas a partir de 1/1/2019
                new Tarefa(1, "Tirar lixo", casaCateg, new DateTime(2018,12,31), null, StatusTarefa.Criada),
                new Tarefa(4, "Fazer o almoço", casaCateg, new DateTime(2017,12,1), null, StatusTarefa.Criada),
                new Tarefa(9, "Ir à academia", saudCateg, new DateTime(2018,12,31), null, StatusTarefa.Criada),
                new Tarefa(7, "Concluir o relatório", trabCateg, new DateTime(2018,5,7), null, StatusTarefa.Pendente),
                new Tarefa(10, "Beber água", saudCateg, new DateTime(2018,12,31), null, StatusTarefa.Criada),
                //dentro do prazo em 1/1/2019
                new Tarefa(8, "Comparecer à reunião", trabCateg, new DateTime(2018,11,12), new DateTime(2018,11,30), StatusTarefa.Concluida),
                new Tarefa(2, "Arrumar a cama", casaCateg, new DateTime(2019,4,5), null, StatusTarefa.Criada),
                new Tarefa(3, "Escovar os dentes", higiCateg, new DateTime(2019,1,2), null, StatusTarefa.Criada),
                new Tarefa(5, "Comprar presente pro João", compCateg, new DateTime(2019,10,8), null, StatusTarefa.Criada),
                new Tarefa(6, "Comprar ração", compCateg, new DateTime(2019,11,20), null, StatusTarefa.Criada),
            };

            var options = new DbContextOptionsBuilder<DbTarefasContext>()
                .UseInMemoryDatabase("DbTarefas")
                .Options;
            var context = new DbTarefasContext(options);
            var repositorioTarefas = new RepositorioTarefas(context);
            repositorioTarefas.IncluirTarefas(tarefas.ToArray());

            var command = new GerenciaPrazoDasTarefas(new DateTime(2019, 1, 1));
            var handle = new GerenciaPrazoDasTarefasHandler(repositorioTarefas);

            //action
            handle.Execute(command);
            var tarefasAtrasadas = repositorioTarefas.ObtemTarefas(t => t.Status == StatusTarefa.EmAtraso);

            //assert
            Assert.Equal(5, tarefasAtrasadas.Count());

        }

        [Fact]
        public void QuandoInvocadoDeveChamarAtualizarTarefasNaQtDeVezesDoTotalDeTarefasAtrasadas()
        {
            //arrange
            var categ = new Categoria("Fake");
            var tarefasAtrasadas = new List<Tarefa>
            {
                new Tarefa(1, "Tirar lixo", categ, new DateTime(2018,12,31), null, StatusTarefa.Criada),
                new Tarefa(4, "Fazer o almoço", categ, new DateTime(2017,12,1), null, StatusTarefa.Criada),
                new Tarefa(9, "Ir à academia", categ, new DateTime(2018,12,31), null, StatusTarefa.Criada),
            };
            var mock = new Mock<IRepositorioTarefas>();
            mock.Setup(r => r.ObtemTarefas(It.IsAny<Func<Tarefa, bool>>())).Returns(tarefasAtrasadas);
            var repositorioTarefas = mock.Object;

            var command = new GerenciaPrazoDasTarefas(new DateTime(2019, 1, 1));
            var handler = new GerenciaPrazoDasTarefasHandler(repositorioTarefas);

            //action
            handler.Execute(command);

            //assert
            mock.Verify(r => r.AtualizarTarefas(It.IsAny<Tarefa[]>()), Times.Once());
        }
    }
}
