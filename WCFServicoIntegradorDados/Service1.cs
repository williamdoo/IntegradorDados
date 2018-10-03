using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServicoIntegradorDados;

namespace WCFServicoIntegradorDados
{
    public partial class Service1 : ServiceBase
    {
        ServiceHost hostNuvem, hostLocal;
        Timer timer;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            HabiliarHospedeiro();
        }

        protected override void OnStop()
        {
            try
            {
                FecharHosts();
            }
            catch(Exception ex)
            {
                GerarLog(ex);
            }
        }

        private void HabiliarHospedeiro()
        {
            try
            {
                FecharHosts();
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();

                hostNuvem = new ServiceHost(typeof(IntegracaoNuvem), new Uri("http://localhost:80/Integrador/Nuvem"));                
                hostNuvem.AddServiceEndpoint(typeof(IIntegracaoNuvem), new WSHttpBinding(), "");

                hostLocal = new ServiceHost(typeof(IntegracaoLocal), new Uri("http://localhost:80/Integrador/Local"));
                hostLocal.AddServiceEndpoint(typeof(IIntegracaoLocal), new WSHttpBinding(), "");

                smb.HttpGetEnabled = true;
                hostNuvem.Description.Behaviors.Add(smb);
                hostLocal.Description.Behaviors.Add(smb);

                hostNuvem.Open();
                hostLocal.Open();

                timer = new Timer(new TimerCallback(Timer1_Tick), null, 5000, 30000);
            }
            catch(Exception ex)
            {
                GerarLog(ex);
            }
        }

        private void Timer1_Tick(object sender)
        {
            AcesarServico();
        }

        private void AcesarServico()
        {
            IIntegracaoNuvem client = null;

            try
            {
                var myBinding = new WSHttpBinding();
                var myEndpoint = new EndpointAddress("http://localhost:80/Integrador/Nuvem");
                var myChannelFactory = new ChannelFactory<IIntegracaoNuvem>(myBinding, myEndpoint);

                client = myChannelFactory.CreateChannel();
                client.SubirDados();
                ((ICommunicationObject)client).Close();
            }
            catch(Exception ex)
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }

                GerarLog(ex);
            }
        }

        private void FecharHosts()
        {
            if (hostNuvem != null)
            {
                hostNuvem.Close();
                hostNuvem = null;
            }

            if (hostLocal != null)
            {
                hostLocal.Close();
                hostLocal = null;
            }
        }

        private void GerarLog(Exception ex)
        {
            using (StreamWriter vWriter = new StreamWriter(@"C:\ErroServicoTransmissor.txt", true))
            {
                vWriter.WriteLine("Erro " + ex.Message + "\n\n" + ex.StackTrace);
            }
        }
    }
}
