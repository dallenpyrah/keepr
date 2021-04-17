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
}

export const vaultsService = new VaultsService()
