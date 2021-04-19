import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class VaultKeepsService {
  async createVaultKeep(newVaultKeep) {
    try {
      const res = await api.post('api/vaultkeeps', newVaultKeep)
      AppState.profileVaultKeeps = [...AppState.profileVaultKeeps, res.data]
    } catch (error) {
      logger.log(error)
    }
  }

  async getKeepsByVaultId(id) {
    try {
      const res = await api.get(`api/vaults/${id}/keeps`)
      console.log(res.data)
      AppState.vaultPage = true
      AppState.vaultKeeps = res.data
      return res
    } catch (error) {
      logger.log(error)
    }
  }

  async deleteVaultKeep(id, vaultId) {
    try {
      await api.delete(`api/vaultkeeps/${id}`)
      this.getKeepsByVaultId(vaultId)
    } catch (error) {

    }
  }
}

export const vaultKeepsService = new VaultKeepsService()
