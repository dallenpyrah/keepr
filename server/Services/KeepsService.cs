using System;
using System.Collections.Generic;
using server.Models;
using server.Repositories;

namespace server.Services
{
    public class KeepsService
    {

        private readonly KeepsRepository _krepo;

        public KeepsService(KeepsRepository krepo)
        {
            _krepo = krepo;
        }

        internal IEnumerable<Keep> GetAllKeeps()
        {
            return _krepo.GetAllKeeps();
        }

        internal Keep GetOneKeep(int id)
        {
            Keep keep = _krepo.GetOneKeep(id);
            if(keep == null){
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
            if(editKeep == null){
                throw new SystemException("Invalid Id: This keep does not exist or the wrong Id was passed in the edit request.");
            }
            if(editKeep.CreatorId != current.CreatorId){
                throw new SystemException("You are not the owner of this keep, you do not have permission to edit.");
            }
            editKeep.Description = editKeep.Description != null ? editKeep.Description : current.Description;
            editKeep.Img = editKeep.Img != null ? editKeep.Img : current.Img;
            editKeep.Keeps = editKeep.Keeps > 0 ? editKeep.Keeps : current.Keeps;
            editKeep.Name = editKeep.Name != null ? editKeep.Name : current.Name;
            editKeep.Shares = editKeep.Shares > 0 ? editKeep.Shares : current.Shares;
            return _krepo.EditOneKeep(editKeep);
        }

        internal Keep DeleteOne(int id, string userInfoId)
        {
            Keep current = GetOneKeep(id);
            if(current == null){
                throw new SystemException("Invalid Id: This keep does not exist or the wrong Id was passed in the delete request.");
            }
            if(current.CreatorId != userInfoId){
                throw new SystemException("You are no the owner of this keep, you do not have the permission to edit.");
            }
            return _krepo.DeleteOneKeep(id);
        }
    }
}