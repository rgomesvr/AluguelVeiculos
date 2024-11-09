using System;
using System.Collections.Generic;

namespace SistemaEstacionamento
{
    class Program
    {
        static void Main(string[] args)
        {
            Estacionamento estacionamento = new Estacionamento();
            bool sair = false;

            while (!sair)
            {
                Console.WriteLine("\nEscolha uma opção:");
                Console.WriteLine("1 - Adicionar veículo");
                Console.WriteLine("2 - Remover veículo");
                Console.WriteLine("3 - Listar veículos");
                Console.WriteLine("4 - Sair");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        estacionamento.AdicionarVeiculo();
                        break;
                    case "2":
                        estacionamento.RemoverVeiculo();
                        break;
                    case "3":
                        estacionamento.ListarVeiculos();
                        break;
                    case "4":
                        sair = true;
                        break;
                    default:
                        Console.WriteLine("Opção inválida, tente novamente.");
                        break;
                }
            }
        }
    }

    class Estacionamento
    {
        private List<Veiculo> veiculos = new List<Veiculo>();
        private decimal precoPorHora = 5.0m;

        public void AdicionarVeiculo()
        {
            Console.Write("Digite a placa do veículo para estacionar: ");
            string placa = Console.ReadLine();
            veiculos.Add(new Veiculo { Placa = placa, HoraEntrada = DateTime.Now });
            Console.WriteLine($"Veículo com placa {placa} adicionado.");
        }

        public void RemoverVeiculo()
        {
            Console.Write("Digite a placa do veículo a ser removido: ");
            string placa = Console.ReadLine();

            Veiculo veiculo = veiculos.Find(v => v.Placa.ToUpper() == placa.ToUpper());

            if (veiculo != null)
            {
                DateTime horaSaida = DateTime.Now;
                TimeSpan permanencia = horaSaida - veiculo.HoraEntrada;
                decimal valorCobrado = precoPorHora * (decimal)Math.Ceiling(permanencia.TotalHours);

                veiculos.Remove(veiculo);
                Console.WriteLine(
                    $"Veículo com placa {placa} removido. Valor cobrado: R$ {valorCobrado:F2}"
                );
            }
            else
            {
                Console.WriteLine("Veículo não encontrado.");
            }
        }

        public void ListarVeiculos()
        {
            if (veiculos.Count > 0)
            {
                Console.WriteLine("Veículos estacionados:");
                foreach (var veiculo in veiculos)
                {
                    Console.WriteLine($"Placa: {veiculo.Placa}, Entrada: {veiculo.HoraEntrada}");
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }
    }

    class Veiculo
    {
        public string Placa { get; set; }
        public DateTime HoraEntrada { get; set; }
    }
}
