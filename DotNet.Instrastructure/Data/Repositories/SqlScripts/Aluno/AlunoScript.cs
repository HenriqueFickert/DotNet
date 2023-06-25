namespace DotNet.Instrastructure.Data.Repositories.SqlScripts.Aluno
{
    public class AlunoScript
    {
        public const string GetAll = @"SELECT DISTINCT
                                                Id,
                                                Nome,
                                                Usuario,
                                                Senha,
                                                Status,
                                                CriadoEm,
                                                AlteradoEm
                                        From Alunos";

        public const string GetAllPaginated = @"SELECT DISTINCT
                                                                Id,
                                                                Nome,
                                                                Usuario,
                                                                Senha,
                                                                Status,
																Total
                                                FROM (
                                                    SELECT *,
                                                        ROW_NUMBER() OVER (ORDER BY Id) AS RowNumber,
                                                        COUNT(*) OVER () AS Total
                                                    FROM Alunos
                                                ) AS SubQuery
                                                WHERE RowNumber BETWEEN @Page AND @PageSize";

        public const string GetById = @"SELECT Id, Nome, Usuario, Senha, Status FROM Alunos WHERE Id = @Id";

        public const string Post = @"INSERT INTO Alunos
                                        (Nome, Usuario, Senha)
                                    VALUES
                                        (@Nome, @Usuario, @Senha);
                                    SELECT CAST(SCOPE_IDENTITY() AS INT);";

        public const string Put = @"UPDATE Alunos SET Nome = @Nome, Usuario = @Usuario, Senha = @Senha, AlteradoEm = GETUTCDATE() WHERE Id = @Id";

        public const string PutStatus = @" UPDATE Alunos SET Status = @Status, AlteradoEm = GETUTCDATE() WHERE Id = @Id";

        public const string Delete = @"DELETE FROM Alunos WHERE Id = @Id";
    }
}