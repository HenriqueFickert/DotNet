using Dapper;
using DotNet.Domain.Core.Interfaces.Repositories;
using DotNet.Domain.Core.Notification;
using DotNet.Domain.Entities;
using DotNet.Domain.Pagination;
using DotNet.Instrastructure.Data.Repositories.Base;
using DotNet.Instrastructure.Data.Repositories.SqlScripts.Aluno;
using DotNet.Instrastructure.Data.Repositories.SqlScripts.AlunoTurma;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace DotNet.Instrastructure.Data.Repositories
{
    public class AlunoRepository : RepositoryBase, IAlunoRepository
    {
        public AlunoRepository(IConfiguration configuration, INotifier notifier) : base(configuration, notifier)
        {
        }

        public async Task<IEnumerable<Aluno>> GetAll()
        {
            using (sqlConnection = new SqlConnection(_configuration.GetConnectionString("Connection")))
            {
                try
                {
                    return await sqlConnection.QueryAsync<Aluno>(AlunoScript.GetAll);
                }
                catch (Exception ex)
                {
                    RequisitionError(ex);
                    return null;
                }
            }
        }

        public async Task<PagedList<Aluno>> GetAllPaginated(int page, int size)
        {
            using (sqlConnection = new SqlConnection(_configuration.GetConnectionString("Connection")))
            {
                try
                {
                    IEnumerable<Tuple<Aluno, int>> results = await sqlConnection.QueryAsync<Aluno, int, Tuple<Aluno, int>>(AlunoScript.GetAllPaginated,
                                                                                                    (aluno, totalCount) => Tuple.Create(aluno, totalCount),
                                                                                                    new { Page = (page - 1) * size + 1, PageSize = size * page },
                                                                                                    splitOn: "Total");

                    IEnumerable<Aluno> data = results.Select(tuple => tuple.Item1);
                    int totalCount = results.FirstOrDefault()?.Item2 ?? 0;

                    return await Task.FromResult(PagedList<Aluno>.ToPagedList(data.ToList(), totalCount, page, size));
                }
                catch (Exception ex)
                {
                    RequisitionError(ex);
                    return null;
                }
            }
        }

        public async Task<Aluno> GetById(int id)
        {
            using (sqlConnection = new SqlConnection(_configuration.GetConnectionString("Connection")))
            {
                try
                {
                    Aluno aluno = await sqlConnection.QueryFirstOrDefaultAsync<Aluno>(AlunoScript.GetById, new { Id = id });

                    if (aluno == null)
                        AddNotification("Aluno não encontrado");

                    return aluno;
                }
                catch (Exception ex)
                {
                    RequisitionError(ex);
                    return null;
                }
            }
        }

        public async Task<Aluno> Post(Aluno aluno)
        {
            try
            {
                using (sqlConnection = new SqlConnection(_configuration.GetConnectionString("Connection")))
                {
                    await sqlConnection.OpenAsync();
                    using DbTransaction transaction = await sqlConnection.BeginTransactionAsync();
                    try
                    {
                        aluno.Id = await sqlConnection.ExecuteScalarAsync<int>(AlunoScript.Post, aluno, transaction);
                        transaction.Commit();

                        if (aluno.Id == 0)
                            AddNotification("Falha ao inserir o aluno.");
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        RequisitionError(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                RequisitionError(ex);
            }

            return aluno;
        }

        public async Task<Aluno> Put(Aluno aluno)
        {
            try
            {
                using (sqlConnection = new SqlConnection(_configuration.GetConnectionString("Connection")))
                {
                    await sqlConnection.OpenAsync();
                    using DbTransaction transaction = await sqlConnection.BeginTransactionAsync();
                    try
                    {
                        bool result = await sqlConnection.ExecuteAsync(AlunoScript.Put, aluno, transaction) > 0;
                        transaction.Commit();

                        if (!result)
                            AddNotification("Falha ao atualizar o aluno.");
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        RequisitionError(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                RequisitionError(ex);
            }

            return aluno;
        }

        public async Task<bool> PutStatus(Aluno aluno)
        {
            try
            {
                using (sqlConnection = new SqlConnection(_configuration.GetConnectionString("Connection")))
                {
                    await sqlConnection.OpenAsync();
                    using DbTransaction transaction = await sqlConnection.BeginTransactionAsync();
                    try
                    {
                        bool result = await sqlConnection.ExecuteAsync(AlunoScript.PutStatus, aluno, transaction) > 0;
                        transaction.Commit();

                        if (!result)
                        {
                            AddNotification("Falha ao atualizar o status do aluno.");
                            return false;
                        }

                        return true;
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        RequisitionError(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                RequisitionError(ex);
            }

            return false;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                using (sqlConnection = new SqlConnection(_configuration.GetConnectionString("Connection")))
                {
                    await sqlConnection.OpenAsync();
                    using DbTransaction transaction = await sqlConnection.BeginTransactionAsync();
                    try
                    {
                        bool result = await sqlConnection.ExecuteAsync(AlunoScript.Delete, new { Id = id }, transaction) > 0;
                        transaction.Commit();

                        if (!result)
                            AddNotification("Falha ao remover o aluno.");

                        return result;
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        RequisitionError(ex);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                RequisitionError(ex);
                return false;
            }
        }

        public async Task<bool> InsertAlunoTurma(int alunoId, int turmaId)
        {
            try
            {
                using (sqlConnection = new SqlConnection(_configuration.GetConnectionString("Connection")))
                {
                    await sqlConnection.OpenAsync();
                    using DbTransaction transaction = await sqlConnection.BeginTransactionAsync();
                    try
                    {
                        var parameters = new { AlunoId = alunoId, TurmaId = turmaId };

                        bool result = await sqlConnection.ExecuteScalarAsync<int>(AlunoTurmaScript.InsertAlunoTurma, parameters, transaction) > 0;
                        transaction.Commit();

                        if (!result)
                            AddNotification("Falha ao inserir o aluno a turma.");

                        return result;
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        RequisitionError(ex);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                RequisitionError(ex);
                return false;
            }
        }

        public async Task<bool> DeleteAlunoTurma(int alunoId, int turmaId)
        {
            try
            {
                using (sqlConnection = new SqlConnection(_configuration.GetConnectionString("Connection")))
                {
                    await sqlConnection.OpenAsync();
                    using DbTransaction transaction = await sqlConnection.BeginTransactionAsync();
                    try
                    {
                        var parameters = new { AlunoId = alunoId, TurmaId = turmaId };
                        bool result = await sqlConnection.ExecuteAsync(AlunoTurmaScript.Delete, parameters, transaction) > 0;
                        transaction.Commit();

                        if (!result)
                            AddNotification("Falha ao desassociar o aluno de turma.");

                        return result;
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        RequisitionError(ex);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                RequisitionError(ex);
                return false;
            }
        }

        public bool AlunoTurmaValidator(int alunoId, int turmaId)
        {
            using (sqlConnection = new SqlConnection(_configuration.GetConnectionString("Connection")))
            {
                try
                {
                    int valor = sqlConnection.ExecuteScalar<int>(AlunoTurmaScript.ValidateAlunoTurma, new { AlunoId = alunoId, TurmaId = turmaId });
                    bool result = valor > 0;
                    return result;
                }
                catch (Exception ex)
                {
                    RequisitionError(ex);
                    return true;
                }
            }
        }
    }
}