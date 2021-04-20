using System;
using System.Collections.Generic;
using System.Linq;
using server.Models;
using server.Repositories;

namespace server.Services
{
    public class KeepsService
    {

        private readonly KeepsRepository _krepo;

        private readonly VaultsService _vservice;

        public KeepsService(KeepsRepository krepo, VaultsService vservice)
        {
            _krepo = krepo;
            _vservice = vservice;
        }

        internal IEnumerable<Keep> GetAllKeeps()
        {
            return _krepo.GetAllKeeps();
        }

        internal Keep GetOneKeep(int id)
        {
            Keep keep = _krepo.GetOneKeep(id);
            if (keep == null)
            {
                throw new SystemException("Invalid Id: This keep does not exist or the wrong Id was passed in the get one request.");
            }
            return keep;
        }

        internal Keep CreateOneKeep(Keep newKeep)
        {
            return _krepo.CreateOneKeep(newKeep);
        }

        internal Keep EditOneKeep(Keep editKeep)
        {
            Keep current = GetOneKeep(editKeep.Id);
            if (editKeep == null)
            {
                throw new SystemException("Invalid Id: This keep does not exist or the wrong Id was passed in the edit request.");
            }
            if (editKeep.CreatorId != current.CreatorId)
            {
                if (editKeep.Keeps > 0)
                {
                    editKeep.Keeps = editKeep.Keeps > 0 ? editKeep.Keeps : current.Keeps;
                }
                 if (editKeep.Views > 0)
                {
                    editKeep.Views = editKeep.Views > 0 ? editKeep.Views : current.Views;
                }
                else
                {

                    throw new SystemException("You are not the owner of this keep, you do not have permission to edit.");
                }
            }
            editKeep.Description = editKeep.Description != null ? editKeep.Description : current.Description;
            editKeep.Img = editKeep.Img != null ? editKeep.Img : current.Img;
            editKeep.Keeps = editKeep.Keeps > 0 ? editKeep.Keeps : current.Keeps;
            editKeep.Name = editKeep.Name != null ? editKeep.Name : current.Name;
            editKeep.Shares = editKeep.Shares > 0 ? editKeep.Shares : current.Shares;
            return _krepo.EditOneKeep(editKeep);
        }

        internal IEnumerable<Keep> GetKeepsByProfileId(string id)
        {
            return _krepo.GetKeepsByProfileId(id);
        }

        internal Keep DeleteOne(int id, string userInfoId)
        {
            Keep current = GetOneKeep(id);
            if (current == null)
            {
                throw new SystemException("Invalid Id: This keep does not exist or the wrong Id was passed in the delete request.");
            }
            if (current.CreatorId != userInfoId)
            {
                throw new SystemException("You are no the owner of this keep, you do not have the permission to edit.");
            }
            return _krepo.DeleteOneKeep(id);
        }

        internal IEnumerable<VaultKeepViewModel> GetKeepsByVaultId(int id)
        {
            var vault = _vservice.GetOneVault(id);
            if (vault.IsPrivate != false)
            {
                throw new SystemException("This vault is private.");
            }
            return _krepo.GetKeepsByVaultId(id);
        }

        internal IEnumerable<VaultKeepViewModel> GetKeepsByVaultId(int id, string userInfoId)
        {
            var vault = _vservice.GetOneVault(id, userInfoId);
            if (vault.IsPrivate == true && userInfoId != vault.CreatorId)
            {
                throw new SystemException("This vault is private.");
            }
            return _krepo.GetKeepsByVaultId(id);
        }
    }
}