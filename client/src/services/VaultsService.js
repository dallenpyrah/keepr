import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class VaultsService {
  async getVaultById(id) {
    try {
      const res = await api.get('api/vaults/' + id)
      AppState.activeVault = res.data
    } catch (error) {
      logger.log(error)
    }
  }

  async deleteVault(id) {
    try {
      await api.delete('api/vaults/' + id)
    } catch (error) {
      logger.log(error)
    }
  }

  async createVault(newVault) {
    try {
      const res = await api.post('api/vaults/', newVault)
      AppState.profileVaults = [...AppState.profileVaults, res.data]
    } catch (error) {
      logger.log(error)
    }
  }
}

export const vaultsService = new VaultsService()
