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
                    Console.WriteLine("#ID {0} - {1}", serie.retornaId(), serie.retornaTitulo());
                }
            }
        }

        private static void InserirSeries(){
            Console.WriteLine("---Inserir Série---");

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

            Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);
            
            repositorio.Insere(novaSerie);
        }

        private static void AtualizarSeries(){
            Console.WriteLine("---Atualizar Série---");

            Console.WriteLine("Digite o ID da série: ");
            int entradaId = int.Parse(Console.ReadLine());

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
        }

        private static void ExcluirSeries(){
            Console.WriteLine("---Excluir Série---");

            Console.WriteLine("Digite o ID da série: ");
            int entradaId = int.Parse(Console.ReadLine());

            repositorio.Exclui(entradaId);
        }

        private static void VisualizarSeries(){
            Console.WriteLine("---Visualizar Série---");

            Console.WriteLine("Digite o ID da série: ");
            int entradaId = int.Parse(Console.ReadLine());

            var serie = repositorio.RetornaPorId(entradaId);

            Console.WriteLine(serie);
        }

        private static string lerOpcao(){
            Console.WriteLine();
            Console.WriteLine("-----SÉRIES-----");
            Console.WriteLine("Informe a opção desejada:");
            Console.WriteLine("1 - Listar Séries");
            Console.WriteLine("2 - Inserir Série");
            Console.WriteLine("3 - Atualizar Série");
            Console.WriteLine("4 - Excluir Série");
            Console.WriteLine("5 - Visualizar Série");
            Console.WriteLine("C - Limpar Tela");
            Console.WriteLine("X - Sair");
            Console.WriteLine();

            string opcao = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcao;
        }
    }
}
