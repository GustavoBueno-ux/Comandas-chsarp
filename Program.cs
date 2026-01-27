List<Comanda> comandas = new List<Comanda>();
List<Produto> produtos = new List<Produto>()
{
    new Produto(1, "Gu Burguer", 19.90m),
    new Produto(2, "Gu Chicken", 22.90m),
    new Produto(3, "Gu Batatas", 9.90m),
    new Produto(4, "Gu Água", 3.00m),
    new Produto(5, "Gu Refri", 6.00m),
    new Produto(6, "Gu Sorvete", 9.00m)
};

int Id = 1;

bool OpcoesMenu()
{
    Console.Clear();

    Console.WriteLine("1 - Abrir comanda");
    Console.WriteLine("2 - Editar comanda");
    Console.WriteLine("3 - Ver comandas");
    Console.WriteLine("4 - Fechar comanda");
    Console.WriteLine("0 - Sair");

    int escolha = int.Parse(Console.ReadLine()!);

    switch (escolha)
    {
        case 0:
            return false;

        case 1:
            AbrirComanda();
            break;

        case 2:
            EditarComanda();
            break;

        case 3:
            VerComandas();
            break;

        case 4:
            FecharComanda();
            break;

        default:
            Console.WriteLine("Opção inválida!");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            break;
    }

    return true;
}

void AbrirComanda()
{
    Console.Clear();
    Console.Write("Digite a mesa para abrir uma comanda: ");
    int Mesa = int.Parse(Console.ReadLine()!);
    Comanda comanda = new Comanda(Id, Mesa);

    Console.WriteLine($"Comanda #{comanda.Id} aberta para a mesa {comanda.Mesa}");
    Id++;
    comandas.Add(comanda);

    Console.Write($"Deseja continuar editando a comanda #{comanda.Id}? Digite s para sim: ");
    char escolha = char.Parse(Console.ReadLine()!);

    if (escolha == 's')
    {
        EditarComanda();
    } else
    {
        Console.WriteLine("Opção invalida");
        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

}

void EditarComanda()
{
    Console.Clear();
    if (comandas.Count == 0)
    {
        Console.WriteLine("Nenhuma comanda aberta no momento");
    }
    else
    {
        Console.WriteLine("Comandas abertas:");

        foreach (Comanda c in comandas)
        {
            Console.WriteLine($"[{c.Id}] Mesa {c.Mesa}");
        }
        Console.WriteLine("Digite o ID da comanda para editar: ");
    }
    int escolhaId = int.Parse(Console.ReadLine()!);

    Comanda? comandaSelecionada = comandas
    .Find(c => c.Id == escolhaId && c.Aberta);

    if (comandaSelecionada == null)
    {
        Console.WriteLine("Não existe comanda aberta com esse ID.");
        Console.ReadKey();
        return;
    }

    bool editar = true;

    while (editar)
    {
        Console.Clear();
        Console.WriteLine($"Editando comanda #{comandaSelecionada.Id} - Mesa {comandaSelecionada.Mesa}");
        Console.WriteLine("1 - Adicionar item");
        Console.WriteLine("2 - Remover item");
        Console.WriteLine("3 - Ver itens");
        Console.WriteLine("0 - Voltar");

        int opcao = int.Parse(Console.ReadLine()!);

        switch (opcao)
        {
            case 1:
                AdicionarItemNaComanda(comandaSelecionada);
                break;

            case 2:
                RemoverItemNaComanda(comandaSelecionada);
                break;

            case 3:
                VerItens(comandaSelecionada);
                break;

            case 0:
                editar = false;
                break;
        }
    }

    Console.WriteLine("\nPressione qualquer tecla para voltar...");
    Console.ReadKey();
}

void VerComandas()
{
    Console.Clear();
    if (comandas.Count == 0)
    {
        Console.WriteLine("Nenhuma comanda aberta no momento");
    }
    else
    {
        Console.WriteLine("Comandas abertas:");
    }
    foreach (Comanda c in comandas)
    {
        Console.WriteLine($"[{c.Id}] Mesa {c.Mesa}");
    }

    Console.WriteLine("\nPressione qualquer tecla para voltar...");
    Console.ReadKey();
}

void FecharComanda()
{
    Console.Clear();

    if (comandas.Count == 0)
    {
        Console.WriteLine("Nenhuma comanda aberta no momento");
        Console.ReadKey();
        return;
    }

    Console.WriteLine("Comandas abertas:");
    foreach (Comanda c in comandas)
    {
        if (c.Aberta)
            Console.WriteLine($"[{c.Id}] Mesa {c.Mesa}");
    }

    Console.Write("\nDigite o ID da comanda para fechar: ");
    int escolha = int.Parse(Console.ReadLine()!);

    Comanda? comandaSelecionada = comandas
        .Find(c => c.Id == escolha && c.Aberta);

    if (comandaSelecionada == null)
    {
        Console.WriteLine("Não existe comanda aberta com esse ID.");
        Console.ReadKey();
        return;
    }

    var itens = comandaSelecionada.GetItens();

    if (itens.Count == 0)
    {
        Console.WriteLine("Não é possível fechar uma comanda sem itens.");
        Console.ReadKey();
        return;
    }

    decimal total = comandaSelecionada.CalcularTotal();

    Console.WriteLine($"\nTotal da comanda: R${total}");
    Console.Write("Confirmar fechamento? (s/n): ");
    char confirmacao = char.Parse(Console.ReadLine()!);

    if (confirmacao == 's')
    {
        comandaSelecionada.Fechar();
        comandas.Remove(comandaSelecionada);
        Console.WriteLine("Comanda fechada com sucesso!");
    }
    else
    {
        Console.WriteLine("Fechamento cancelado.");
    }

    Console.ReadKey();
}


void AdicionarItemNaComanda(Comanda comanda)
{
    Console.Clear();
    Console.WriteLine("Produtos disponíveis:");

    foreach (var p in produtos)
    {
        Console.WriteLine($"[{p.Id}] {p.Nome} - R${p.Preco}");
    }

    Console.Write("Digite o ID do produto: ");
    int idProduto = int.Parse(Console.ReadLine()!);

    Produto? produto = produtos.Find(p => p.Id == idProduto);

    if (produto == null)
    {
        Console.WriteLine("Produto inválido.");
        Console.ReadKey();
        return;
    }

    Console.Write("Quantidade: ");
    int quantidade = int.Parse(Console.ReadLine()!);

    comanda.AdicionarItem(produto.Nome, produto.Preco, quantidade);

    Console.WriteLine("Item adicionado!");
    Console.ReadKey();
}

void RemoverItemNaComanda(Comanda comanda)
{
    Console.Clear();

    var itens = comanda.GetItens();

    if (itens.Count == 0)
    {
        Console.WriteLine("Nenhum item na comanda.");
        Console.ReadKey();
        return;
    }

    Console.WriteLine($"Itens da comanda #{comanda.Id}:\n");

    for (int i = 0; i < itens.Count; i++)
    {
        var item = itens[i];
        Console.WriteLine($"[{i}] {item.Nome} - Qtd: {item.Quantidade} - R${item.Preco}");
    }

    Console.Write("\nDigite o número do item: ");
    int indice = int.Parse(Console.ReadLine()!);

    Console.Write("Quantidade a remover: ");
    int quantidade = int.Parse(Console.ReadLine()!);

    bool sucesso = comanda.RemoverItem(indice, quantidade);

    if (sucesso)
        Console.WriteLine("Item removido com sucesso!");
    else
        Console.WriteLine("Não foi possível remover o item.");

    Console.ReadKey();
}


void VerItens(Comanda comanda)
{
    Console.Clear();

    var itens = comanda.GetItens();

    if (itens.Count == 0)
    {
        Console.WriteLine("Nenhum item na comanda.");
        Console.ReadKey();
        return;
    }

    Console.WriteLine($"Itens da comanda #{comanda.Id}:\n");

    foreach (var item in itens)
    {
        decimal subtotal = item.Preco * item.Quantidade;

        Console.WriteLine(
            $"{item.Nome} | Qtd: {item.Quantidade} | Unit: R${item.Preco} | Subtotal: R${subtotal}"
        );
    }

    Console.WriteLine("\nPressione qualquer tecla para voltar...");
    Console.ReadKey();
}



while (OpcoesMenu())
{
}