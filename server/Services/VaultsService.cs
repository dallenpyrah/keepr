using System;
using System.Collections.Generic;
using System.Linq;
using server.Models;
using server.Repositories;

namespace server.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _vrepo;

        public VaultsService(VaultsRepository vrepo)
        {
            _vrepo = vrepo;
        }

        internal IEnumerable<Vault> GetAllVaults()
        {
            return _vrepo.GetAllVaults();
        }

        internal Vault GetOneVault(int id)
        {
            Vault vault = _vrepo.GetOneVault(id);
            if (vault == null)
            {
                throw new SystemException("Invalid Id: This vault does not exist or the wrong Id was passed in the get one request.");
            }
            if (vault.IsPrivate == true)
            {
                throw new SystemException("This vault is private.");
            }
            return vault;
        }

        internal Vault CreateOneVault(Vault newVault)
        {

            return _vrepo.CreateOneVault(newVault);
        }

        internal Vault EditOneVault(Vault editVault)
        {
            Vault current = GetOneVault(editVault.Id);
            if (current == null)
            {
                throw new SystemException("Invalid Id: This vault does not exist or the wrong Id was passed in the put request.");
            }
            if (current.CreatorId != editVault.CreatorId)
            {
                throw new SystemException("You are not the creator of this vault therefore you do not have permission to edit this.");
            }
            editVault.Description = editVault.Description != null ? editVault.Description : current.Description;
            editVault.Name = editVault.Name != null ? editVault.Name : current.Name;
            editVault.IsPrivate = editVault.IsPrivate != editVault.IsPrivate ? editVault.IsPrivate : current.IsPrivate;
            return _vrepo.EditOneVault(editVault);
        }

        internal IEnumerable<Vault> GetVaultsByProfileId(string id)
        {
                // return _vrepo.GetVaultsByProfileId(id);
                IEnumerable<Vault> vaults = _vrepo.GetVaultsByProfileId(id);
                return vaults.ToList().FindAll(v => v.IsPrivate == false);
        }

        internal Vault DeleteOneVault(int id, string userInfoId)
        {
            Vault current = GetOneVault(id);
            if (current == null)
            {
                throw new SystemException("Invalid Id: This vault does not exist or the wrong Id was passed in the delete request.");
            }
            if (current.CreatorId != userInfoId)
            {
                throw new SystemException("You are not the creator of this vault therefore you do not have permission to delete this.");
            }
            return _vrepo.DeleteOneVault(id);
        }
    }
}