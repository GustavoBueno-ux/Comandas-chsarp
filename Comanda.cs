public class Comanda
{
    public int Id { get; }
    public int Mesa { get; set; }
    public bool Aberta { get; private set; }

    public List<ItemComanda> itens;

    public Comanda(int id, int mesa)
    {
        Id = id;
        Mesa = mesa;
        Aberta = true;
        itens = new List<ItemComanda>();
    }

    public void Fechar()
    {
        Aberta = false;
    }

    public void AdicionarItem(string nome, decimal preco, int quantidade)
{
    ItemComanda? itemExistente = itens
        .Find(i => i.Nome == nome && i.Preco == preco);

    if (itemExistente != null)
    {
        itemExistente.Quantidade += quantidade;
    }
    else
    {
        itens.Add(new ItemComanda(nome, preco, quantidade));
    }
}


    public bool RemoverItem(int indice, int quantidade)
{
    if (indice < 0 || indice >= itens.Count)
        return false;

    if (quantidade <= 0)
        return false;

    ItemComanda item = itens[indice];

    if (quantidade >= item.Quantidade)
    {
        itens.RemoveAt(indice);
    }
    else
    {
        item.Quantidade -= quantidade;
    }

    return true;
}

    public decimal CalcularTotal()
    {
        decimal total = 0;

        foreach (var item in itens)
        {
            total += item.Preco * item.Quantidade;
        }

        return total;
    }

    public List<ItemComanda> GetItens()
    {
        return itens;
    }
}
