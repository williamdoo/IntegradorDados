# IntegradorDados
Exemplo de projeto de como criar e hospedar um Serviço WCF em um Serviço do Windows Gerenciado

### Inicializar o Serviço


Para Iniciar um serviço baixe o projeto e compile.
Na pasta degub vai ser criado um WCFServicoIntegradorDados.exe

Abre o CMD no Modo Administrador e execute os comandos

cd C:\Windows\Microsoft.NET\Framework\v4.0.30319

**nessa pasta está o InstallUtil.exe depedendo do .NetFramework pode estar em outro caminho**

Depois execute o comando
**InstallUtil "C:\CaminhoDoProjeto\bin\Debug\WCFServicoIntegradorDados.exe"**

Se tudo der certo vai mostrar a mensagem **Instalação transacionada concluida.**

Abre o Gerenciador de Serviço, procure o serviço Integrador de Dados e inicie, caso não estiver iniciado.

Será gerado um arquivo em C:\TesteServico.txt que fica setando a data e hora de 30 em 30 segundos.

Precisa ter o IIS instalado para inciar o serviço WCF

Após iniciado você poderá digita em seu navegador os links abaixo que você verá seu serviço WCF funcionado.

http://localhost:80/Integrador/Nuvem

http://localhost:80/Integrador/Local
