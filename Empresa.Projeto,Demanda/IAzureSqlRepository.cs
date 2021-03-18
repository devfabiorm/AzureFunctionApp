using System.Collections.Generic;

namespace Empresa.Projeto_Demanda
{
    public interface IAzureSqlRepository
    {
        IEnumerable<UserSql> GetAll();
        void SaveADO(UserSql userSql);
        void SaveDapper(UserSql userSql);
    }
}