using CrediariaSoChorei;
using System.Diagnostics;
using static System.Formats.Asn1.AsnWriter;

List<CCorrente> contas = new List<CCorrente>();

int MenuAcesso()
{
    int op;
    string? escolha;
    Console.Clear();
    Console.WriteLine("Bem vindo ao Menu Principal, como posso ajudar?");
    Console.WriteLine("\n\nTIPO DE ACESSO");
    Console.WriteLine("1- Acesso administrativo");
    Console.WriteLine("2- Caixa eletrônico");
    Console.WriteLine("0- Sair");

    Console.Write("\nOpção desejada:");
    escolha = Console.ReadLine();
    Int32.TryParse(escolha, out op);

    return op;
}
 //=================================================================================//
void Administrativo()
{
    int op;
    string? escolha;

    do
    {
        Console.WriteLine("\n//=================================================================================//");
        Console.WriteLine("\nACESSO ADMINISTRATIVO");
        Console.WriteLine("1- Cadastro de conta corrente");
        Console.WriteLine("2- Mostrar saldo de todas as contas");
        Console.WriteLine("3- Excluir conta");
        Console.WriteLine("0- Voltar");

        Console.Write("\nDigite sua opção:");
        escolha = Console.ReadLine();
        Int32.TryParse(escolha, out op);
        switch (op)
        {
            case 1:
                Console.WriteLine("\n\nCADASTRO DE CONTA CORRENTE");
                CadastroCC();
                break;

            case 2:
                Console.WriteLine("\n\nMOSTRAR SALDO DE TODAS AS CONTAS");
                MostraSaldos();
                break;

            case 3:
                Console.WriteLine("\n\nEXCLUIR CONTA");
                ExcluirCC();
                break;

            case 0:
                break;

            default:
                Console.WriteLine("\nPor favor, insira um valor válido.");
                break;
        }
    } while (op != 0);
    Console.WriteLine("\n\n===================================================================================\n");
}

void CadastroCC()
{
    Console.Write("Digite o numero da conta: ");
    string? num = Console.ReadLine();
    double limiteCC;
    CCorrente? conta = contas.Find(c => c.numero == num);

    if(conta == null)
    {
        Console.Write("\nDigite qual será o limite da conta: ");
        string? valor = Console.ReadLine();
        bool n = Double.TryParse(valor, out limiteCC);  

        while(limiteCC < 0 || !n)
        {
            Console.Write("\nPor favor, insira um valor válido: ");
            valor = Console.ReadLine();
            n = Double.TryParse(valor, out limiteCC);
        }
        contas.Add(new CCorrente(num, limiteCC));
    }
    else
    {
        Console.WriteLine("Essa conta já existe.");
    }
}

void MostraSaldos()
{
    Console.WriteLine("(numero) - (status) -> (saldo)");
    foreach(var conta in contas) 
    {
        if(conta.status == true)
            Console.WriteLine(conta.numero + " - (ativada) -> " + conta.saldo);
        else
            Console.WriteLine(conta.numero + " - (desativada) -> " + conta.saldo);
    }
}

void ExcluirCC()
{
    Console.Write("Digite o número da conta que deseja excluir: ");
    string? num = Console.ReadLine();
    CCorrente? conta = contas.Find(c => c.numero == num);

    if (conta != null)
    {
        if(conta.saldo == 0)
            Console.WriteLine("Ação concluida com sucesso.");
        else
            Console.WriteLine("Não foi possivel excluir a conta. Verifique se o saldo da conta que deseja excluir seja igual a 0 (zero).");
    }
    else
    {
        Console.WriteLine("A ação não pode ser efetivada. Verifique se o cógido foi inserido corretamente.");
    }
}

//=================================================================================//

void CaixaEletro()
{
    string? preferencia;
    int opcao;
    bool n;
    double valores;
    Console.WriteLine("\n//=================================================================================//");
    Console.WriteLine("\nCAIXA ELETRÔNICO");
    Console.Write("Digite o numero da conta para consulta: ");
    string? num = Console.ReadLine();
    CCorrente? conta = contas.Find(c => c.numero == num);

    if(conta != null)
    {
        do
        {
            Console.WriteLine("\nO QUE DESEJA?\n");
            Console.WriteLine("1- Saque");
            Console.WriteLine("2- Depósito");
            Console.WriteLine("3- Transferência");
            Console.WriteLine("0- Voltar");
            Console.Write("Informe o que deseja: ");
            preferencia = Console.ReadLine();
            Int32.TryParse(preferencia, out opcao);

            switch (opcao)
            {
                case 1:
                    Console.Write("\n\nDigite o valor do saque: ");
                    preferencia = Console.ReadLine();
                    n = Double.TryParse(preferencia, out valores);

                    while (valores < 0 || !n)
                    {
                        Console.Write("\nPor favor, tente novamente de maneira válida: ");
                        preferencia = Console.ReadLine();
                        n = Double.TryParse(preferencia, out valores);
                    }
                    if (conta.Sacar(valores))
                    {
                        Console.WriteLine("Saque efetivado com eficiência.");
                    }
                    else
                    {
                        Console.WriteLine("Saldo insuficiente ou acima do limite.");
                    }
                    break;

                case 2:
                    Console.Write("\n\nDigite o valor do deposito: ");
                    preferencia = Console.ReadLine();
                    n = Double.TryParse(preferencia, out valores);

                    while (!n)
                    {
                        Console.Write("\nPor favor, tente novamente de maneira válida: ");
                        preferencia = Console.ReadLine();
                        n = Double.TryParse(preferencia, out valores);
                    }
                    if (conta.Depositar(valores))
                    {
                        Console.WriteLine("Deposito efetivado com eficiência.");
                    }
                    else
                    {
                        Console.WriteLine("Valor invalido pra a acao.");
                    }
                    break;

                case 3:
                    Console.Write("\n\nDigite o numero da conta destino: ");
                    string? ContaDestino = Console.ReadLine();
                    CCorrente? CCdestino = contas.Find(c => c.numero == ContaDestino);

                    if (CCdestino != null)
                    {
                        Console.Write("Digite o valor que deseja transferir: ");
                        preferencia = Console.ReadLine();
                        Double.TryParse(preferencia, out valores);
                        if (conta.Transferir(CCdestino, valores))
                            Console.WriteLine("Transferencia efetuada com sucesso!");
                        else
                            Console.WriteLine("Nao foi possivel realizar a transferencia. É possível que a conta destino esteja desativada. Por favor, tente novamente.");
                    }
                    else
                    {
                        Console.WriteLine("Nao foi possivel encontrar a conta destino.");
                    }
                    break;

                case 0:
                    break;

                default:
                    Console.WriteLine("Por favor, digite um valor valido.");
                    break;
            }
        }while (opcao != 0);
    }
    else
    {
        Console.WriteLine("Nao foi possivel encontrar a conta que deseja, revise o numero inserido.");
    }
    Console.WriteLine("===================================================================================");
}

//=================================================================================//

int escolha;
do
{
    escolha = MenuAcesso();

    switch (escolha) 
    {
        case 1:
            Administrativo();
            break;
        case 2:
            CaixaEletro();
            break;
        case 0:
            Console.WriteLine("Obrigado pela preferencia, volte sempre!");
            break;
        default:
            Console.WriteLine("Por favor, insira uma opcao valida.");
            break;
    }
}while (escolha != 0);
