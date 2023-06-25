namespace DotNet.Instrastructure.Data.Repositories.SqlScripts.Turma
{
    public class TurmaScript
    {
        public const string GetAll = @"SELECT DISTINCT Id,
                                                    Turma as [Nome],
                                                    CursoId,
                                                    Ano,
                                                    Status
                                        From Turmas";

        public const string GetAllPaginated = @"SELECT DISTINCT
                                                                Id,
                                                                Turma as [Nome],
                                                                CursoId,
                                                                Ano,
                                                                Status,
																Total
                                                FROM (
                                                    SELECT *,
                                                        ROW_NUMBER() OVER (ORDER BY Id) AS RowNumber,
                                                        COUNT(*) OVER () AS Total
                                                    FROM Turmas
                                                ) AS SubQuery
                                                WHERE RowNumber BETWEEN @Page AND @PageSize";

        public const string GetById = @"SELECT Id, Turma as [Nome], CursoId, Ano, Status FROM Turmas WHERE Id = @Id";

        public const string GetDetails = @"SELECT
		                                        T.Id,
		                                        T.Turma as [Nome],
		                                        T.CursoId,
		                                        T.Ano,
		                                        T.Status,
		                                        A.Id,
		                                        A.Nome,
		                                        A.Usuario,
		                                        A.Status,
                                        CASE WHEN TotalAlunos IS NULL THEN 0 ELSE TotalAlunos END AS TotalAlunos
                                        FROM Turmas AS T
                                        LEFT JOIN AlunosTurmas AS ALT ON ALT.TurmaId = T.Id
                                        INNER JOIN Alunos AS A ON A.Id = ALT.AlunoId
                                        LEFT JOIN (
                                            SELECT TurmaId, COUNT(*) AS TotalAlunos
                                            FROM AlunosTurmas
                                            GROUP BY TurmaId
                                        ) AS CountTable ON CountTable.TurmaId = T.Id
                                        WHERE T.Id = @Id
                                        ORDER BY A.Id
                                        OFFSET (@PageNumber - 1) * @PageSize ROWS
                                        FETCH NEXT @PageSize ROWS ONLY;";

        public const string Post = @"INSERT INTO Turmas
                                        (Turma, CursoId, Ano)
                                    VALUES
                                        (@Nome, @CursoId, @Ano);
                                    SELECT CAST(SCOPE_IDENTITY() AS INT);";

        public const string Put = @"UPDATE Turmas SET Turma = @Nome, CursoId = @CursoId, Ano = @Ano, AlteradoEm = GETUTCDATE() WHERE Id = @Id";

        public const string PutStatus = @" UPDATE Turmas SET Status = @Status, AlteradoEm = GETUTCDATE() WHERE Id = @Id";

        public const string Delete = @"DELETE FROM Turmas WHERE Id = @Id";

        public const string ValidateTurmaName = @"SELECT COUNT(*) FROM Turmas WHERE Turma = @Nome AND Id <> @Id";
    }
}