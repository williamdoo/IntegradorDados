using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WCFServicoIntegradorDados
{
    [RunInstaller(true)]
    public class InstalarServico : Installer
    {
        private ServiceProcessInstaller processo;
        private ServiceInstaller servico;
        public InstalarServico()
        {
            processo = new ServiceProcessInstaller();
            processo.Account = ServiceAccount.LocalSystem;
            servico = new ServiceInstaller();
            servico.ServiceName = "Integrador de Dados";
            servico.DisplayName = "Integrador de Dados";
            servico.Description = "Gerenciamento de integração das informações no banco de dados local e nuvem utilizando o WCF";
            servico.StartType = ServiceStartMode.Automatic;
            Installers.Add(processo);
            Installers.Add(servico);
        }

    }
}
