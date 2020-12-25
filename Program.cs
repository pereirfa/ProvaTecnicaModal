using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading;


namespace ConsoleFileView
{
    class Program
    {

        public class BufferTmp
        {
            public int posicao { get; set; }
            public string linha { get; set; }
        }

        public static string arquivo ;
        public static int totalinhas;
        private const int BufferSize = 1024;
        public static int qtdTake = 0;
        public static int posInicioBuffer = 0;
        public static int posFimBuffer = 0;
        public static int posIniLinha = 0;
        public static int posFimLinha = 10;

        private const char CR = '\r';
        private const char LF = '\n';
        private const char NULL = (char)0;
        private static bool inwork = false;

        #region public class ConsoleSpiner
        public class ConsoleSpiner
        {
            int counter;
            public ConsoleSpiner()
            {
                counter = 0;
            }
            public void Turn()
            {
                counter++;
                switch (counter % 4)
                {
                    case 0: Console.Write("|"); break;
                    case 1: Console.Write("\\"); break;
                    case 2: Console.Write("-"); break;
                    case 3: Console.Write("/"); break;
                }
                Thread.Sleep(100);
                Console.SetCursorPosition((Console.CursorLeft == 0 ? 1 : Console.CursorLeft) - 1, Console.CursorTop);
            }
        }
        #endregion

        #region public static int ContarLinhaStream(Stream stream)
        public static int ContarLinhaStream(Stream stream)
        {
            int linhas = 0;

            var buffer = new byte[1024 * 1024];
            const int bytes = 4;
            var fimdeLinha = NULL;
            var caractere = NULL;

            int bytesX;
            while ((bytesX = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                var i = 0;

                for (; i <= bytesX - bytes; i += bytes)
                {
                    caractere = (char)buffer[i];

                    if (fimdeLinha != NULL)
                    {
                        if (caractere == fimdeLinha)
                        {
                            linhas++;
                        }

                        caractere = (char)buffer[i + 1];
                        if (caractere == fimdeLinha)
                        {
                            linhas++;
                        }

                        caractere = (char)buffer[i + 2];
                        if (caractere == fimdeLinha)
                        {
                            linhas++;
                        }

                        caractere = (char)buffer[i + 3];
                        if (caractere == fimdeLinha)
                        {
                            linhas++;
                        }
                    }
                    else
                    {
                        if (caractere == LF || caractere == CR)
                        {
                            fimdeLinha = caractere;
                            linhas++;
                        }
                        i -= bytes - 1;
                    }
                }

                for (; i < bytesX; i++)
                {
                    caractere = (char)buffer[i];

                    if (fimdeLinha != NULL)
                    {
                        if (caractere == fimdeLinha)
                        {
                            linhas++;
                        }
                    }
                    else
                    {
                        if (caractere == LF || caractere == CR)
                        {
                            fimdeLinha = caractere;
                            linhas++;
                        }
                    }
                }
            }

            if (caractere != LF && caractere != CR && caractere != NULL)
            {
                linhas++;
            }
            return linhas;
        }
        #endregion

        #region  static void RunTaskAsync()
        static void RunTaskAsync()
        {

            FileStream fs = new FileStream(arquivo, FileMode.Open, FileAccess.Read);
            totalinhas = ContarLinhaStream(fs);
            inwork = false;
            Console.SetCursorPosition((Console.CursorLeft == 0 ? 1 : Console.CursorLeft) - 1, Console.CursorTop);
        }
        #endregion

        #region static void Main(string[] args)
        static void Main(string[] args)
        {
            var listbuffer = new List<BufferTmp>();

            Console.WriteLine("▓███████████████████████████████████████████████████████████████▓");
            Console.WriteLine("▒         Prova Técnica : Visualizador de arquivos:             ▒");
            Console.WriteLine("▒               Nome: Fábio Sarmento Pereira                    ▒");
            Console.WriteLine("▒███████████████████████████████████████████████████████████████▒");
            Console.WriteLine("\r\n");

            bool showMenu = true;
            while (showMenu)
            {
               showMenu = MenuPrincipal(listbuffer);
            }
        }
        #endregion

        #region  private static bool MenuPrincipal(List<BufferTmp> listbuffer)
        private static bool MenuPrincipal(List<BufferTmp> listbuffer)
        {
            //Console.Clear();
            Console.WriteLine("Digite a opção desejada:");
            Console.WriteLine("1) Importar arquivo");
            Console.WriteLine("2) Arrow Down ↓");  //Percorrer uma linha para baixo
            Console.WriteLine("3) Arrow Up ↑");    //Percorrer uma linha para cima  
            Console.WriteLine("4) Page Down");     //Percorrer 11 linhas para baixo 
            Console.WriteLine("5) Page Up");       //Percorrer 11 linhas para baixo  
            Console.WriteLine("6) Digitar Linha");
            Console.WriteLine("7) Sair");
            Console.Write("\r\nDigite uma opção: ");

            switch (Console.ReadLine())
            {
                case "1":
                    ImportarArquivo(listbuffer);
                    return true;
                case "2":
                    BuscarLinhas(listbuffer , 2 );
                    return true;
                case "3":
                    BuscarLinhas(listbuffer, 3 );
                    return true;
                case "4":
                    BuscarLinhas(listbuffer, 4);
                    return true;
                case "5":
                    BuscarLinhas(listbuffer, 5);
                    return true;
                case "6":
                    BuscarLinhas(listbuffer, 6);
                    return true;
                case "7":
                    return false;
                default:
                    Console.WriteLine("Opção inválida!");
                    Console.WriteLine("\r\n");
                    Console.WriteLine("\r\n");
                    return true;
            }
        }
        #endregion

        #region private static bool ImportarArquivo(List<BufferTmp> listbuffer)
        private static bool ImportarArquivo(List<BufferTmp> listbuffer)
        {
            DateTime tempoini;
            DateTime tempofim;
            
            Console.WriteLine("Digite o diretorio do arquivo para importação, e pressione Enter");
            arquivo = Convert.ToString(Console.ReadLine());

            //Passo 1 - Carregar o total de linhas 
            try
            {
                var fileStream = File.OpenRead(arquivo);

                Console.WriteLine("\r\n"); 
                Console.WriteLine("\r\n");

                Console.WriteLine("▓███████████████████████████████████████████████████████████████▓");
                Console.WriteLine("▒              Iniciando contagem de linhas                     ▓");
                Console.WriteLine("▓███████████████████████████████████████████████████████████████▓");

                tempoini = DateTime.Now;

                Thread th = new Thread(RunTaskAsync);
                ConsoleSpiner sp = new ConsoleSpiner();
                inwork = true;
                th.Start();
                while (inwork)
                {
                    sp.Turn();
                }

                tempofim = DateTime.Now;

                posInicioBuffer = 0;
                posFimBuffer = 0;
                posIniLinha = 0;
                posFimLinha = 10;

                CarregarBuffer(listbuffer, 0, 100);
                Console.WriteLine("\r\n");
                Console.WriteLine("▓███████████████████████████████████████████████████████████████▓");
                Console.WriteLine("▒Nome arquivo: {0}                       ▒", arquivo);
                Console.WriteLine("▒        Quantidade Linhas: {0}                            ▓", totalinhas);
                Console.WriteLine("▒        Tempo Total: {0}                          ▓", ( tempofim - tempoini ) );
                Console.WriteLine("▓███████████████████████████████████████████████████████████████▓");


                Console.WriteLine("\r\n");
                Console.WriteLine("\r\n");
                return true;
            }
            catch (Exception ) 
            {
                Console.WriteLine("\r\n");
                Console.WriteLine("▓███████████████████████████████████████████████████████████████▓");
                Console.WriteLine("▒                   Arquivo Inválido                            ▓");
                Console.WriteLine("▓███████████████████████████████████████████████████████████████▓");
                Console.WriteLine("\r\n");
                Console.WriteLine("\r\n");
                return true;
            }

        }
        #endregion

        #region private static IEnumerable<BufferTmp> CarregarBuffer(List<BufferTmp> listbuffer , int qtdLinhasIni , int qtdLinhasFim )
        private static IEnumerable<BufferTmp> CarregarBuffer(List<BufferTmp> listbuffer , int qtdLinhasIni , int qtdLinhasFim )
        {
            int cont = 0;

            if ( listbuffer.Count() == 0  || (qtdLinhasIni < posInicioBuffer || qtdLinhasFim > posFimBuffer )) 
            {
                listbuffer.Clear();

                posInicioBuffer = qtdLinhasIni;
                
                if ((qtdLinhasIni + 100 ) > totalinhas )
                   posFimBuffer = totalinhas;
                else
                   posFimBuffer = qtdLinhasIni + 100 ;

                qtdTake = posFimBuffer - posInicioBuffer ;

                cont = posInicioBuffer   ;
                IEnumerable<string> list = File.ReadLines(arquivo).Skip(qtdLinhasIni).Take(qtdTake).ToList();
                foreach (var item in list)
                {
                    listbuffer.Add(new BufferTmp
                    {
                        posicao = cont,
                        linha = item
                    });
                    cont += 1;
                }
            }

            return listbuffer ; 
        }
        #endregion

        #region private static bool BuscarLinhas(List<BufferTmp> listbuffer , int codOpcao )
        private static bool BuscarLinhas(List<BufferTmp> listbuffer , int codOpcao )
        {
            string opcao ="";
            int numLine = 0;

            qtdTake = 11;

            if (listbuffer.Count() == 0)
            {

                Console.WriteLine("\r\n");
                Console.WriteLine("▓███████████████████████████████████████████████████████████████▓");
                Console.WriteLine("▒   É necessário importar arquivo antes de selecionar opção!    ▓");
                Console.WriteLine("▓███████████████████████████████████████████████████████████████▓");
                Console.WriteLine("\r\n");
                Console.WriteLine("\r\n");
                return true;
            }


            if (codOpcao == 2)
            {
                opcao = "Arrow Down ↓";

                if (posIniLinha + 1 < totalinhas)
                    posIniLinha += 1;
                else
                    posIniLinha = totalinhas;

                if (posFimLinha + 1 < totalinhas)
                    posFimLinha += 1;
                else
                    posFimLinha = totalinhas ;
            }

            if (codOpcao == 3)
            {
                opcao = "Arrow Up ↑";

                if (posIniLinha - 1 < 0 )
                    posIniLinha = 1;
                else
                    posIniLinha -= 1 ;

                if (posFimLinha -1 < 0 )
                    posFimLinha = 1;
                else
                    posFimLinha -= 1;
            }

            if (codOpcao == 4)
            {
                opcao = "Page Down";

                if (posFimLinha + 1 > totalinhas )
                    posIniLinha = posFimLinha ;
                else
                    posIniLinha = posFimLinha + 1 ;

                if (posIniLinha + 11 > totalinhas )
                    posFimLinha = totalinhas;
                else
                    posFimLinha = posIniLinha + 11 ;
            }

            if (codOpcao == 5)
            {
                opcao = "Page UP";

                if (posIniLinha -1 < 0 )
                    posFimLinha = posIniLinha ;
                else
                    posFimLinha = posIniLinha -1 ;

                if (posIniLinha - 11 < 0)
                    posIniLinha = 0 ;
                else
                    posIniLinha -= 11 ;
            }

            if (codOpcao == 6)
            {
                opcao = "Redefinição Linha";
                Console.WriteLine("Digite o numero da linha para referência, e pressione Enter");
                numLine = Convert.ToInt32(Console.ReadLine());

                if (numLine > totalinhas)
                {
                    Console.WriteLine("\r\n");
                    Console.WriteLine("▓███████████████████████████████████████████████████████████████▓");
                    Console.WriteLine("▒                Posição de linha inválida!                     ▓");
                    Console.WriteLine("▓███████████████████████████████████████████████████████████████▓");
                    Console.WriteLine("\r\n");
                    Console.WriteLine("\r\n");
                    return true;
                }


                if ( numLine > 5 )
                {
                    posIniLinha = numLine - 5 ;

                    if ( (numLine + 5) < totalinhas)
                        posFimLinha = numLine + 5;
                    else
                        posFimLinha = totalinhas;
                }
                else
                {
                    posIniLinha = 1;

                    if ((numLine + 5) < totalinhas)
                        posFimLinha = numLine + 5;
                    else
                        posFimLinha = totalinhas;
                }   
            }

            Console.WriteLine("\r\n");
            
            Console.WriteLine("▓███████████████████████████████████████████████████████████████▓");
            Console.WriteLine("▓                                                               ▓");
            Console.WriteLine("▓     OPÇÃO :{0}                                        ▓ ", opcao);
            Console.WriteLine("▓                                                               ▓ ");
            Console.WriteLine("▓███████████████████████████████████████████████████████████████▓");

            CarregarBuffer(listbuffer, posIniLinha-1, posFimLinha-1);

            //IEnumerable<string> list = File.ReadLines(arquivo).Skip(qtdSkip).Take(qtdTake).ToList();

            Console.WriteLine("\r\n");
            Console.WriteLine("\r\n");

            foreach (var item in listbuffer.Where(x=> x.posicao >= (posIniLinha-1) && x.posicao <= (posFimLinha-1) ))
            {
               Console.WriteLine("{0}", item.linha);
            }

            Console.WriteLine("\r\n");
            Console.WriteLine("\r\n");
            Console.WriteLine("▓███████████████████████████████████████████████████████████████▓");
            Console.WriteLine("▓                                                               ▓");
            Console.WriteLine("                        FIM   EXECUÇÃO                           "); 
            Console.WriteLine("▓                                                               ▓");
            Console.WriteLine("▓███████████████████████████████████████████████████████████████▓");

            Console.WriteLine("\r\n");
            Console.WriteLine("\r\n");
            return true;
        }
    }
    #endregion
}
