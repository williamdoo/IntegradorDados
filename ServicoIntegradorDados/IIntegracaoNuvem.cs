using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServicoIntegradorDados
{
    [ServiceContract]
    public interface IIntegracaoNuvem
    {
        [OperationContract]
        void SubirDados();
    }
}
