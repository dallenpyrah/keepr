using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using server.Models;

namespace server.Repositories
{
    public class KeepsRepository
    {

        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Keep> GetAllKeeps()
        {
            string sql = @"
            SELECT
            keep.*,
            profile.*
            FROM keeps keep
            JOIN profiles profile ON keep.creatorId = profile.id;";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
            {
                keep.Creator = profile;
                return keep;
            }, splitOn: "id");
        }

        internal Keep GetOneKeep(int id)
        {
            string sql = @"
            SELECT
            keep.*,
            profile.*
            FROM keeps keep
            JOIN profiles profile ON keep.creatorId = profile.id
            WHERE keep.id = @id";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
            {
                keep.Creator = profile;
                return keep;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        internal Keep CreateOneKeep(Keep newKeep)
        {
            string sql = @"
            INSERT INTO keeps
            (name, description, img, creatorId)
            VALUES
            (@Name, @Description, @Img, @CreatorId);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newKeep);
            newKeep.Id = id;
            return newKeep;
        }

        internal Keep EditOneKeep(Keep editKeep)
        {
            string sql = @"UPDATE keeps
                SET 
                name = @Name,
                description = @Description,
                img = @Img
                WHERE id = @id;";
            return _db.QueryFirstOrDefault<Keep>(sql, editKeep);
        }

        internal Keep DeleteOneKeep(int id)
        {
            string sql = @"DELETE FROM keeps WHERE id = @id LIMIT 1;";
            return _db.QueryFirstOrDefault<Keep>(sql, new { id });
        }

        internal IEnumerable<Keep> GetKeepsByProfileId(string id)
        {
            string sql = @"
            SELECT
            keep.*,
            profile.*
            FROM keeps keep
            JOIN profiles profile ON keep.creatorId = profile.id
            WHERE keep.creatorId = @id;";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
            {
                keep.Creator = profile;
                return keep;
            }, new { id }, splitOn: "id");
        }


        //  This joins the keeps and profiles table to the vaultkeep table at the keepId and creatorId.
        //  Make sure in the future that you are joining the keeps to the vaultkeep table and not the vaultkeep table to the keeps.
        internal IEnumerable<VaultKeepViewModel> GetKeepsByVaultId(int id)
        {
            string sql = @"
            SELECT
            keep.*,
            vaultkeep.id AS VaultKeepId,
            profile.*
            FROM vaultkeeps vaultkeep
            JOIN keeps keep ON vaultkeep.keepId = keep.id
            JOIN profiles profile ON vaultkeep.creatorId = profile.id
            WHERE vaultkeep.vaultId = @id;";
            return _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (vk, profile) =>
            {
                vk.Creator = profile;
                return vk;
            }, new { id }, splitOn: "id");
        }
    }
}