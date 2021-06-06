import os
import sys
import sqlite3
from sqlite3 import Error
from datetime import datetime

# ____________________________________________________________ #
### As funções abaixo são funções ligadas às operações na BD ###
# ____________________________________________________________ #

'''
    Esta é uma função indispensável
    Cria uma instância da conexão à BD
'''
def conectar(caminho):
    
    conexao = None

    try:
        conexao = sqlite3.connect("Horarios.db")
        return conexao

    except Error as e:
        print(e)

    return conexao

# Função para criação da tabela na BD
def criarTabelas(conexao):
    
    sql_criar_tabela_aulas = '''CREATE TABLE IF NOT EXISTS aulas(
                                    nr integer PRIMARY KEY,
                                    id integer NOT NULL,
                                    aula text NOT NULL,
                                    dia integer NOT NULL,
                                    hora integer NOT NULL,
                                    presenca text
                                );'''

    sql_criar_tabela_exames = '''CREATE TABLE IF NOT EXISTS exames(
                                    nr integer PRIMARY KEY,
                                    id integer NOT NULL,
                                    cadeira text NOT NULL,
                                    dia integer NOT NULL
                                );'''                        

    try:
        c = conexao.cursor()
        c.execute(sql_criar_tabela_aulas)
        c.execute(sql_criar_tabela_exames)

    except Error as e:
        print(e)

# Função para inserção dos valores na BD
def adicionarAula(conexao, aula):
    
    sql = ''' INSERT INTO aulas(id, aula, dia, hora, presenca)
                VALUES(?, ?, ?, ?, ?) '''
    c = conexao.cursor()
    c.execute(sql, aula)
    conexao.commit()
    return c.lastrowid

def adicionarExame(conexao, exame):
    
    sql = ''' INSERT INTO exames(id, cadeira, dia)
                VALUES(?, ?, ?) '''
    c = conexao.cursor()
    c.execute(sql, exame)
    conexao.commit()
    return c.lastrowid

# Função para atribuição dos valores predefinidos
def iniciarDados(conexao):

    criarTabelas(conexao)

    aulas = [
        (3387563893, "Electrotecnia Teórica", 0, 13, "https://bit.ly/3cQ2A8D"),
        (3387563893, "Análise Matemática II", 0, 15, "-"),
        (7068575499, "Programação II", 0, 17, "https://bit.ly/2Q1HjzS"),
        (2348432941, "Sistemas Operativos", 1, 13, "https://tinyurl.com/FP-SO-2021"),
        (3043530187, "Análise Matemática II", 1, 15, "-"),
        (3043530187, "Matemática Discreta", 1, 17, "https://bit.ly/2PAuqwD"),
        (8235178394, "Sistemas Operativos", 2, 11, "https://tinyurl.com/FP-SO-2021"),
        (3711443715, "Electrotecnia Teórica", 2, 13, "https://bit.ly/3cQ2A8D"),
        (8235178394, "Programação II", 2, 15, "https://bit.ly/2Q1HjzS"),
        (6363897612, "Análise Matemática II", 3, 13, "-"),
        (7068575499, "Electrônica Analógica", 3, 15, "https://bit.ly/3moS8I7"),
        (9728863709, "Matemática Discreta", 3, 17, "https://bit.ly/2PAuqwD"),
        (3265261727, "Electrônica Analógica", 4, 11, "https://bit.ly/3cQ46r0"),
        (3265261727, "Electrotecnia Teórica", 4, 13, "https://bit.ly/2Q1HjzS"),
    ]

    exames = [
        (2720416423, "Electrotecnia Teórica", 7),
        (3043530187, "Matemática Discreta", 8),
        (9994156338, "Programação II", 10),
        (6363897612, "Análise Matemática II", 11),
        (6159608597, "Electrónica Analógica", 15),
        (7068575499, "Sistemas Operativos", 17)
    ]
    
    for i in range(len(aulas)):
        adicionarAula(conexao, aulas[i])

    for i in range(len(exames)):
        adicionarExame(conexao, exames[i])

# Esta função retorna a aula decorrendo na hora selecionada
def selecionarAulas(conexao, hora, dia):

    data = (str(dia), str(hora))

    sql = """ SELECT id FROM aulas WHERE dia = ? AND hora = ?"""

    c = conexao.cursor()
    c.execute(sql, data)

    rows = c.fetchall()

    if len(rows) == 0:
        print("Nenhuma aula decorre no momento.")
        return 0
    else:
        return int(rows[0][0])

# Esta função retorna o exame decorrendo na hora selecionada
def selecionarExame(conexao, dia):

    sql = """ SELECT id FROM exames WHERE dia = ?"""

    c = conexao.cursor()
    c.execute(sql, str(dia))

    rows = c.fetchall()

    if len(rows) == 0:
        print("Nenhum exame decorre no momento.")
        return 0
    else:
        return int(rows[0][0])

# Esta função retorna a lista de presenca na hora selecionada
def selecionarPresenca(conexao, hora, dia):

    data = (dia, hora)

    sql = """ SELECT presenca FROM aulas WHERE dia = ? AND hora = ?"""

    c = conexao.cursor()
    c.execute(sql, data)

    rows = c.fetchall()

    if len(rows) == 0:
        print("Nenhuma aula decorre no momento.")
        return "."
    else:
        return str(rows[0][0])

# Esta função verifica a existência de qualquer dado na BD
def temDados(conexao):

    rows = None
    try:
        sql = """ SELECT * FROM aulas"""

        c = conexao.cursor()
        c.execute(sql)

        rows = c.fetchall()
        
        if len(rows) != 0:
            return True
        else:
            return False

    except Error as e:
        print(e)
    
    return False

# Esta função apaga todos os dados contidos na BD
def apagarTudo(conexao):

    sql = """DELETE FROM aulas """

    c = conexao.cursor()
    c.execute(sql)

# ____________________________________________________________ #
### As funções abaixo estão ligadas à gestão do tempo e data ###
# ____________________________________________________________ #

# Esta função encontra a hora actual
def horaActual():
    agora = datetime.now()
    hora = int(agora.strftime("%H"))
    minuto = agora.strftime("%M")
    if hora%2 == 0:
        hora -= 1
    elif int(minuto) <= 10:
        hora -= 2
    
    return hora

# Esta função encontra o dia actual
def diaActual():
    agora = datetime.now()
    dia = agora.today().weekday()
    return dia

def dia():
    return datetime.now().day

# __________________________________ #
### Abaixo as funcs que usam o cmd ###
# __________________________________ #

# Func que executa um comando dado
def executarComando(comando):
    os.system(comando)

# Func que abre o ID actual
def abrirAula(id):
    if id != 0:
        comando = "xdg-open https://zoom.us/j/" + str(id)
        print("Abrindo a aula...")
        executarComando(comando)
    else:
        print("ID invalido!")

# Func que abre lista de presenca
def abrirPresenca(presenca):
    if presenca == "-":
        print("Esta aula não tem lista de presença.")
    elif presenca != ".":
        comando = "xdg-open " + str(presenca)
        print("Abrindo a lista de presenças...")
        executarComando(comando)

# ______________________________________________________________________ #
### Abaixo se encontra a chamada dos métodos para a execução do script ###
# ______________________________________________________________________ #

if __name__ == "__main__":

    conexao = conectar("")

    if len(sys.argv) == 1:
        username = os.getlogin()
        desktop = "/home/" + username + "/Desktop/"
        cwd = os.getcwd()
        commands = [
            'echo cd "' + cwd + '">>"' + desktop + 'aula.sh"',
            'echo python3 id_finder.py a>>"' + desktop + 'aula.sh"',
            'echo cd "' + cwd + '">>"' + desktop + 'presenca.sh"',
            'echo python3 id_finder.py p>>"' + desktop + 'presenca.sh"',
            'echo cd "' + cwd + '">>"' + desktop + 'exame.sh"',
            'echo python3 id_finder.py e>>"' + desktop + 'exame.sh"'
            ]
        for command in commands:
            executarComando(command)

    elif len(sys.argv) == 2:
        if sys.argv[1] == "a":
            abrirAula(selecionarAulas(conexao, horaActual(), diaActual()))
        elif sys.argv[1] == "p":
            abrirPresenca(selecionarPresenca(conexao, horaActual(), diaActual()))
        elif sys.argv[1] == "e":
            abrirAula(selecionarExame(conexao, dia()))
        else:
            print("Erro. Regra: python3 id_finder.py [argumento]\n" +
                    "'a' para abrir a aula actual\n" + 
                    "'p' para abrir a lista de presenças")

    elif len(sys.argv) == 3:
        if sys.argv[1] == "o":
            try:
                if type(int(sys.argv[2])) == int:
                    abrirAula(int(sys.argv[2]))
            except:
                print("Erro. O ID deve ser composto de digitos apenas.")
        else:
            print("Erro. Regra: python3 id_finder.py o [ID]")
        
    else:
        print("Inválido. Deve ter 2 argumentos no máximo")

    conexao = conectar("")

    if not temDados(conexao):
        iniciarDados(conexao)

    conexao.close()
