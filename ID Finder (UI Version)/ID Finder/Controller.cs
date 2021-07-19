using System;
using System.Collections;

namespace ID_Finder
{
    class Controller
    {

        public static ArrayList listar(string turma)
        {
            var connection = DatabaseConnector.connection();
            connection.Open();

            ArrayList lista = new ArrayList();

            var command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM $table";
            command.Parameters.AddWithValue("$table", turma);

            using( var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    lista.Add(new Model(reader.GetInt64(0),
                        reader.GetString(1), reader.GetInt32(2), reader.GetInt64(3),
                        reader.GetInt64(4), reader.GetString(5)));
                }
            }

            connection.Close();

            return lista;

        }

        public static void inicializarDados()
        {
            /// Preenchimento da lista I21
            ArrayList listaI21 = new ArrayList();
            listaI21.Add(new Model(2099200684, "Electrónica de Rádio (T)", 0,
                        1320, 1510, "-"));
            listaI21.Add(new Model(2099200684, " Teoria de Circuitos (T)", 0,
                        1520, 1710, "-"));
            listaI21.Add(new Model(2099200684, "Electrónica Digital (T)", 0,
                        1720, 1910, "-"));
            listaI21.Add(new Model(7740042926, "Análise Númerica (P)", 1,
                        1520, 1710, "-"));
            listaI21.Add(new Model(7740042926, "Análise Matemática III (P)", 1,
                        1720, 1910, "-"));
            listaI21.Add(new Model(8235178394, "Programação III (T)", 2,
                        720, 910, "-"));
            listaI21.Add(new Model(6363897612, "Electrónica Digita (P)", 2,
                        920, 1110, "-"));
            listaI21.Add(new Model(3710025929, "Análise Matemática III (T)", 2,
                        1120, 1310, "-"));
            listaI21.Add(new Model(2235988220, "Análise Matemática III (P)", 3,
                        1520, 1710, "-"));
            listaI21.Add(new Model(2099200684, "Análise Númerica (T)", 3,
                        1720, 1910, "-"));
            listaI21.Add(new Model(2348432941, "Programação III (P)", 4,
                        720, 910, "-"));
            listaI21.Add(new Model(2348432941, "Electrónica Digital (P)", 4,
                        920, 1110, "-"));
            listaI21.Add(new Model(6363897612, "Electrónica de Rádio (P)", 5,
                        720, 910, "-"));
            listaI21.Add(new Model(6363897612, " Teoria de Circuitos (P)", 5,
                        920, 1110, "-"));

            /// Preenchimento da lista I22
            ArrayList listaI22 = new ArrayList();
            listaI22.Add(new Model(2099200684, "Electrónica de Rádio (T)", 0,
                        1320, 1510, "-"));
            listaI22.Add(new Model(2099200684, " Teoria de Circuitos (T)", 0,
                        1520, 1710, "-"));
            listaI22.Add(new Model(2099200684, "Electrónica Digital (T)", 0,
                        1720, 1910, "-"));
            listaI22.Add(new Model(8511059503, "Análise Númerica (P)", 1,
                        1320, 1510, "-"));
            listaI22.Add(new Model(8511059503, "Análise Matemática III (P)", 1,
                        1520, 1710, "-"));
            listaI22.Add(new Model(2348432941, "Programação III (T)", 1,
                        1720, 1910, "-"));
            listaI22.Add(new Model(8235178394, "Teoria de Circuitos (P)", 2,
                        920, 1110, "-"));
            listaI22.Add(new Model(3710025929, "Análise Matemática III (T)", 2,
                        1120, 1310, "-"));
            listaI22.Add(new Model(6363897612, "Electrónica de Rádio (P)", 2,
                        1320, 1510, "-"));
            listaI22.Add(new Model(2099200684, "Electrónica Digital (P)", 3,
                        1520, 1710, "-"));
            listaI22.Add(new Model(2099200684, "Análise Númerica (T)", 3,
                        1720, 1910, "-"));
            listaI22.Add(new Model(2348432941, "Análise Matemática III (P)", 4,
                        1320, 1510, "-"));
            listaI22.Add(new Model(2348432941, "Electrónica Digital (P)", 4,
                        1520, 1710, "-"));
            listaI22.Add(new Model(2348432941, "Programação III (P)", 4,
                        1720, 1910, "-"));

            /// Preenchimento da lista I23
            ArrayList listaI23 = new ArrayList();
            listaI23.Add(new Model(3710025929, "Análise Matemática III (P)", 0,
                        1320, 1510, "-"));
            listaI23.Add(new Model(3710025929, "Electrónica de Rádio (T)", 0,
                        1520, 1710, "-"));
            listaI23.Add(new Model(3710025929, "Teoria de Circuitos (T)", 0,
                        1720, 1910, "-"));
            listaI23.Add(new Model(6363897612, "Electrónica Digital (P)", 1,
                        1320, 1510, "-"));
            listaI23.Add(new Model(6363897612, "Análise Númerica (P)", 1,
                        1520, 1710, "-"));
            listaI23.Add(new Model(8235178394, "Programação III (T)", 1,
                        1720, 1910, "-"));
            listaI23.Add(new Model(6363897612, "Electrónica Digital (T)", 2,
                        720, 910, "-"));
            listaI23.Add(new Model(3710025929, "Análise Matemática III (T)", 2,
                        920, 1110, "-"));
            listaI23.Add(new Model(8680491980, "Electrónica Digital (P)", 3,
                        1320, 1510, "-"));
            listaI23.Add(new Model(8680491980, "Análise Númerica (T)", 3,
                        1520, 1710, "-"));
            listaI23.Add(new Model(8680491980, "Análise Matemática III (T)", 3,
                        1720, 1910, "-"));
            listaI23.Add(new Model(8680491980, "Electrónica Digital (P)", 4,
                        1320, 1510, "-"));
            listaI23.Add(new Model(8680491980, "Análise Númerica (T)", 4,
                        1520, 1710, "-"));
            listaI23.Add(new Model(8680491980, "Análise Matemática III (T)", 4,
                        1720, 1910, "-"));
            listaI23.Add(new Model(9994156338, "Teoria de Circuitos (P)", 5,
                        720, 910, "-"));
            listaI23.Add(new Model(9994156338, "Electrónica de Rádio (P)", 5,
                        920, 1110, "-"));
            listaI23.Add(new Model(2348432941, "Programação III (P)", 5,
                        1120, 1310, "-"));

            // Criacao das tabelas das turmas
            criarTabela("I21");
            criarTabela("I22");
            criarTabela("I23");

            // Insercao de dados
            int i = 0;
            foreach (Model model in listaI21)
            {
                inserir("I21", model, i++);
            }

            i = 0;
            foreach (Model model in listaI22)
            {
                inserir("I22", model, i++);
            }

            i = 0;
            foreach (Model model in listaI23)
            {
                inserir("I23", model, i++);
            }

        }

        private static async void inserir(String tabela, Model model, int nr)
        {
            using (var connection = DatabaseConnector.connection())
            {
                try
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"INSERT INTO " + tabela +
                                            @"(nr, id, aula, dia, inicio, fim, presenca)
                                            VALUES($nr,$id,$aula,$dia,$inicio,$fim,$presenca)";
                        command.Parameters.AddWithValue("$nr", nr);
                        command.Parameters.AddWithValue("$id", model.ID);
                        command.Parameters.AddWithValue("$aula", model.Aula);
                        command.Parameters.AddWithValue("$dia", model.Dia);
                        command.Parameters.AddWithValue("$inicio", model.Inicio);
                        command.Parameters.AddWithValue("$fim", model.Fim);
                        command.Parameters.AddWithValue("$presenca", model.Presenca);
                        await command.ExecuteNonQueryAsync();
                        Console.WriteLine("Sucesso: Dados inseridos com sucesso na tabela {0}", tabela);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro: {0}", e.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private static async void criarTabela(string tabela)
        {
            using (var connection = DatabaseConnector.connection())
            {
                try
                {
                    await connection.OpenAsync();

                    var command = connection.CreateCommand();

                    command.CommandText = @"CREATE TABLE IF NOT EXISTS " + tabela + @"(
                                        nr integer PRIMARY KEY,
                                        id integer NOT NULL,
                                        aula text NOT NULL,
                                        dia integer NOT NULL,
                                        inicio integer NOT NULL,
                                        fim integer NOT NULL,
                                        presenca text
                                    );";
                    await command.ExecuteNonQueryAsync();
                    Console.WriteLine("Sucesso: Tabela {0} criada", tabela);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro: {0}", e.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static ArrayList lista(string tabela)
        {
            ArrayList lista = new ArrayList();

            using(var connection = DatabaseConnector.connection())
            {
                connection.Open();
                using(var command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT * FROM " + tabela;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Model(reader.GetInt64(1),
                                reader.GetString(2), reader.GetInt16(3),
                                reader.GetInt64(4), reader.GetInt64(5),
                                reader.GetString(6)));
                        }
                    }
                }
            }

            return lista;
        }

        public static Model getAula(string tabela, int dia, long hora)
        {
            using (var connection = DatabaseConnector.connection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    try
                    {
                        command.CommandText = @"SELECT * FROM " + tabela + @" 
                            WHERE dia=$dia AND inicio<=$hora AND fim>=$hora";
                        command.Parameters.AddWithValue("$dia", dia);
                        command.Parameters.AddWithValue("$hora", hora);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                return new Model(reader.GetInt64(1),
                                    reader.GetString(2), reader.GetInt16(3),
                                    reader.GetInt64(4), reader.GetInt64(5),
                                    reader.GetString(6));
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Erro: {0}", e.Message);
                        return null;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

    }
}
