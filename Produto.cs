public class Produto
{
    public int Id { get; }
    public string Nome { get; }
    public decimal Preco { get; }

    public Produto(int id, string nome, decimal preco)
    {
        Id = id;
        Nome = nome;
        Preco = preco;
    }
}