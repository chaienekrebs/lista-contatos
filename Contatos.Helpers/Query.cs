using System.Reflection;

namespace Contatos.Helpers
{
    public static class Query
    {
        public static IQueryable<TEntity> Filtrar<TEntity>(this IQueryable<TEntity> consulta, int limite, int pular) where TEntity : class
        {
            if (limite > 0)
            {
                consulta = consulta.Skip(pular).Take(limite);
            }

            return consulta;
        }

        public static IQueryable<TEntity> FiltrarPaginado<TEntity>(this IQueryable<TEntity> consulta, int start, int length, string sortColumn = "", string sortColumnDirection = "asc") where TEntity : class
        {
            var properties = typeof(TEntity).GetProperties();
            PropertyInfo prop = null;
            foreach (var item in properties)
            {
                if (item.Name.ToLower().Equals(sortColumn.ToLower()))
                {
                    prop = item;
                    break;
                }
            }
            var campoOrdem = prop;
            if (sortColumnDirection == "asc")
            {
                consulta = consulta.OrderBy(campoOrdem.GetValue).AsQueryable().Skip(start).Take(length);
            }
            else
            {
                consulta = consulta.OrderByDescending(campoOrdem.GetValue).AsQueryable().Skip(start).Take(length);
            }
            return consulta;
        }

    }
}
