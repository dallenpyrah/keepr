using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using server.Models;

namespace server.Repositories
{
    public class VaultsRepository
    {

        private readonly IDbConnection _db;

        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Vault> GetAllVaults()
        {
            string sql = @"
            SELECT 
            vault.*,
            profile.*
            FROM vaults vault
            JOIN profiles profile ON vault.creatorId = profile.id;";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
            {
                vault.Creator = profile;
                return vault;
            }, splitOn: "id");
        }

        internal Vault GetOneVault(int id)
        {
            string sql = @"
            SELECT
            vault.*,
            profile.*
            FROM vaults vault
            JOIN profiles profile ON vault.creatorId = profile.id
            WHERE vault.id = @id;";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
            {
                vault.Creator = profile;
                return vault;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        internal Vault CreateOneVault(Vault newVault)
        {
            string sql = @"INSERT INTO vaults
            (name, description, isPrivate, img, creatorId)
            VALUES
            (@Name, @Description, @IsPrivate, @Img, @CreatorId);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newVault);
            newVault.Id = id;
            return newVault;
        }

        internal Vault EditOneVault(Vault editVault)
        {
            string sql = @"UPDATE vaults
            SET
                name = @Name,
                description = @Description,
                img = @Img,
                isPrivate = @IsPrivate
            WHERE id = @id;";
            return _db.QueryFirstOrDefault<Vault>(sql, editVault);
        }

        internal IEnumerable<Vault> GetVaultsByProfileId(string id)
        {
            string sql = @"
            SELECT 
            vault.*,
            profile.*
            FROM vaults vault
            JOIN profiles profile ON vault.creatorId = profile.id
            WHERE vault.creatorId = @id;";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
            {
                vault.Creator = profile;
                return vault;
            }, new { id }, splitOn: "id");
        }

        internal Vault DeleteOneVault(int id)
        {
            string sql = @"DELETE FROM vaults WHERE id = @id LIMIT 1;";
            return _db.QueryFirstOrDefault<Vault>(sql, new { id });
        }
    }
}