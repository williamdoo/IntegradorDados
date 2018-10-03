using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServicoIntegradorDados
{
    public class IntegracaoNuvem : IIntegracaoNuvem
    {
        public void SubirDados()
        {
            using (StreamWriter vWriter = new StreamWriter(@"C:\TesteServico.txt", true))
            {
                vWriter.WriteLine("Servico Rodando: " + DateTime.Now.ToString());
            }
        }        
    }
}
