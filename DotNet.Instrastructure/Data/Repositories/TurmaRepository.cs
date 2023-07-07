using Dapper;
using DotNet.Domain.Core.Interfaces.Repositories;
using DotNet.Domain.Core.Notification;
using DotNet.Domain.Entities;
using DotNet.Domain.Pagination;
using DotNet.Instrastructure.Data.Repositories.Base;
using DotNet.Instrastructure.Data.Repositories.SqlScripts.Turma;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace DotNet.Instrastructure.Data.Repositories
{
    public class TurmaRepository : RepositoryBase, ITurmaRepository
    {
        public TurmaRepository(IConfiguration configuration, INotifier notifier) : base(configuration, notifier)
        {
        }

        public async Task<IEnumerable<Turma>> GetAll()
        {
            using (sqlConnection = new SqlConnection(_configuration.GetConnectionString("Connection")))
            {
                try
                {
                    return await sqlConnection.QueryAsync<Turma>(TurmaScript.GetAll);
                }
                catch (Exception ex)
                {
                    RequisitionError(ex);
                    return null;
                }
            }
        }

        public async Task<PagedList<Turma>> GetAllPaginated(int page, int size)
        {
            using (sqlConnection = new SqlConnection(_configuration.GetConnectionString("Connection")))
            {
                try
                {
                    var parameters = new { Page = (page - 1) * size + 1, PageSize = size * page };

                    IEnumerable<Tuple<Turma, int>> results = await sqlConnection.QueryAsync<Turma, int, Tuple<Turma, int>>(TurmaScript.GetAllPaginated,
                                                                                                    (turma, totalCount) => Tuple.Create(turma, totalCount),
                                                                                                    parameters,
                                                                                                    splitOn: "Total");

                    IEnumerable<Turma> data = results.Select(tuple => tuple.Item1);
                    int totalCount = results.FirstOrDefault()?.Item2 ?? 0;

                    return await Task.FromResult(PagedList<Turma>.ToPagedList(data.ToList(), totalCount, page, size));
                }
                catch (Exception ex)
                {
                    RequisitionError(ex);
                    return null;
                }
            }
        }

        public async Task<Turma> GetById(int id)
        {
            using (sqlConnection = new SqlConnection(_configuration.GetConnectionString("Connection")))
            {
                try
                {
                    Turma turma = await sqlConnection.QueryFirstOrDefaultAsync<Turma>(TurmaScript.GetById, new { Id = id });

                    if (turma == null)
                        AddNotification("Turma não encontrada");

                    return turma;
                }
                catch (Exception ex)
                {
                    RequisitionError(ex);
                    return null;
                }
            }
        }

        public async Task<EntityPaged<Turma>> GetDetails(int id, int page, int pageSize)
        {
            using (sqlConnection = new SqlConnection(_configuration.GetConnectionString("Connection")))
            {
                try
                {
                    int total = 0;
                    Dictionary<int, Turma> turmaDictionary = new Dictionary<int, Turma>();
                    await sqlConnection.QueryAsync<Turma, Aluno, int, Turma>(TurmaScript.GetDetails, (turma, aluno, totalAlunos) =>
                    {
                        if (!turmaDictionary.TryGetValue(turma.Id, out var turmaEntry))
                        {
                            turmaEntry = turma;
                            turmaDictionary.Add(turmaEntry.Id, turmaEntry);
                        }

                        turmaEntry.Alunos.Add(aluno);
                        total = totalAlunos;

                        return turmaEntry;
                    }, splitOn: "Id, Id, TotalAlunos", param: new { Id = id, PageNumber = page, PageSize = pageSize });

                    EntityPaged<Turma> turma = new(turmaDictionary.Values.FirstOrDefault(), total, page, pageSize);

                    if (turma.Entity == null)
                        AddNotification("Nenhum aluno registrado na turma.");

                    return turma;
                }
                catch (Exception ex)
                {
                    RequisitionError(ex);
                    return null;
                }
            }
        }

        public async Task<Turma> Post(Turma turma)
        {
            try
            {
                using (sqlConnection = new SqlConnection(_configuration.GetConnectionString("Connection")))
                {
                    await sqlConnection.OpenAsync();
                    using DbTransaction transaction = await sqlConnection.BeginTransactionAsync();
                    try
                    {
                        turma.Id = await sqlConnection.ExecuteScalarAsync<int>(TurmaScript.Post, turma, transaction);

                        if (turma.Id == 0)
                            AddNotification("Falha ao inserir a turma.");

                        transaction.Commit();
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

            return turma;
        }

        public async Task<Turma> Put(Turma turma)
        {
            try
            {
                using (sqlConnection = new SqlConnection(_configuration.GetConnectionString("Connection")))
                {
                    await sqlConnection.OpenAsync();
                    using DbTransaction transaction = await sqlConnection.BeginTransactionAsync();
                    try
                    {
                        bool result = await sqlConnection.ExecuteAsync(TurmaScript.Put, turma, transaction) > 0;
                        transaction.Commit();

                        if (!result)
                            AddNotification("Falha ao atualizar a turma.");
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

            return turma;
        }

        public async Task<bool> PutStatus(Turma turma)
        {
            try
            {
                using (sqlConnection = new SqlConnection(_configuration.GetConnectionString("Connection")))
                {
                    await sqlConnection.OpenAsync();
                    using DbTransaction transaction = await sqlConnection.BeginTransactionAsync();
                    try
                    {
                        bool result = await sqlConnection.ExecuteAsync(TurmaScript.PutStatus, turma, transaction) > 0;
                        transaction.Commit();

                        if (!result)
                        {
                            AddNotification("Falha ao atualizar o status da turma.");
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
                        bool result = await sqlConnection.ExecuteAsync(TurmaScript.Delete, new { Id = id }, transaction) > 0;
                        transaction.Commit();

                        if (!result)
                            AddNotification("Falha ao remover a turma.");

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

        public bool TurmaNameValidator(string name, int id)
        {
            using (sqlConnection = new SqlConnection(_configuration.GetConnectionString("Connection")))
            {
                try
                {
                    return sqlConnection.ExecuteScalar<int>(TurmaScript.ValidateTurmaName, new { Nome = name, Id = id }) > 0;
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