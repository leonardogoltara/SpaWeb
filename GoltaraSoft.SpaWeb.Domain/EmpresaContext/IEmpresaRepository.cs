namespace GoltaraSolutions.SpaWeb.Domain.EmpresaContext
{
    public interface IEmpresaRepository
    //: IRepository<EmpresaModel>
    {
        EmpresaModel Find(string cnpj);
        EmpresaModel Find(long id);
        EmpresaModel FindIncludingAll(long id);
        void Save(EmpresaModel model);
        void Delete(long id);
        void PopularBancoTeste(EmpresaModel empresa);
    }
}
