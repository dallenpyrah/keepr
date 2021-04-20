import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import { profilesService } from './ProfilesService'
import { vaultKeepsService } from './VaultKeepsService'

class KeepsService {
  async getAllKeeps() {
    try {
      const res = await api.get('api/keeps')
      AppState.keeps = res.data
    } catch (error) {
      logger.log(error)
    }
  }

  async deleteKeep(id, vaultId) {
    try {
      await api.delete('api/keeps/' + id)
      this.getAllKeeps()
      await profilesService.getUserKeeps(AppState.account.id)
      if (vaultId) { await vaultKeepsService.getKeepsByVaultId(vaultId) }
    } catch (error) {
      logger.log(error)
    }
  }

  async createKeep(newKeep) {
    try {
      const res = await api.post('api/keeps', newKeep)
      AppState.profileKeeps = [...AppState.profileKeeps, res.data]
    } catch (error) {
      logger.log(error)
    }
  }

  async editKeepCount(id, editKeep) {
    try {
      editKeep.keeps++
      await api.put('api/keeps/' + id, editKeep)
    } catch (error) {
      logger.log(error)
    }
  }

  async editViewCount(id, editKeep) {
    editKeep.views++
    await api.put('api/keeps/' + id, editKeep)
  }
}

export const keepsService = new KeepsService()
