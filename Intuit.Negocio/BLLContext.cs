using Intuit.EF;

namespace Intuit.Negocio
{
    /// <summary>
    /// Clase Abstracta que provee acceso al EF Object Context. Permite utilizar un context para POCOs.
    /// </summary>
    /// <author>Ramiro</author>
    public abstract class BLLContext : IDisposable
    {
        /// <summary>
        /// Contexto principal de MyProyect.
        /// </summary>
        protected IntuitContext Context;

        /// <summary>
        /// Constructor principal. Instancia el Contexto según la cadena de conexión: EFStringConnection.StringConnection.
        /// </summary>
        public BLLContext()
        {
            EFStringConnection.GetStringConnection();
            Context = new IntuitContext(EFStringConnection.StringConnection!);
        }

        #region Miembros de IDisposable

        /// <summary>
        /// Cierra el contexto.
        /// </summary>
        public void Dispose()
        {
            Context.Dispose();
        }

        #endregion

    }

}
