using System;

namespace dio_series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();

        static void Main(string[] args){
            string opcao = lerOpcao();

            while (opcao.ToUpper() != "X"){
                switch (opcao){
                    case "1":
                        ListarSeries();
                        break;
                    
                    case "2":
                        InserirSeries();
                        break;

                    case "3":
                        AtualizarSeries();
                        break;

                    case "4":
                        ExcluirSeries();
                        break;

                    case "5":
                        VisualizarSeries();
                        break;
                    
                    case "6":
                        AssistirSerie();
                        break;

                    case "C":
                        Console.Clear();
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                opcao = lerOpcao();
            }

            Console.WriteLine("---");
            Console.ReadLine();
        }

        private static void ListarSeries(){
            Console.WriteLine("---Listar Séries---");

            var lista = repositorio.Lista();

            if(lista.Count == 0){
                Console.WriteLine("Nenhuma série cadastrada.");
                return;
            }

            foreach (var serie in lista){
                var excluido = serie.retornaExcluido();
                if(!excluido){
                    var check = serie.retornaAssistida();
                    Console.WriteLine("#ID {0} - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (check ? "✓" : ""));
                }
            }
        }

        private static void InserirSeries(){
            Console.WriteLine("---Inserir Série---");
            Console.WriteLine();

            foreach (int i in Enum.GetValues(typeof(Genero))){
                Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }

            Console.WriteLine("Escolha o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());
            
            Console.WriteLine();
            Console.WriteLine("Digite o título: ");
            string entradaTitulo = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Digite o ano de lançamento: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.WriteLine();
            Console.WriteLine("Digite a descrição: ");
            string entradaDescricao = Console.ReadLine();

            Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);
            
            repositorio.Insere(novaSerie);

            Console.WriteLine();
            Console.WriteLine("Série inserida.");
            Console.WriteLine("#ID: {0}", novaSerie.Id);
            Console.WriteLine();
        }

        private static void AtualizarSeries(){
            Console.WriteLine("---Atualizar Série---");

            Console.WriteLine("Digite o ID da série: ");
            int entradaId = int.Parse(Console.ReadLine());

            Console.WriteLine();
            Console.WriteLine("Série encontrada:");
            var serie = repositorio.RetornaPorId(entradaId);
            Console.WriteLine(serie);

            Console.WriteLine();
            Console.WriteLine("Inserir novos dados:");
            Console.WriteLine();

            foreach (int i in Enum.GetValues(typeof(Genero))){
                Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }

            Console.WriteLine("Escolha o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());
            
            Console.WriteLine("Digite o título: ");
            string entradaTitulo = Console.ReadLine();

            Console.WriteLine("Digite o ano de lançamento: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite a descrição: ");
            string entradaDescricao = Console.ReadLine();

            Serie atualizaSerie = new Serie(id: entradaId,
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);
            
            repositorio.Atualiza(entradaId, atualizaSerie);
            Console.WriteLine();
            Console.WriteLine("Dados atualizados. ");
            Console.WriteLine();
        }

        private static void ExcluirSeries(){
            Console.WriteLine("---Excluir Série---");

            Console.WriteLine("Digite o ID da série: ");
            int entradaId = int.Parse(Console.ReadLine());

            Console.WriteLine();
            var serie = repositorio.Lista();
            Console.WriteLine("Deseja excluir '{0}'?", serie[entradaId].retornaTitulo());
            Console.WriteLine("Y / N ");
            string opcao = Console.ReadLine().ToUpper();

            if(opcao.ToUpper() == "Y"){
                repositorio.Exclui(entradaId);
                Console.WriteLine();
                Console.WriteLine("'{0}' foi excluída.", serie[entradaId].retornaTitulo());
            } else {
                Console.WriteLine();
                Console.WriteLine("Ação cancelada. ");
                Console.WriteLine();
            }
        }

        private static void VisualizarSeries(){
            Console.WriteLine("---Visualizar Série---");

            Console.WriteLine("Digite o ID da série: ");
            int entradaId = int.Parse(Console.ReadLine());

            var serie = repositorio.RetornaPorId(entradaId);

            Console.WriteLine();
            Console.WriteLine(serie);
            Console.WriteLine();
        }

        private static void AssistirSerie(){
            Console.WriteLine("---Série Assistida---");

            Console.WriteLine("Digite o ID da série: ");
            int entradaId = int.Parse(Console.ReadLine());

            Console.WriteLine();
            Console.WriteLine("Série encontrada:");
            var info = repositorio.RetornaPorId(entradaId);
            Console.WriteLine(info);
            Console.WriteLine();

            var serie = repositorio.Lista();
            Console.WriteLine("Deseja marcar '{0}' como assistida?", serie[entradaId].retornaTitulo());
            Console.WriteLine("Y / N ");
            string opcao = Console.ReadLine().ToUpper();

            if(opcao.ToUpper() == "Y"){
                serie[entradaId].Assistir();
                Console.WriteLine(info);
                Console.WriteLine();
            } else {
                Console.WriteLine();
                Console.WriteLine("Ação cancelada. ");
                Console.WriteLine();
            }
        }

        private static string lerOpcao(){
            Console.WriteLine();
            Console.WriteLine("SÉRIES");
            Console.WriteLine("Informe a opção desejada:");
            Console.WriteLine("1 - Listar Séries");
            Console.WriteLine("2 - Inserir Série");
            Console.WriteLine("3 - Atualizar Dados da Série");
            Console.WriteLine("4 - Excluir Série");
            Console.WriteLine("5 - Visualizar Série");
            Console.WriteLine("6 - Marcar como Assistida");
            Console.WriteLine("C - Limpar Tela");
            Console.WriteLine("X - Sair");
            Console.WriteLine();

            string opcao = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcao;
        }
    }
}
