import { AppState } from '../AppState'
import { api } from './AxiosService'

class VaultsService {
  async getVaultById(id) {
    const res = await api.get('api/vaults/' + id)
    AppState.activeVault = res.data
  }

  async deleteVault(id) {
    await api.delete('api/vaults/' + id)
  }

  async createVault(newVault) {
    const res = await api.post('api/vaults/', newVault)
    AppState.profileVaults = [...AppState.profileVaults, res.data]
  }
}

export const vaultsService = new VaultsService()
