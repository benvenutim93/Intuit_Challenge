using Intuit.EF;
using Intuit.Models;

namespace Intuit.Negocio
{
    public class ClienteNegocio : BaseNegocio<Cliente>
    {
        public bool Existe(int pId)
        {
            return Context.Clientes.Count(c => c.Id == pId) > 0;
        }
    }
}