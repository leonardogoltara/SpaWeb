using GoltaraSolutions.SpaWeb.Domain.ProdutoContext;

namespace GoltaraSolutions.SpaWeb.Infra.Repository.DapperSQLServer.ProdutoContext
{
    public class ProdutoRepository : SimpleCRUD<ProdutoModel>, IProdutoRepository
    {
        public ProdutoRepository() : base()
        {
        }
    }
}
