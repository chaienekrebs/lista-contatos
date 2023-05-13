namespace Contatos.Domain.DTO
{
    public class ListaPaginada<T>
    {
        public ListaPaginada()
        {
            Lista = new List<T>();
        }

        public List<T> Lista { get; set; }
        public int TotalRegistros { get; set; }
    }
}
