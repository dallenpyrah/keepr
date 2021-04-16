using System;
using System.Data;
using Dapper;
using server.Models;

namespace server.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;

        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal VaultKeep CreateOne(VaultKeep newVaultKeep)
        {
            string sql = @"INSERT INTO vaultkeeps
            (vaultId, keepId, creatorId)
            VALUES
            (@VaultId, @KeepId, @CreatorId);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newVaultKeep);
            newVaultKeep.Id = id;
            return newVaultKeep;
        }

        internal VaultKeep DeleteOne(int id)
        {
            string sql = "DELETE FROM vaultkeeps WHERE id = @id LIMIT 1;";
            return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
        }

        internal VaultKeep GetOneById(int id)
        {
            string sql = @"SELECT * FROM vaultkeeps WHERE id = @id;";
            return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
        }
    }
}