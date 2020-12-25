# # Visualizador Arquivo Texto

Candidato: **Fábio Sarmento Pereira**

Prova Tecnica : **Analista Desenvolvedor .NET**. 


# # Objetivo

    O objetivo do programa será a visualização de arquivos grandes (maior que 20GBs) via linha de comando, 
a carga dos dados do arquivo deverá ser dinâmica para evitar um consumo de memória excessivo. O programa deverá receber o nome do arquivo a ser visualizado como parâmetro da linha de comando.
O programa apresentará 11 linhas de cada vez ao usuário, podendo receber os seguintes comandos:


# # Opções

**Arrow Down**: O sistema deverá apresentar as próximas 11 linhas do arquivo, iniciando na linha subseqüente a linha inicial que estava apresentando anteriormente. 
                Por exemplo: se o usuário estiver visualizando as linhas de 25 a 35, ao pressionar esta tecla, o sistema deverá apresentar as linhas de 26 a 36.

**Arrow UP**: O sistema deverá apresentar as 11 linhas anteriores do arquivo, iniciando na linha anterior a linha inicial que estava apresentando anteriormente. 
              Por exemplo: se o usuário estiver visualizando as linhas de 25 a 35, ao pressionar esta tecla, o sistema deverá apresentar as linhas de 24 a 34.

**Page Down**: O sistema deverá apresentar as próximas 11 linhas do arquivo, iniciando na linha posterior a linha final que estava apresentando anteriormente. 
               Por exemplo: se o usuário estiver visualizando as linhas de 25 a 35, ao pressionar esta tecla, o sistema deverá apresentar as linhas de 36 a 46.

**Page UP**: O sistema deverá apresentar as 11 linhas anteriores do arquivo, finalizando na linha anterior a linha final que estava apresentando anteriormente. 
             Por exemplo: se o usuário estiver visualizando as linhas de 25 a 35, ao pressionar esta tecla, o sistema deverá apresentar as linhas de 14 a 24.

**L**: O sistema aguardará que o usuário insira o numero da linha do arquivo que se deseja visualizar, neste caso, deverá ser apresentado as 5 linhas anteriores e as 5 posteriores a linha que usuário indicou. 
       Por exemplo: se o usuário escolher a linha 30, o sistema deverá apresentar as linhas de 25 a 35.
	   O sistema deverá também atender aos seguintes requisitos:


# # Observação

  Manter sempre um buffer de 100 linhas para agilizar a busca. Caso o usuário navegue para uma linha fora do intervalo do buffer, 
  o sistema deverá abandonar o buffer mantido anteriormente e recarregá-lo dentro do intervalo pedido.







   
