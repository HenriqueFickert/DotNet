namespace DotNet.Instrastructure.Data.Repositories.SqlScripts.AlunoTurma
{
    public class AlunoTurmaScript
    {
        public const string InsertAlunoTurma = @"INSERT INTO AlunosTurmas (AlunoId, TurmaId) VALUES (@AlunoId, @TurmaId); SELECT CAST(SCOPE_IDENTITY() AS INT);";

        public const string Delete = @"DELETE FROM AlunosTurmas WHERE AlunoId = @AlunoId AND TurmaId = @TurmaId";

        public const string ValidateAlunoTurma = @"SELECT COUNT(*) FROM AlunosTurmas WHERE (AlunoId = @AlunoId AND TurmaId = @TurmaId);";
    }
}