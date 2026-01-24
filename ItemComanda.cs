public class ItemComanda
{
    public string Nome { get; }
    public decimal Preco { get; }
    public int Quantidade { get; set; }

    public ItemComanda(string nome, decimal preco, int quantidade)
    {
        Nome = nome;
        Preco = preco;
        Quantidade = quantidade;
    }
}