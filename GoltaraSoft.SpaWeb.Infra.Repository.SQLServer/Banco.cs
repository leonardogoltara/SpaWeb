using GoltaraSolutions.SpaWeb.Domain.ClienteContext;
using GoltaraSolutions.SpaWeb.Domain.EmpresaContext;
using GoltaraSolutions.SpaWeb.Domain.FuncionarioContext;
using GoltaraSolutions.SpaWeb.Domain.ProdutoContext;
using GoltaraSolutions.SpaWeb.Domain.ServicoContext;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.SQLServer
{
    internal static class Banco
    {
        public static void Seed(Contexto context)
        {
            try
            {
                EmpresaModel empresa = new EmpresaModel("48960528000139", "Goltara Solutions", "Leonardo", "(11) 94555-1463", "lsgolt94@gmail.com");
                context.Empresas.AddOrUpdate(x => x.CNPJ, empresa);

                context.Servicos.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ServicoModel(empresa, "Corte", 25, true));
                context.Servicos.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ServicoModel(empresa, "Luzes", 60, true));
                context.Servicos.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ServicoModel(empresa, "Relaxamento", 90, true));
                context.Servicos.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ServicoModel(empresa, "Hidratação", 80, true));
                context.Servicos.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ServicoModel(empresa, "Progressiva", 80, true));
                context.Servicos.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ServicoModel(empresa, "Sombrancelha", 30, false));
                context.Servicos.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ServicoModel(empresa, "Sombrancelha Definitiva", 30, false));
                context.Servicos.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ServicoModel(empresa, "Unhas - Pés", 20, false));
                context.Servicos.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ServicoModel(empresa, "Unhas - Mãos", 20, true));
                context.Servicos.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ServicoModel(empresa, "Unhas - Pés/Mãos", 30, false));
                context.SaveChanges();

                context.Produtos.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ProdutoModel(empresa, "Gel", 10));
                context.Produtos.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ProdutoModel(empresa, "Creme Hidratante", 45));
                context.Produtos.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ProdutoModel(empresa, "Shanpoo", 45));
                context.Produtos.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ProdutoModel(empresa, "Condicionador", 30));
                context.SaveChanges();

                context.Origens.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new OrigemModel(empresa, "Facebook"));
                context.Origens.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new OrigemModel(empresa, "Site"));
                context.Origens.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new OrigemModel(empresa, "Panfleto"));
                context.Origens.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new OrigemModel(empresa, "Indicação de Funcionário"));
                context.Origens.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new OrigemModel(empresa, "Indicação de Cliente"));
                context.Origens.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new OrigemModel(empresa, "Blog"));
                context.Origens.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new OrigemModel(empresa, "Instagram"));
                context.Origens.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new OrigemModel(empresa, "Display de Propaganda"));
                context.Origens.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new OrigemModel(empresa, "Outdoor"));
                context.Origens.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new OrigemModel(empresa, "Twitter"));
                context.Origens.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new OrigemModel(empresa, "Feiras de Exposição"));
                context.Origens.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new OrigemModel(empresa, "Outros"));
                context.SaveChanges();

                context.Funcionarios.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new FuncionarioModel(empresa, "Fernando João Rafael Rocha", new DateTime(1980, 5, 7)
                    , "(11) 4555-1463", "(11) 97164-5267", "fernando@gmail.com", "M", new List<ServicoModel> { context.Servicos.Find(1), context.Servicos.Find(2) }));
                context.Funcionarios.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new FuncionarioModel(empresa, "Eloisa Pereira Bueno", new DateTime(1986, 5, 7)
                    , "(11) 4555-1463", "(11) 97164-5267", "pereira@gmail.com", "F", new List<ServicoModel> { context.Servicos.Find(3), context.Servicos.Find(4) }));
                context.Funcionarios.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new FuncionarioModel(empresa, "Cassia Moraes Pacheco", new DateTime(1994, 5, 7)
                    , "(11) 4555-1463", "(11) 97164-5267", "pacheco@gmail.com", "F", new List<ServicoModel> { context.Servicos.Find(5), context.Servicos.Find(6) }));
                context.Funcionarios.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new FuncionarioModel(empresa, "Francisco Erivaldo", new DateTime(1975, 5, 7)
                    , "(11) 4555-1463", "(11) 97164-5267", "erivaldo@gmail.com", "M", new List<ServicoModel> { context.Servicos.Find(7), context.Servicos.Find(8) }));
                context.Funcionarios.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new FuncionarioModel(empresa, "Marcos Antonio", new DateTime(1988, 5, 7)
                    , "(11) 4555-1463", "(11) 97164-5267", "antonio@gmail.com", "M", new List<ServicoModel> { context.Servicos.Find(9), context.Servicos.Find(10) }));

                context.SaveChanges();


                context.Clientes.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ClienteModel(empresa, "Isaac Tomás Alves", new DateTime(1995, 2, 8)
                    , "(61) 99712-0510", "(61) 3626-9145", "italves@stetnet.com.br", "M", context.Origens.Find(1)));

                context.Clientes.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ClienteModel(empresa, "Heloisa Julia Ester Moura", new DateTime(1986, 11, 2)
                    , "(65) 2981-9395", "(65) 98441-9512", "heloisa_julia@grupointegraambiental.com.br", "F", context.Origens.Find(2)));

                context.Clientes.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ClienteModel(empresa, "Lívia Stella Oliveira", new DateTime(1986, 8, 9)
                    , "(79) 3654-6457", "(79) 99361-6284", "livia-stella76@andressamelo.com.br", "F", context.Origens.Find(3)));

                context.Clientes.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ClienteModel(empresa, "Anthony Lorenzo Davi Barros", new DateTime(1995, 11, 1)
                    , "(86) 2505-9900", "(86) 99233-9908", "anthony_l_barros@flextroniocs.copm.br", "M", context.Origens.Find(4)));

                context.Clientes.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ClienteModel(empresa, "Stefany Isabel Cardoso", new DateTime(1990, 11, 19)
                    , "(79) 2933-8743", "(79) 99474-1231", "stefany.isabel.cardoso@br.ibn.com", "F", context.Origens.Find(5)));


                context.Clientes.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ClienteModel(empresa, "Tomás Cardoso", new DateTime(1992, 12, 8)
                    , "(61) 99712-0510", "(61) 3626-9145", "tcardoso@stetnet.com.br", "M", context.Origens.Find(1)));

                context.Clientes.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ClienteModel(empresa, "Isabel Oliveira", new DateTime(1991, 4, 2)
                    , "(65) 2981-9395", "(65) 98441-9512", "ioliveira@grupointegraambiental.com.br", "F", context.Origens.Find(2)));

                context.Clientes.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ClienteModel(empresa, "Stefany Moura", new DateTime(1983, 3, 9)
                    , "(79) 3654-6457", "(79) 99361-6284", "smoura@andressamelo.com.br", "F", context.Origens.Find(3)));

                context.Clientes.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ClienteModel(empresa, "Lorenzo Barros", new DateTime(1991, 5, 1)
                    , "(86) 2505-9900", "(86) 99233-9908", "lbarro@flextroniocs.copm.br", "M", context.Origens.Find(4)));

                context.Clientes.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ClienteModel(empresa, "Davi Alves", new DateTime(1974, 7, 19)
                    , "(79) 2933-8743", "(79) 99474-1231", "dalves@br.ibn.com", "M", context.Origens.Find(5)));
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                //throw;
            }
        }
        public static void Seed(EmpresaModel empresa, Contexto context)
        {
            try
            {
                context.Servicos.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ServicoModel(empresa, "Corte", 25, true));
                context.Servicos.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ServicoModel(empresa, "Luzes", 60, true));
                context.Servicos.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ServicoModel(empresa, "Relaxamento", 90, true));
                context.Servicos.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ServicoModel(empresa, "Hidratação", 80, true));
                context.Servicos.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ServicoModel(empresa, "Progressiva", 80, true));
                context.Servicos.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ServicoModel(empresa, "Sombrancelha", 30, false));
                context.Servicos.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ServicoModel(empresa, "Sombrancelha Definitiva", 30, false));
                context.Servicos.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ServicoModel(empresa, "Unhas - Pés", 20, false));
                context.Servicos.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ServicoModel(empresa, "Unhas - Mãos", 20, true));
                context.Servicos.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ServicoModel(empresa, "Unhas - Pés/Mãos", 30, false));
                context.SaveChanges();

                context.Produtos.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ProdutoModel(empresa, "Gel", 10));
                context.Produtos.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ProdutoModel(empresa, "Creme Hidratante", 45));
                context.Produtos.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ProdutoModel(empresa, "Shanpoo", 45));
                context.Produtos.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ProdutoModel(empresa, "Condicionador", 30));
                context.SaveChanges();

                context.Origens.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new OrigemModel(empresa, "Facebook"));
                context.Origens.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new OrigemModel(empresa, "Site"));
                context.Origens.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new OrigemModel(empresa, "Panfleto"));
                context.Origens.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new OrigemModel(empresa, "Indicação de Funcionário"));
                context.Origens.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new OrigemModel(empresa, "Indicação de Cliente"));
                context.Origens.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new OrigemModel(empresa, "Blog"));
                context.Origens.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new OrigemModel(empresa, "Instagram"));
                context.Origens.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new OrigemModel(empresa, "Display de Propaganda"));
                context.Origens.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new OrigemModel(empresa, "Outdoor"));
                context.Origens.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new OrigemModel(empresa, "Twitter"));
                context.Origens.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new OrigemModel(empresa, "Feiras de Exposição"));
                context.Origens.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new OrigemModel(empresa, "Outros"));
                context.SaveChanges();

                context.Funcionarios.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new FuncionarioModel(empresa, "Fernando João Rafael Rocha", new DateTime(1980, 5, 7)
                    , "(11) 4555-1463", "(11) 97164-5267", "fernando@gmail.com", "M", new List<ServicoModel> { context.Servicos.Find(1), context.Servicos.Find(2) }));
                context.Funcionarios.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new FuncionarioModel(empresa, "Eloisa Pereira Bueno", new DateTime(1986, 5, 7)
                    , "(11) 4555-1463", "(11) 97164-5267", "pereira@gmail.com", "F", new List<ServicoModel> { context.Servicos.Find(3), context.Servicos.Find(4) }));
                context.Funcionarios.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new FuncionarioModel(empresa, "Cassia Moraes Pacheco", new DateTime(1994, 5, 7)
                    , "(11) 4555-1463", "(11) 97164-5267", "pacheco@gmail.com", "F", new List<ServicoModel> { context.Servicos.Find(5), context.Servicos.Find(6) }));
                context.Funcionarios.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new FuncionarioModel(empresa, "Francisco Erivaldo", new DateTime(1975, 5, 7)
                    , "(11) 4555-1463", "(11) 97164-5267", "erivaldo@gmail.com", "M", new List<ServicoModel> { context.Servicos.Find(7), context.Servicos.Find(8) }));
                context.Funcionarios.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new FuncionarioModel(empresa, "Marcos Antonio", new DateTime(1988, 5, 7)
                    , "(11) 4555-1463", "(11) 97164-5267", "antonio@gmail.com", "M", new List<ServicoModel> { context.Servicos.Find(9), context.Servicos.Find(10) }));

                context.SaveChanges();


                context.Clientes.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ClienteModel(empresa, "Isaac Tomás Alves", new DateTime(1995, 2, 8)
                    , "(61) 99712-0510", "(61) 3626-9145", "italves@stetnet.com.br", "M", context.Origens.Find(1)));

                context.Clientes.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ClienteModel(empresa, "Heloisa Julia Ester Moura", new DateTime(1986, 11, 2)
                    , "(65) 2981-9395", "(65) 98441-9512", "heloisa_julia@grupointegraambiental.com.br", "F", context.Origens.Find(2)));

                context.Clientes.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ClienteModel(empresa, "Lívia Stella Oliveira", new DateTime(1986, 8, 9)
                    , "(79) 3654-6457", "(79) 99361-6284", "livia-stella76@andressamelo.com.br", "F", context.Origens.Find(3)));

                context.Clientes.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ClienteModel(empresa, "Anthony Lorenzo Davi Barros", new DateTime(1995, 11, 1)
                    , "(86) 2505-9900", "(86) 99233-9908", "anthony_l_barros@flextroniocs.copm.br", "M", context.Origens.Find(4)));

                context.Clientes.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ClienteModel(empresa, "Stefany Isabel Cardoso", new DateTime(1990, 11, 19)
                    , "(79) 2933-8743", "(79) 99474-1231", "stefany.isabel.cardoso@br.ibn.com", "F", context.Origens.Find(5)));


                context.Clientes.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ClienteModel(empresa, "Tomás Cardoso", new DateTime(1992, 12, 8)
                    , "(61) 99712-0510", "(61) 3626-9145", "tcardoso@stetnet.com.br", "M", context.Origens.Find(1)));

                context.Clientes.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ClienteModel(empresa, "Isabel Oliveira", new DateTime(1991, 4, 2)
                    , "(65) 2981-9395", "(65) 98441-9512", "ioliveira@grupointegraambiental.com.br", "F", context.Origens.Find(2)));

                context.Clientes.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ClienteModel(empresa, "Stefany Moura", new DateTime(1983, 3, 9)
                    , "(79) 3654-6457", "(79) 99361-6284", "smoura@andressamelo.com.br", "F", context.Origens.Find(3)));

                context.Clientes.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ClienteModel(empresa, "Lorenzo Barros", new DateTime(1991, 5, 1)
                    , "(86) 2505-9900", "(86) 99233-9908", "lbarro@flextroniocs.copm.br", "M", context.Origens.Find(4)));

                context.Clientes.AddOrUpdate(x => new { x.Nome, x.IdEmpresa }, new ClienteModel(empresa, "Davi Alves", new DateTime(1974, 7, 19)
                    , "(79) 2933-8743", "(79) 99474-1231", "dalves@br.ibn.com", "M", context.Origens.Find(5)));
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                //throw;
            }
        }
    }
}
