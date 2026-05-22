using System;

ExecuteNonQuery();

Console.WriteLine("Книга успешно добавлена!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }

            Pause();
        }

        // ПОИСК КНИГИ
        static void FindBook()
{
    try
    {
        Console.Write("Введите название книги: ");
        string title = Console.ReadLine();

        using (NpgsqlConnection connection =
            new NpgsqlConnection(connectionString))
        {
            connection.Open();

            string query =
                "SELECT * FROM Books WHERE Title ILIKE @title";

            NpgsqlCommand command =
                new NpgsqlCommand(query, connection);

            command.Parameters.AddWithValue("@title", "%" + title + "%");

            NpgsqlDataReader reader = command.ExecuteReader();

            Console.WriteLine("\nРЕЗУЛЬТАТ ПОИСКА:\n");

            while (reader.Read())
            {
                Console.WriteLine(
                    $"ID: {reader["Id"]} | " +
                    $"Название: {reader["Title"]} | " +
                    $"Автор: {reader["Author"]} | " +
                    $"Год: {reader["PublishYear"]} | " +
                    $"Количество: {reader["Quantity"]}"
                );
            }

            reader.Close();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Ошибка: " + ex.Message);
    }

    Pause();
}

// УДАЛЕНИЕ КНИГИ
static void DeleteBook()
{
    try
    {
        Console.Write("Введите ID книги: ");
        int id = int.Parse(Console.ReadLine());

        using (NpgsqlConnection connection =
            new NpgsqlConnection(connectionString))
        {
            connection.Open();

            string query =
                "DELETE FROM Books WHERE Id = @id";

            NpgsqlCommand command =
                new NpgsqlCommand(query, connection);

            command.Parameters.AddWithValue("@id", id);

            int rows = command.ExecuteNonQuery();

            if (rows > 0)
            {
                Console.WriteLine("Книга удалена!");
            }
            else
            {
                Console.WriteLine("Книга не найдена!");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Ошибка: " + ex.Message);
    }

    Pause();
}

static void Pause()
{
    Console.WriteLine("\nНажмите Enter...");
    Console.ReadLine();
}
    }
}
