using System;
using MoviesApi.Models;
using System.Data.SqlClient;

namespace MoviesApi.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        //private readonly string connectionString = "Data Source=localhost\\SQLEXPRESS;" +
        //                                           "Initial Catalog=MotionPictures;Integrated Security=True;";
        private readonly string connectionString = "Server=127.0.0.1,1433;Database=MotionPictures;User Id=sa;" +
                                                   "Password=reallyStrongPwd123;MultipleActiveResultSets=True;";

        public Movie GetMovieById(int id)
        {
            Movie result = new Movie();
            string queryString = "SELECT * FROM MotionPictures WHERE ID = @ID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@ID", id);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        result = GetMovieFromReader(reader);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public bool MovieExists(int id)
        {
            bool exists = true;

            try
            {
                Movie movie = GetMovieById(id);

                if (movie.Name == null)
                {
                    exists = false;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return exists;
        }

        public List<Movie> GetMovies()
        {
           List<Movie> results = new List<Movie>();
           string queryString = "SELECT * from dbo.MotionPictures;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(queryString, connection);
                    SqlDataReader reader = command.ExecuteReader();
                                     
                    while (reader.Read())
                    {
                        Movie newMovie = GetMovieFromReader(reader);
                        results.Add(newMovie);
                    }                
                }
            }
            catch (Exception)
            {
                throw;
            }

            return results;
        }

        public bool InsertMovie(Movie movie)
        {
            bool inserted = true;
            string queryString = "INSERT INTO MotionPictures (Name, Description, [Release Year], AcademyAward, DirectorId) " +
                                 "VALUES (@Name, @Description, @ReleaseYear, @AcademyAward, @DirectorId);";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@Name", movie.Name);
                    command.Parameters.AddWithValue("@Description", movie.Description);
                    command.Parameters.AddWithValue("@ReleaseYear", movie.ReleaseYear);
                    command.Parameters.AddWithValue("@AcademyAward", movie.AcademyAward);
                    if (movie.DirectorId != null)
                    {
                        command.Parameters.AddWithValue("@DirectorId", movie.DirectorId);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@DirectorId", DBNull.Value);
                    }
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected != 1)
                    {
                        inserted = false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return inserted;
        }

        public Movie UpdateMovie(Movie movie)
        {
            string queryString = "UPDATE MotionPictures SET Name = @Name, Description = @Description, " +
                                 "[Release Year] = @ReleaseYear, AcademyAward = @AcademyAward, DirectorId = @DirectorId " +
                                 "WHERE ID = @ID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@ID", movie.ID);
                    command.Parameters.AddWithValue("@Name", movie.Name);
                    command.Parameters.AddWithValue("@Description", movie.Description);
                    command.Parameters.AddWithValue("@ReleaseYear", movie.ReleaseYear);
                    command.Parameters.AddWithValue("@AcademyAward", movie.AcademyAward);
                    if (movie.DirectorId != null)
                    {
                        command.Parameters.AddWithValue("@DirectorId", movie.DirectorId);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@DirectorId", DBNull.Value);
                    }
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected != 1)
                    {
                        throw new Exception("Error updating movie entry!");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return GetMovieById(movie.ID);
        }

        public bool DeleteMovie(int id)
        {
            bool deleted = true;
            string queryString = "DELETE FROM MotionPictures WHERE ID = @ID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@ID", id);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected != 1)
                    {
                        deleted = false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return deleted;
        }

        // Helper Method
        private Movie GetMovieFromReader(SqlDataReader reader)
        {
            Movie newMovie = new Movie();

            newMovie.ID = Convert.ToInt32(reader["ID"]);
            newMovie.Name = Convert.ToString(reader["Name"]);
            newMovie.Description = Convert.ToString(reader["Description"]);
            newMovie.ReleaseYear = Convert.ToInt32(reader["Release Year"]);
            newMovie.AcademyAward = Convert.ToBoolean(reader["AcademyAward"]);
            newMovie.DirectorId = reader["DirectorId"].GetType() == typeof(DBNull) ? null : Convert.ToInt32(reader["DirectorId"]);

            return newMovie;
        }
    }
}
