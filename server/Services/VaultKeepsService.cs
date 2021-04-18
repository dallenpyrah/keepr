using System;
using server.Models;
using server.Repositories;

namespace server.Services
{
    public class VaultKeepsService
    {

        private readonly VaultKeepsRepository _vkrepo;

        public VaultKeepsService(VaultKeepsRepository vkrepo)
        {
            _vkrepo = vkrepo;
        }

        internal VaultKeep CreateOne(VaultKeep newVaultKeep)
        {
            VaultKeep current = _vkrepo.GetOneKeepById(newVaultKeep.KeepId);
            if (current == null)
            {
                return _vkrepo.CreateOne(newVaultKeep);
            }
            else
            {
                throw new SystemException("Vault keep already exists");
            }
        }

        internal VaultKeep DeleteOne(int id, string userInfoId)
        {
            VaultKeep current = _vkrepo.GetOneById(id);
            if (current == null)
            {
                throw new SystemException("Invalid Id: This vault keep does not exist or the wrong Id was passed in the delete request.");
            }
            if (current.CreatorId != userInfoId)
            {
                throw new SystemException("You are not the creator of this, therefore you can not delete this relationsip.");
            }
            _vkrepo.DeleteOne(id);
            return current;

        }
    }
}